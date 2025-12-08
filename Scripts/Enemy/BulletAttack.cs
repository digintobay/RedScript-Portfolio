using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public static BulletAttack Instance { get; private set; }
    public int count = 5;
    public int sheepCount = 5;
    public float radius = 5f;
    public GameObject[] sheepObject;

    public GameObject bulletObject;
    public Transform bulletContainer;

    private Transform _playerPosition;

    public int angleInterval = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        _playerPosition = GameObject.Find("PYSpot").transform;
    }

    public IEnumerator MakeSheepTime()
    {
        for (int i = 0; i < sheepCount; i++)
        {
            float angle = i * (360 / sheepCount);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;

            Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos);

            int random = Random.Range(0, 2);
            if (random == 0)
            {
                Instantiate(sheepObject[0], bulletContainer.position + pos, rot, transform);

            }
            else Instantiate(sheepObject[1], bulletContainer.position + pos, rot, transform);







            yield return new WaitForSeconds(0.5f);
        }
    }


    public IEnumerator MakeBulletTime()
    {
        for (int i = 0; i < count; i++)
        {
            float angle = i * (180f / count);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;

            Quaternion rot = Quaternion.LookRotation(Vector3.forward, pos);

            Instantiate(bulletObject, bulletContainer.position + pos, rot, transform);


            yield return new WaitForSeconds(0.5f);
        }


    }
}
