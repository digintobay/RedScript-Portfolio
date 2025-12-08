using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUISound : MonoBehaviour
{
    public AudioClip fireSound;

   
    public void FireSoundPlay()
    {
        SoundManager.instance.SFXPlay("BurnButton", fireSound);
    }
}
