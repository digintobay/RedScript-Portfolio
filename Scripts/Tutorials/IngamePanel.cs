using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using TMPro.Examples;
using RichTextSubstringHelper;

public class IngamePanel : MonoBehaviour
{

 

    [Multiline]
    [SerializeField]
    public List<string> text;
    [SerializeField]
    public List<string> skip;
    [SerializeField]
    private TextMeshProUGUI textPanel;

    public PlayerMove playerMove;
    public GameObject mainCamera;

    private CameraController cameraController;

    public EnemyAI wolf;

    private State state = State.NotInitialized;

    enum State
    {
        NotInitialized,
        Playing,
        PlayingSkipping,
        Completed,
    }



    private void Awake()
    {
        cameraController = mainCamera.GetComponent<CameraController>();
        wolf = wolf.GetComponent<EnemyAI>();    
    }

    private void Update()
    {

         if ((Input.GetMouseButtonDown(0)))
         {
             Skip();
         }


        if (state == State.Completed)
        {
            textPanel.text = " ";
       
            playerMove.moveStop = false;
            cameraController.enabled = true;
            wolf.firstMeet = true;
            gameObject.SetActive(false);
       
           // TutorialsHouseManager.Instance.TutorialTextPanelOn();
        }
    }

    private void OnEnable()
    {
        playerMove.PlayerIdle();
        playerMove.moveStop = true;
        Debug.Log(playerMove.moveStop);
        cameraController.enabled = false;
        StartCoroutine(Tutorials());
    }



    IEnumerator Tutorials()
    {

        state = State.Playing;
        playerMove.PlayerIdle();

        for (int i = 0; i < text.Count; i += 1)
        {
            yield return PlayLine(text[i]);

        }
        state = State.Completed;


    }


    

    IEnumerator PlayLine(string text)
    {

        for (int i = 0; i < text.RichTextLength() + 1; i += 1)
        {
            yield return new WaitForSeconds(0.05f);
            if (state == State.PlayingSkipping)
            {
                textPanel.text = text;
                state = State.Playing;
                break;
            }
            textPanel.text = text.RichTextSubString(i);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 25; i += 1)
        {
            yield return new WaitForSeconds(0.1f);
            if (state == State.PlayingSkipping)
            {
                state = State.Playing;
                break;
            }
        }
    }

    public void Skip()
    {
        state = State.PlayingSkipping;
    }

    public IEnumerator WaitForComplete()
    {
        while (state != State.Completed)
        {
            yield return null;
        }
    }
}
