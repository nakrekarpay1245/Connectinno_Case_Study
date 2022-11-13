using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer; 
    
    public void SetVolume(float _value)
    {
        audioMixer.SetFloat("volume", _value);
    }
}
