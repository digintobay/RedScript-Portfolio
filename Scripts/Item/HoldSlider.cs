using UnityEngine;
using UnityEngine.UI;
using System;

public class HoldSlider : MonoBehaviour
{
    public Image gaugeImage;
    public float fillDuration = 3f;  
    private bool isHolding = false;
    private Action onComplete;
    private float fillSpeed;

    private Animator playerAnim;

    [Header("SFX 이펙트 효과음")]
    public AudioClip SFXcollectStart;

    private void Awake()
    {
        playerAnim = GameObject.Find("PlayerTextureSprite").gameObject.GetComponent<Animator>();
    }

    public void Init(Action onComplete)
    {
               this.onComplete = onComplete;
        playerAnim.SetBool("Collect", false);
        gaugeImage.gameObject.SetActive(false);
        gaugeImage.fillAmount = 0f;          fillSpeed = 1f / fillDuration;      }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            playerAnim.SetBool("Collect", true);
            if (!isHolding)
            {
                isHolding = true;
                gaugeImage.gameObject.SetActive(true);
                gaugeImage.fillAmount = 0f;
            }

                         gaugeImage.fillAmount += fillSpeed * Time.deltaTime;

            if (gaugeImage.fillAmount >= 1f)
            {
                gaugeImage.fillAmount = 1f;
                gaugeImage.gameObject.SetActive(false);
                isHolding = false;

                onComplete?.Invoke();
                Destroy(gameObject);
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            playerAnim.SetBool("Collect", false);
            isHolding = false;
            gaugeImage.gameObject.SetActive(false);
            gaugeImage.fillAmount = 0f;
        }
    }
}
