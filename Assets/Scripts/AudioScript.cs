using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource sound;
    public void PlaySound()
    {
        sound.time = 0.05f;
        sound.Play();
    }
}