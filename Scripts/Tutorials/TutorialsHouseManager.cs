using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialsHouseManager : MonoBehaviour
{
    public static TutorialsHouseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject houseTextPanel;
    public TextMeshPro text;

    public GameObject playerTutoTextPanel;
    public GameObject playerTutoText;
    public TextMeshPro ptext;


    [Header("튜토리얼 관련 오브젝트들")]
    [SerializeField] private GameObject IngameAxPanel;
    [SerializeField] private GameObject IngameEnemyPanel;


    public void IngameAXPanel()
    {
        IngameAxPanel.SetActive(true);

    }

   public void TutorialTextPanelOn()
    {
        Debug.Log("실행");
        houseTextPanel.SetActive(true);
        StartCoroutine(TutorialsTexts());

    }


    private IEnumerator TutorialsTexts()
    {
        houseTextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        houseTextPanel.SetActive(false);
        ptext.text = "도끼의 이름도 붙여주는 게 좋을까?";
        playerTutoText.SetActive(true);
        playerTutoTextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerTutoText.SetActive(false);
        playerTutoTextPanel.SetActive(false);

        text.text = "친구가 있다는 건 정말 든든한 일이지.";
        houseTextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        text.text = "그렇지만 빨간 망토야… 이거 하나를 기억하렴.";
        yield return new WaitForSeconds(3f);
        text.text = "어떤 문제는 스스로 맞닥뜨려야 한단다.";
        houseTextPanel.SetActive(false);
        ptext.text = "세상 사는 게 녹록지 않네….";
        playerTutoText.SetActive(true);
        playerTutoTextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerTutoText.SetActive(false);
        playerTutoTextPanel.SetActive(false);
    }
}
