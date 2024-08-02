using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    // Create a singleton of this class so only one copy of it exists at a time
    public static AudioManager Instance {get; private set;}



    public Sound[] sounds; // An array to hold all our sound effects; assign in editor




    private void Awake()
    {
         // If there is no other copy of this in the scene...
        if (Instance == null)
        {
            // Assign this as the one and only copy
            Instance = this;
        } else {
            // If this is a duplicate, destroy it
            Destroy(gameObject);
        }



        DontDestroyOnLoad(gameObject); // Do not destroy the AudioManager between scenes, we need the sound effects  intact a1 throughout gameplay




        foreach (Sound s in sounds) // For each soubd object in the sounds array...
        {
            s.audioSource = gameObject.AddComponent<AudioSource>(); // Add an audio  source component to the AudioManager for this given sound, & assign it as that sound's given audio source 
            s.audioSource.clip = s.audioClip; // Assign the audio clip of this sound's audio source 
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }


    }


    private void Start()
    {
         PlaySound("KnightHallows"); // Play the theme music when the game starts 
    }


    
    public void PlaySound(string name)
    {
         // Takes in a string for the name of the sound , &  plays its audio clip
         Sound sound = Array.Find(sounds, sound => sound.name == name);



         if (sound == null)
         {
            Debug.LogWarning("Could not find " + name + " sound!");
            return;

         }

         sound.audioSource.Play();
    }     



    public void StopSound(string name)
    {
         // Takes in a string for the name of the sound, & plays its audio clip
         Sound sound = Array.Find(sounds, sound => sound.name == name);


         if (sound == null)
         {
             Debug.LogWarning("Could not find " + name + " sound!");
             return;
         }

         sound.audioSource.Stop();
    }

}                        


