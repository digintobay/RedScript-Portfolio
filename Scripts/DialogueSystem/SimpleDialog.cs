using RichTextSubstringHelper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleDialog : MonoBehaviour
{
    [Multiline]
    [SerializeField]
    public List<string> text;
    [SerializeField]
    TextMeshProUGUI uiText;

    private State state = State.NotInitialized;

    enum State
    {
        NotInitialized,
        Playing,
        PlayingSkipping,
        Completed,
    }


    IEnumerator Start()
    {

        state = State.Playing;
        yield return new WaitForSeconds(1.2f);


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
