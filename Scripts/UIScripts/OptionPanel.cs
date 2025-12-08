using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionPanel : MonoBehaviour
{
   
    public static OptionPanel instance;

    public bool panelmode = true;

    public GameObject button;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetResolution();
    }

    /// <summary>
    /// 해상도 고정 함수
    /// </summary>
    public void SetResolution()
    {
        int setWidth = 1920; // 화면 너비
        int setHeight = 1080; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, panelmode);
    }

    public void ChangeMode()
    {
        button.SetActive(!button.activeSelf);
        int setWidth = 1920; // 화면 너비
        int setHeight = 1080; // 화면 높이
        panelmode = !panelmode;
        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, !panelmode);


    }
 
}
