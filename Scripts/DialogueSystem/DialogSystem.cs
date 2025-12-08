using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RichTextSubstringHelper;
using UnityEngine.SceneManagement;


public class DialogSystem : MonoBehaviour
{
  
    [Multiline]
    [SerializeField]
    public List<string> text;
    [SerializeField]
    public Sprite[] sprites;
    [SerializeField]
    public UnityEngine.UI.Image image;
    [SerializeField]
    public List<string> skip;
    [SerializeField]
    TextMeshProUGUI uiText;

    public string sceneName;



    private State state = State.NotInitialized;

    enum State
    {
        NotInitialized,
        Playing,
        PlayingSkipping,
        Completed,
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0)))
        {
            Skip();
        }

        if (state == State.Completed)
        {
            StartCoroutine(ChangePart());
        }

    }

 

    IEnumerator ChangePart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Start()
    {
        
        state = State.Playing;
        yield return new WaitForSeconds(1.2f);
  

        for (int i = 0; i < text.Count; i += 1)
        {
            yield return PlayLine(text[i]);
            image.sprite = sprites[i];
          
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
