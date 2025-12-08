using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] public string itemName; [SerializeField] public string uiMethod;
    [SerializeField] public float respawnTime = 5;
    [SerializeField] public GameObject sliderPrefab; [SerializeField] public GameObject anim;

    [Header("SFX 이펙트 효과음")]
    public AudioClip SFXcollet;

    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public GameObject parentObject;

    public Animator playerAnim;
    public bool firstCheck = false;

    public Action onDeactivateAction;

    public virtual void Awake()
    {
        parentObject = transform.parent.gameObject;
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        playerAnim = GameObject.Find("PlayerTextureSprite").gameObject.GetComponent<Animator>();
    }

    public void RegisterDetector(Action callback)
    {
        onDeactivateAction = callback;
    }


    public virtual void Deactivate()
    {
        GameObject uiPanel = GameObject.Find("UIPanel");
        if (uiPanel == null)
        {
            Debug.LogError("UIPanel 오브젝트를 찾을 수 없습니다!");
            return;
        }


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

        GameObject sliderInstance = Instantiate(sliderPrefab, uiPanel.transform);
        sliderInstance.transform.position = screenPos;
        HoldSlider holdSlider = sliderInstance.GetComponent<HoldSlider>();
        if (holdSlider == null)
        {
            Debug.LogError("슬라이더 프리팹에 HoldSlider 스크립트가 없습니다!");
            return;
        }

        holdSlider.Init(() =>
{
   playerAnim.SetBool("Collect", false);
   meshRenderer.enabled = false;
   meshCollider.enabled = false;
   anim.SetActive(false);
   parentObject.SetActive(false);
   onDeactivateAction?.Invoke();
   ItemGet();
   SoundManager.instance.SFXPlay("Collect", SFXcollet);
});
    }

    public virtual void ItemGet()
    {
        InventoryManager.Instance.AddItem(itemName, true);
        var method = typeof(InventoryPanel).GetMethod(uiMethod);
        method?.Invoke(InventoryPanel.Instance, null);

        if (itemName == "ax" && !firstCheck)
        {
            CombatPanelManager.Instance.PlayerHPPanelOn();
            CombatPanelManager.Instance.NormalOn();
            QuestSystem.Instance.AxcMission("putax"); firstCheck = true;
        }

        if (itemName == ("ham") ||
               itemName == ("bread") ||
               itemName == ("fruit") ||
               itemName == ("wine") ||
               itemName == ("cookie")
               )
        {
            int num = 1;
            QuestSystem.Instance.FoodMissionCheck(num);
        }
    }


    public System.Collections.IEnumerator RespawnProcess()
    {
        yield return new WaitForSeconds(respawnTime);

        meshRenderer.enabled = true;
        meshCollider.enabled = true;
    }

}
