using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
  public Transform cameraTransform;
  public CharacterController controller;
  public LayerMask groundLayer;
  public float speed = 10f;
  public float rotationSpeed = 7f;
  private float pitch = 0f;
  public float pitchSpeed = 7f;
  public float pitchRange =  45f;
  private bool isGrounded;

  public float groundCheckDist =  1.0f; // Check if grounded by distance
  public float jumpForce =  5f; // Jump Force
  public float gravity = -9.81f; // Gravity Constant Value
  public float gravityScale = 3f; // Adjust the gravity scale as needed
  private Vector3 velocity; //  Player's current velocity  

  public float moveSpeed = 5; //variable to control how fast the player moves

 private Vector3 moveDirection = Vector3.zero;  // A vector (x, y, z) to determine the direction the player moves in

 public int health;

 public EnemyFollow enemy;

   //Start is called before the first frame update 

  //variable to control how fast the player moves
    // Start is called before the first frame update
    void Start()
    {
     controller = GetComponent<CharacterController>();// Assign the controller variable to the player's character controller component 
    }

    // Update is called once per frame
    void Update()
    {
      MovePlayer();

      // Handle looking around the mouse/trackpad
      LookAround();

      // Apply gravity to the player
      ApplyGravity();

      // Jump input
      if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
      {
         Jump();
      }
    }

    void MovePlayer()
    {  
      // Get Input from WASD keys or arrow keys
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");

      // Calculate movement direction relative to the camera 
      Vector3 forward = cameraTransform.forward;
      Vector3 right = cameraTransform.right;

      // Ensure the movement direction is horizontal 
      forward.y =  0f;
      right.y = 0f;

      // Normalize directions to ensure consistent speed 
      forward.Normalize();
      right.Normalize();

      // Calculate movement direction 
      Vector3 movement = forward * moveVertical + right * moveHorizontal;

      // Move the player using the CharacterController
      controller.Move(movement * speed * Time.deltaTime);
    }

    void LookAround()
    {
       // Get mouse input for rotation
       float rotateVertical = Input.GetAxis("Mouse Y");
       float rotateHorizontal = Input.GetAxis("Mouse X");

       Debug.Log("Pitch value:" + Input.GetAxis("Mouse Y"));

       // Rotate the player around the Y-axis (horizontal rotation)
       transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed);

       // Adjust the pitch (verticaal rotation) of the camera
       pitch -= rotateVertical * pitchSpeed;
       pitch =  Mathf.Clamp(pitch, -pitchRange, pitchRange); //  Clamp pitch to avoid flipping over

       // Apply the pitch rotation to the camera 
       cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Jump()
    {
        // Set the velocity for the jump
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity * gravityScale);  
    }

    void ApplyGravity()
    {
        // Check if the player is grounded 
        isGrounded =  controller.isGrounded || IsGrounded();

        // If grounded and descending, set a small negative value to keep the player grounded 
        if (isGrounded && velocity.y < 0 )
        {
          velocity.y = -2f; 
        }

        // Apply gravity to the velocity
        velocity.y += gravity  * gravityScale * Time.deltaTime;

        // Move the player using the CharacterController 
        controller.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Peform a Raycast downward to check if there's ground beneath the player
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDist, groundLayer);
    }    

    void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("Enemy"))
      {
        //trigger the damage
        health -= enemy.damage;

        if(health <= 0)
        {
          GameManager.Instance.GameOver();

          Destroy(gameObject);
        }
        

      }
    }  
   
}
