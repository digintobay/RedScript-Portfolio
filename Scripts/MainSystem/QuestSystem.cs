using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        QuestOptionReset();
    }

    public InventoryManager inventory;

    [SerializeField]
    private string[] spotInfoName = new string[] {"붉은 오두막", "삐쭉빼쭉 숲",
        "양의 둥지", "늑대 야영지", "버섯 길", "숲의 미아"};
    [SerializeField]
    private string[] questInfo = new string[] {"착한 아이의 길", " ", " ",
    "가엾은 늑대에게 자비를", " ", " "};
    [SerializeField]
    private bool[] questOption = new bool[5];


    public TextMeshProUGUI _spotName;
    public TextMeshProUGUI _questName;

    public GameObject _missionObjectText;
    public GameObject _theanoObjectText;


    public TextMeshProUGUI _missionPanel;
    public TextMeshProUGUI _theanoMissionPanel;

    public int _foodNum = 0;

    public GameObject _axcPopupTuto;
    public GameObject _attackPopupTuto;
    public GameObject _meetRabbitTuto;
    public GameObject _shopPopupTuto;
    public GameObject _meetWolfTuto;
    public GameObject _happyWolfAnd;
    public GameObject _foodGroup;

    public AudioClip dingSound;

    public bool[] checkTuto = new bool[10];
    public int attacknum = 0;
    public int invennum = 0;
    public int meetnum = 0;

    public bool firstSadOn = false;

    public PlayerMove playerMove;
    public GameObject mainCamera;
    public EnemyAI wolfEnemy;
    public FrogSystem frogSystem;
    private CameraController cameraController;
    public MeetFriendCollider meetCollider;

    public string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);

        QuestOptionNum(0);

        if (questOption[0] == true)
        {
            _spotName.text = "· " + spotInfoName[0];
            _questName.text = "#1  " + questInfo[0];

        }

        for (int i = 0; i < checkTuto.Length; i++)
        {

         checkTuto[i] = false;


        }

        if (currentSceneName == "NoneTutorials")
        {
            checkTuto[1] = true;
            checkTuto[2] = true;
            checkTuto[3] = true;
            checkTuto[4] = true;
        }

        cameraController = mainCamera.GetComponent<CameraController>();
        wolfEnemy = wolfEnemy.GetComponent<EnemyAI>();
        meetCollider = meetCollider.GetComponent<MeetFriendCollider>();
        frogSystem = frogSystem.GetComponent<FrogSystem>();
    }

    void QuestOptionReset()
    {
        for (int i = 0; i < questOption.Length; i++)
        {
            questOption[i] = false;
        }
    }

    void QuestOptionNum(int questindex)
    {
        questOption[questindex] = true;
    }

                   public void FoodMissionCheck(int itemadd)
    {
        _foodNum += itemadd;
        
        SoundManager.instance.SFXPlay("ding", dingSound);

        if (firstSadOn)
        {
            _missionPanel.text = "아이템을 수집하세요!\r\n 음식 (" + _foodNum.ToString() + "/5)";
            FoodCheck(); 
        }

    }

    public void FoodMissionFirstActive()
    {
        frogSystem.GetForgText();
        _missionObjectText.SetActive(true);
        _foodGroup.SetActive(true);
        _missionPanel.text = "아이템을 수집하세요!\r\n 음식 (" + _foodNum.ToString() + "/5)";

        SoundManager.instance.SFXPlay("ding", dingSound);
        firstSadOn = true;
        FoodCheck();
    }

    public void FoodCheck()
    {
        if (_foodNum >= 5)
        {
            meetCollider.HappyOn();
            _missionPanel.text = "늑대에게 돌아가 볼까요?";
            wolfEnemy.HappyEnd();
        }
    }

    public void GoToHappyEnd()
    {
        DontMove();
        _happyWolfAnd.SetActive(true);
    }

    public void Check()
    {
        if (attacknum == 2 && invennum == 1)
        {
          
           
            SoundManager.instance.SFXPlay("ding", dingSound);
            checkTuto[0] = false;
            checkTuto[1] = true;
            _theanoMissionPanel.text = "친구 만나기 (0/4)";
        }
    }



    public void AxcMission(string name)
    {
        if (name == "noax")
        {
            _theanoObjectText.SetActive(true);
            _axcPopupTuto.SetActive(true);
            DontMove();
            _theanoMissionPanel.text = "도끼 줍기 (" + 0.ToString() + "/1)";

        }
        else if (name == "putax")
        {
            _theanoMissionPanel.text = "도끼 줍기 (" + 1.ToString() + "/1)";
            SoundManager.instance.SFXPlay("ding", dingSound);
            checkTuto[0] = true;
            _attackPopupTuto.SetActive(true);
            DontMove();
            AxcMission("useax");
        }

        if (checkTuto[0])
        {


            if (name == "useax")
            {
                _theanoMissionPanel.text = "도끼 휘두르기 (" + 0.ToString() + "/2)\n" +
                                     "인벤토리 열기 (" + 0.ToString() + " /1)";
                Check();
            }

            if (name == "leftax")
            {
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++attacknum;
                _theanoMissionPanel.text = "도끼 휘두르기 (" + attacknum.ToString() + "/2)\n" +
                                    "인벤토리 열기 (" + invennum.ToString() + " /1)";
                Check();
            }

            if (name == "rightax")
            {
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++attacknum;
                _theanoMissionPanel.text = "도끼 휘두르기 (" + attacknum.ToString() + "/2)\n" +
                                    "인벤토리 열기 (" + invennum.ToString() + " /1)";
                Check();
            }

            if (name == "inven")
            {
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++invennum;
                _theanoMissionPanel.text = "도끼 휘두르기 (" + attacknum.ToString() + "/2)\n" +
                                    "인벤토리 열기 (" + invennum.ToString() + " /1)";

                Check();
            }

          

        }

        if (checkTuto[1] == true)
        {
            if (name == "rabbit")
            {
                

                if (checkTuto[2]) return;
                _meetRabbitTuto.SetActive(true);
                DontMove();
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++meetnum;
                _theanoMissionPanel.text = "친구 만나기 (" + meetnum.ToString() + "/4)";
                checkTuto[2] = true;

            }

            if (name == "store")
            {
                if (checkTuto[3]) return;

                _shopPopupTuto.SetActive(true);
                DontMove();
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++meetnum;
                _theanoMissionPanel.text = "친구 만나기 (" + meetnum.ToString() + "/4)";
                checkTuto[3] = true;
            }

            if (name == "frog")
            {
                if (checkTuto[4]) return;
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++meetnum;
                _theanoMissionPanel.text = "친구 만나기 (" + meetnum.ToString() + "/4)";
                checkTuto[4] = true;
            }

            if (name == "wolf")
            {
                if (checkTuto[5]) return;
                _meetWolfTuto.SetActive(true);

                DontMove();
                SoundManager.instance.SFXPlay("ding", dingSound);
                ++meetnum;
                _theanoMissionPanel.text = "친구 만나기 (" + meetnum.ToString() + "/4)";
                checkTuto[5] = true;
            }

            if (meetnum >= 4)
            {
                _theanoMissionPanel.text = "";
            }

        }


    }

    public void DontMove()
    {
        playerMove.moveStop = true;
        playerMove.PlayerIdle();
        cameraController.enabled = false;
    }

    public void GoMove()
    {
        playerMove.moveStop = false;

        cameraController.enabled = true;
    }

}
