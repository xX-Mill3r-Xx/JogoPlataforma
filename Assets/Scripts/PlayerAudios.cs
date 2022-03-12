using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudios : MonoBehaviour
{

    private AudioSource audioClip;

    public AudioClip coin;
    public AudioClip jump;
    public AudioClip hit;

    void Start()
    {
        audioClip = GetComponent<AudioSource>();
    }
    
    public void PlaySFX(AudioClip sfx)
    {
        audioClip.PlayOneShot(sfx);
    }
}
