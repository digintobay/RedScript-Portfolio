using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerWalkTest : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip Walkclip;

    public void WalkSound()
    {
        audioSource.PlayOneShot(Walkclip); // 1È¸ Àç»ý
    }
}
