using System;
using UnityEngine;

public class InteractiveAX : MonoBehaviour
{
    [SerializeField] private float respawnTime = 5;
    [SerializeField] private GameObject sliderPrefab;      private Animator playerAnim;

    [Header("SFX 이펙트 효과음")]
    public AudioClip SFXcollet;


         private Action onDeactivateAction;

    public virtual void Awake()
    {
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
            SoundManager.instance.SFXPlay("Collect", SFXcollet);
            ItemGet();
            playerAnim.SetBool("Collect", false);
            onDeactivateAction?.Invoke();
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

        });
    }

    public virtual void ItemGet()
    {         InventoryManager.Instance.SetItemActive("ax", true);


    }

}
