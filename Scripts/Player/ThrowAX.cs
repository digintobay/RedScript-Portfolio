using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAX : MonoBehaviour
{
    public GameObject prefab;
    public float throwForce = 10f;
    private Camera mainCam;
    public Transform throwSpot;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }


    public void ThrowObject()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPos;
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            targetPos = hit.point;
        }
        else
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float enter;
            if (groundPlane.Raycast(ray, out enter))
                targetPos = ray.GetPoint(enter);
            else
                targetPos = ray.GetPoint(10f);
        }

        GameObject obj = Instantiate(prefab, throwSpot.position, Quaternion.identity);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (targetPos - throwSpot.position).normalized;
            rb.AddForce(dir * throwForce, ForceMode.VelocityChange);
        }
    }
}
