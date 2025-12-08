using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YangleCameraBillboard : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 dir = mainCam.transform.position - transform.position;
        dir.y = 0f;          transform.rotation = Quaternion.LookRotation(dir);
        
    }
}
