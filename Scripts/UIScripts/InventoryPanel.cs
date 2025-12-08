using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public PlayerMove playerMove;
    [SerializeField]
    private GameObject uiInventoryPanel;
    [SerializeField]
    private GameObject uiBookPanel;
    public GameObject storePanel;
    private GameObject uiOptionPanel;

    public Image BGPanel;
    public Sprite[] bgSprite;
    public GameObject[] InventoryItemObjects;

    [Header("인벤토리 텍스트")]
    public TextMeshProUGUI Inven_ItemText;
    public TextMeshProUGUI Inven_DescriptionText;
    public string[] Inven_Description;

    [Header("도감 텍스트")]
    public TextMeshProUGUI Book_Text;
    public TextMeshProUGUI Book_DescriptionText;

   
    public bool firstcheck = false;


    public static InventoryPanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // "OptionCanvas"라는 이름의 오브젝트를 찾기
        GameObject optionCanvas = GameObject.Find("OptionCanvas");

        if (optionCanvas != null && optionCanvas.transform.childCount > 0)
        {
            // 첫 번째 자식 가져오기
            uiOptionPanel = optionCanvas.transform.GetChild(0).gameObject;

            Debug.Log("첫 번째 자식을 성공적으로 가져왔습니다: " + uiOptionPanel.name);
        }
        else
        {
            Debug.LogWarning("OptionCanvas를 찾을 수 없거나 자식이 없습니다!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!firstcheck)
            {
                QuestSystem.Instance.AxcMission("inven");
                firstcheck = true;
                

            }

            playerMove.moveStop = !playerMove.moveStop;
            uiInventoryPanel.SetActive(!uiInventoryPanel.activeSelf);
        }

     
     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerMove.moveStop = !playerMove.moveStop;
            uiOptionPanel.SetActive(!uiOptionPanel.activeSelf);
        }
    }


    public void BGFruit()
    {
        BGPanel.sprite = bgSprite[0];
        Inven_ItemText.text = "과일";
        InventoryItemObjects[0].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[0];
    }

    public void BGBread()
    {
        BGPanel.sprite = bgSprite[1];
        Inven_ItemText.text = "빵";
        InventoryItemObjects[1].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[1];
    }

    public void BGCookie()
    {
        BGPanel.sprite = bgSprite[2];
        Inven_ItemText.text = "쿠키";
        InventoryItemObjects[2].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[2];
    }

    public void BGHam()
    {
        BGPanel.sprite = bgSprite[3];
        Inven_ItemText.text = "햄";
        InventoryItemObjects[3].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[3];
    }

    public void BGWine()
    {
        BGPanel.sprite = bgSprite[4];
        Inven_ItemText.text = "와인";
        InventoryItemObjects[4].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[4];
    }

    public void BGAx()
    {
        BGPanel.sprite = bgSprite[5];
        Inven_ItemText.text = "도끼";
        InventoryItemObjects[5].SetActive(true);
        Inven_DescriptionText.text = Inven_Description[5];
    }

    public void BGCTree()
    {
        BGPanel.sprite = bgSprite[6];
        Inven_ItemText.text = "나무";
        Inven_DescriptionText.text = Inven_Description[6];
    }

    public void BGShip()
    {
        BGPanel.sprite = bgSprite[7];
        Inven_ItemText.text = "양";
        Inven_DescriptionText.text = Inven_Description[7];
    }

    public void BGCrow()
    {
        BGPanel.sprite = bgSprite[8];
        Inven_ItemText.text = "까마귀";
        Inven_DescriptionText.text = Inven_Description[8];
    }

    public void BGWooltari()
    {
        BGPanel.sprite = bgSprite[9];
        Inven_ItemText.text = "울타리";
        Inven_DescriptionText.text = Inven_Description[9];
    }

    public void BGRope()
    {
        BGPanel.sprite = bgSprite[10];
        Inven_ItemText.text = "로프";
        Inven_DescriptionText.text = Inven_Description[10];
    }

    public void BGClock()
    {
        BGPanel.sprite = bgSprite[11];
        Inven_ItemText.text = "시계";
        Inven_DescriptionText.text = Inven_Description[11];
    }
    public void InventoryMouseClick()
    {
        uiInventoryPanel.SetActive(!uiInventoryPanel.activeSelf);
    }

    public void BoockMouseClick()
    {
        uiBookPanel.SetActive(!uiBookPanel.activeSelf);
    }

    public void OptionMosueClick()
    {
        uiOptionPanel.SetActive(!uiOptionPanel.activeSelf);
    }

}
