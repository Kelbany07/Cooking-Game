using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Call this from the Button's OnClick()
    public void PlayClick()
    {
        if (clickClip == null) return;
        audioSource.PlayOneShot(clickClip);
    }
}
