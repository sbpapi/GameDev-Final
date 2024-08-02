using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
  //create a singleton for this target
  public static GameManager Instance{get;private set;}

 public int score = 0;// a var to track the player's current points

 public GameObject pickupParent;// a var to hold the pickup parent game object; this is used to count our picks // ASSIGN IN EDITOR
 public TextMeshProUGUI scoreText;

 public GameObject victoryText; // a var to hold our victory text

public int totalPickups = 0;
private PlayerController player;

    private void Awake() // Awake() is called once when the script enters the score
    {
        if (Instance == null) // if there is no other copy of this script in the scene...
        {
            Instance = this; // "this" refers to itself
        }
        else // this is NOT the only copy of the GameManager script in the scene...
        { 
        // delete this extra copy of this script
            Debug.LogWarning("Cannot have more than one instance of [GameManager] in the scene! Deleted extra copy");
            Destroy(this.gameObject);
        }
        
    }


    private void Start()
    {
        score = 0; // reset the score back to 0 every time the game starts 
        UpdateScoreText();

        victoryText.SetActive(false); // disabled the victory text when the game begins


        totalPickups = pickupParent.transform.childCount; // count how many pickups are in the level 
        // count how many pick ups are in the game
    }

    public void UpdateScore(int amount)
    {
        // Increase the score variable by the amount given
        score += amount;
        UpdateScoreText();
        
        if (totalPickups <= -50) // if there are no more pickups in the level 
        {
          WinGame(); // win the game
        }
    }

    public void UpdateScoreText()
    { // string  //int
        scoreText.text = "Score: " + score.ToString(); // updates the score text text from the player's score
    }
    
    public void WinGame()
    {
        victoryText.SetActive(true); // enable our victory text
        GameOver();
    }

    public void GameOver()
    {
        Invoke("LoadCurrentLevel",2f);
    }
    
    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoseGame()
    {
        if(player.health <= 0)
     {
        GameOver();
     }
   
    }

    // New method to update player's health
    public void UpdateHealth(int newHealth)
    {
        if (player != null)
        {
            player.health = newHealth;

            // Optimal: Check for player's death
            if (player.health <= 0)
            {
                LoseGame();
            }
        }
    }            

}

