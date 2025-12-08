using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rigid;
    private Transform _playerPosition;

    public float speed = 10f;
    public int damage = 1;
    private Vector3 _targetDir;   
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {


        Invoke("GoToBullet", 1f);  
    }

    private void GoToBullet()
    {
        _playerPosition = GameObject.Find("PYSpot").transform;

                 _targetDir = _playerPosition.position - transform.position;

                 rigid.velocity = _targetDir * speed;

                 StartCoroutine(DestroyAfterSeconds(3f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WolfGround"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}