using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;      }

    void LateUpdate()
    {
                 transform.LookAt(transform.position + mainCam.transform.forward);
    }
}
