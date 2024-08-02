using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  //1. Detect collisions
  //2. Identify objects colliding with this
  //3. Destroy this if the colliding object
  //4. Every frame, update the rotation of this to attract the player

   public int pointValue = 1; //a var to store how many points this pickup is worth

  //This function is called whenever this collider collides with another marked as a trigger (this object can be the trigger)
    private void OnTriggerEnter(Collider other)
    {  
     if(other.gameObject.CompareTag("Player")) // if the collider this pickup just hit has the tag "Player"
      {
        Destroy(this.gameObject); //destroy this pickup
      GameManager.Instance.UpdateScore(pointValue); //Tell the GameManager to update the score by 1
      GameManager.Instance.totalPickups -=1; //tell the game manager to subtract from the total # of pickups
      }
      
    }

   private void Update()
    {
      transform.Rotate(new Vector3(15,30,45) * Time.deltaTime); //Time.delta will make something frame independent
    }
}
