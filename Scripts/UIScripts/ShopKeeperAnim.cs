using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeperAnim : MonoBehaviour
{
    public Image mainImage;
    public Sprite[] sprites;


    private void OnEnable()
    {
        StartCoroutine(UIAnimation());
    }


    private void OnDisable()
    {
        StopCoroutine(UIAnimation());
    }

    IEnumerator UIAnimation()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            mainImage.sprite = sprites[i];
            yield return new WaitForSeconds(0.2f);

            if (i == sprites.Length - 1)
            {
                i = 0;
            }
        }
    }
}
