using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXCameraBillboard : MonoBehaviour
{

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 dir = mainCam.transform.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir); Quaternion currentRot = transform.rotation;
            transform.rotation = Quaternion.Euler(
   currentRot.eulerAngles.x,
   targetRot.eulerAngles.y,
   currentRot.eulerAngles.z
);
        }
    }
}
