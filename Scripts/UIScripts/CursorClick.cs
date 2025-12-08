using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorClick : MonoBehaviour
{
    public AudioClip SFXclick;


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.SFXPlay("Click", SFXclick);

        }

    }

}
