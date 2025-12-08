using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutoQuestPanel : MonoBehaviour
{

  
    public PlayerMove playerMove;
    public GameObject mainCamera;

    private CameraController cameraController;


    private void Awake()
    {

        cameraController = mainCamera.GetComponent<CameraController>();
    }


    public void AxPopupExitTuto()
    {
        playerMove.moveStop = false;
        cameraController.enabled = true;
     
    }

    public void AxUsePopupTuto()
    {
        playerMove.moveStop = false;
        cameraController.enabled = true;
      
    }

}
