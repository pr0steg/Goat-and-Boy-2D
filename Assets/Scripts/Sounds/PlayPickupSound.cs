using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPickupSound : MonoBehaviour
{
    // The AudioSource component that will play the sound
    public AudioSource pickupSound;
    public AudioSource pickupSoundBlue;
    public AudioSource pickupSoundYellow;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Milk")
        {
            // Play the sound
            pickupSound.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSound.Play();
        }
        else if (collision.gameObject.tag == "blueMilk")
        {
            // Play the sound
            pickupSoundBlue.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSoundBlue.Play();
        }
        else if (collision.gameObject.tag == "yellowMilk")
        {
            // Play the sound
            pickupSoundYellow.volume = PlayerPrefs.GetFloat("EffectsVolume");
            pickupSoundYellow.Play();
        }
    }
}