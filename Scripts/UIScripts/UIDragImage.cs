using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIDragImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform dragTarget; // 드래그할 대상(UI Image)
    [SerializeField] private float minY = -200f;       // 내려갈 수 있는 최소 y
    [SerializeField] private float maxY = 0f;          // 올라올 수 있는 최대 y

  private GameObject optionPanel;

    private PlayerMove playerMove;

    private Vector2 offset;

    void Awake()
    {
        // "OptionCanvas"라는 이름의 오브젝트를 찾기
        GameObject optionCanvas = GameObject.Find("OptionCanvas");

        if (optionCanvas != null && optionCanvas.transform.childCount > 0)
        {
            // 첫 번째 자식 가져오기
            optionPanel = optionCanvas.transform.GetChild(0).gameObject;

            Debug.Log("첫 번째 자식을 성공적으로 가져왔습니다: " + optionPanel.name);
        }
        else
        {
            Debug.LogWarning("OptionCanvas를 찾을 수 없거나 자식이 없습니다!");
        }

    }

    public void GotoMain()
    {
        SceneManager.LoadScene("Main");

    }

    public void GotoExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // 클릭한 지점과 이미지 pivot 사이의 차이 저장
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragTarget.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPoint
        );
        offset = dragTarget.anchoredPosition - localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragTarget.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPoint))
        {
            Vector2 targetPos = localPoint + offset;

            // Y 값 제한 (딸려 내려가는 범위)
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

            dragTarget.anchoredPosition = new Vector2(dragTarget.anchoredPosition.x, targetPos.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // "OptionCanvas"라는 이름의 오브젝트를 찾기
        GameObject optionCanvas = GameObject.Find("OptionCanvas");
        GameObject player = GameObject.Find("Player"); 

        if (optionCanvas != null && optionCanvas.transform.childCount > 0)
        {
            // 첫 번째 자식 가져오기
            optionPanel = optionCanvas.transform.GetChild(0).gameObject;

            Debug.Log("첫 번째 자식을 성공적으로 가져왔습니다: " + optionPanel.name);
        }
        else
        {
            Debug.LogWarning("OptionCanvas를 찾을 수 없거나 자식이 없습니다!");
        }

        if (player != null)
        {
            playerMove = player.GetComponent<PlayerMove>();
            playerMove.moveStop = false;
        }
        

        dragTarget.anchoredPosition = new Vector2(dragTarget.anchoredPosition.x, maxY);
        optionPanel.SetActive(!optionPanel.activeSelf);
        // 환경 설정 팝업 띄워 주기
    }

    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }

    public void ExitButton()
    {
        GameObject player = GameObject.Find("Player");
    
        if (playerMove != null)
        {
            playerMove = player.GetComponent<PlayerMove>();

            playerMove.moveStop = false;
            optionPanel.SetActive(!optionPanel.activeSelf);

        }
        else if (playerMove == null)
        {
         
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
        else
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }

  

}