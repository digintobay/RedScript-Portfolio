using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnOff : MonoBehaviour
{
    private SpriteRenderer finderSprite;
    private Vector3 originalLocalPos;

    private void Awake()
    {
                 finderSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        finderSprite.gameObject.SetActive(false);  
                 originalLocalPos = finderSprite.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 relativePos = other.transform.position - transform.position;

            if (relativePos.x < 0)
            {
                                 finderSprite.flipX = true;
                finderSprite.transform.localPosition = originalLocalPos + new Vector3(-0.5f, 0, 0);
            }
            else
            {
                                 finderSprite.flipX = false;
                finderSprite.transform.localPosition = originalLocalPos;
            }

            finderSprite.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finderSprite.gameObject.SetActive(false);
            finderSprite.transform.localPosition = originalLocalPos;          }
    }
}