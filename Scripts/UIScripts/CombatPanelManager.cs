using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPanelManager : MonoBehaviour
{
    public static CombatPanelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public GameObject[] faces;

    public GameObject PlayerHPPanel;
    public GameObject EnemyHPPanel;


    public void PlayerHPPanelOn()
    {
        PlayerHPPanel.SetActive(true);  
    }

    public void EnemyHPPanelOn()
    {
        EnemyHPPanel.SetActive(true);
        Debug.Log("실행");
    }

    public void EnemyHPPanelOff()
    {
        EnemyHPPanel.SetActive(false);
        Debug.Log("실행2");
    }


    public void NormalOn()
    {
        FaceALLOff();
        faces[0].SetActive(true);
    }

    public void MiddleOn()
    {
        FaceALLOff();
        faces[1].SetActive(true);
    }

    public void KillerOn()
    {
        FaceALLOff();
        faces[2].SetActive(true);
    }

    public void FaceALLOff()
    {
        for (int i=0; i<3; i++)
        {
            faces[i].SetActive(false);
        }
    }
}
