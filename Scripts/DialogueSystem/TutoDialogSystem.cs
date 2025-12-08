using RichTextSubstringHelper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoDialogSystem : MonoBehaviour
{
    [Multiline]
    [SerializeField]
    public List<string> text;
    [SerializeField]
    public List<string> skip;
    [SerializeField]
    TextMeshProUGUI uiText;

    public PlayerMove playerMove;
    public GameObject mainCamera;

    public GameObject noWalkZoneObjects;

    private CameraController cameraController;

    private State state = State.NotInitialized;

    private bool done = false;

    private void Awake()
    {
        
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    enum State
    {
        NotInitialized,
        Playing,
        PlayingSkipping,
        Completed,
    }

    private void Update()
    {
       /* if ((Input.GetMouseButtonDown(0)))
        {
            Skip();
        }*/

        if (state == State.Completed && !done)
        {
            uiText.text = " ";
            StartCoroutine(MoveChange());
            done= true;
        }

    }

    private void OnEnable()
    {
        StartCoroutine(Tutorials());
    }

 

    IEnumerator Tutorials()
    {

        state = State.Playing;


        for (int i = 0; i < text.Count; i += 1)
        {
            yield return PlayLine(text[i]);

        }
        state = State.Completed;


    }

    IEnumerator MoveChange()
    {
        yield return new WaitForSeconds(3f);
        playerMove.moveStop = false;
        noWalkZoneObjects.SetActive(false);
        cameraController.enabled = true;
        QuestSystem.Instance.AxcMission("noax");

        yield break;
  

    }

    IEnumerator PlayLine(string text)
    {

        for (int i = 0; i < text.RichTextLength() + 1; i += 1)
        {
            yield return new WaitForSeconds(0.02f);
            if (state == State.PlayingSkipping)
            {
                uiText.text = text;
                state = State.Playing;
                break;
            }
            uiText.text = text.RichTextSubString(i);
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
