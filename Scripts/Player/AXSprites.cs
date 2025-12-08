using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AXSprites : MonoBehaviour
{
    //SpriteRenderer spriteRenderer;
    Rigidbody rigid;

    GameObject firstChild;
    AXCameraBillboard cameraBillboard;

    public GameObject talkPanel;
    public TextMeshPro talkText;
    private string[] talkPaneltext = { "그렇지! 그렇게!\n다 잡아 버려!", "아하하하!\n아하하하!",
    "빨간 망토야,\n내 소중한 친구!", "사랑스럽구나 얘야!\n끔찍하게도!"};
    private bool checkGround = false;

    private void Awake()
    {
        firstChild = transform.GetChild(0).gameObject;
      
        rigid = GetComponent<Rigidbody>();
        cameraBillboard = firstChild.GetComponent<AXCameraBillboard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
      
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            firstChild.transform.localRotation = Quaternion.Euler(0, 0, -90f);
            cameraBillboard.enabled = true;
            checkGround = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && checkGround)
        {
            var random = Random.Range(0, talkPaneltext.Length);
            talkText.text = talkPaneltext[random];
            talkPanel.SetActive(true);
            checkGround = false;
        }
    }


}
