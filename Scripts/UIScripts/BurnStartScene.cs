using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BurnStartScene : MonoBehaviour
{

    public string[] sceneName;
 
    public void GotoMain()
    {
        SceneChanger(sceneName[0]);

    }

    public void GotoExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void SceneChanger(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
