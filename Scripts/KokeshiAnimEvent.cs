using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokeshiAnimEvent : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip riseClip;
    public AudioClip lowerClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Rise()
    {
        audioSource.PlayOneShot(riseClip, 0.5f);
    }

    public void Lower()
    {
        audioSource.PlayOneShot(lowerClip, 0.5f);
    }
}
