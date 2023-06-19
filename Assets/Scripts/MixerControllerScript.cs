using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerControllerScript : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string parameter;

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat(parameter, Mathf.Log10(sliderValue) * 20);
    }
}