using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingFly : MonoBehaviour
{
    public float speed = 0.5f;   // 움직이는 속도
    public float a = 5f;       // x축 크기
    public float b = 5f;       // z축 크기
    public float yRotationSpeed = 3f; // y축 회전 속도 (deg/sec)

    private float t = 0f;
    private Vector3 center;
    private SpriteRenderer sprite;


    private void Start()
    {
       sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        GameObject obj = GameObject.Find("FlySpot");
        center = obj.transform.position; // 초기 위치를 중심으로
        t += Time.deltaTime * speed;

        // 8자 좌표
        float x = a * Mathf.Sin(t);
        float z = b * Mathf.Sin(t) * Mathf.Cos(t);

        transform.position = center + new Vector3(x, 0, z);

        // y축 회전
        transform.Rotate(Vector3.up, yRotationSpeed * Time.deltaTime);

        if(transform.localPosition.x < 0)
        {
            sprite.flipX = true;
        }
    }
}
