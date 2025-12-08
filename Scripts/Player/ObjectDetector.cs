using UnityEngine;
using TMPro;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textInteraction;
    public GameObject[] Player_IntercativeSprites;
    public GameObject Store_PanelUI;
    public StorePop storePop;

    private InteractiveObject interactable;
    private InteractiveAX interactableAX;
    private Animator playerAnim;

    public AudioClip storeSound;

    public PlayerMove playerMove;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || interactable != null || interactableAX != null )
        {
          

            if (interactable !=null)
            {
                
                interactable.Deactivate();
                interactable = null;

            }
            else if (interactableAX != null)
            {
                interactableAX.Deactivate();
                interactableAX = null;
            }
            
            
            if (storePop.StorePlayer == true)
            {
                SoundManager.instance.SFXPlay("AttackAX", storeSound);
                Store_PanelUI.SetActive(true);
                playerMove.moveStop = true;
            }
        }

        

    
    }


    private void Awake()
    {
        playerAnim = GameObject.Find("PlayerTextureSprite").gameObject.GetComponent<Animator>();
        for (int i = 0; i < Player_IntercativeSprites.Length - 1; i++)
        {
            Player_IntercativeSprites[i].SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {



        if (other.CompareTag("Item"))
        {

            int random = Random.Range(0, 2);
            Player_IntercativeSprites[random].SetActive(true);

            textInteraction.enabled = true;

            var firstChild = other.transform.GetChild(0).gameObject;
            interactable = firstChild.GetComponent<InteractiveObject>();

            // 여기서 콜백 등록!
            interactable.RegisterDetector(ForceExit);


            string name = other.name.Split('_')[0];
            textInteraction.text = $"{name}";
        }
        else if (other.CompareTag("OnewayItem"))
        {
            interactable = null;

            int random = Random.Range(0, 2);
            Player_IntercativeSprites[random].SetActive(true);

            textInteraction.enabled = true;

            string name = other.name.Split('_')[0];
            textInteraction.text = $"{name}";

        }
        else if (other.CompareTag("AxTemp"))
        {
            interactableAX = other.GetComponent<InteractiveAX>();
            interactableAX.RegisterDetector(ForceExit);
       


        }
        else if (other.CompareTag("Store"))
        {
           
            int random = Random.Range(0, 2);
            Player_IntercativeSprites[random].SetActive(true);

            textInteraction.enabled = true;

            string name = other.name.Split('_')[0];
            textInteraction.text = $"{name}";
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            interactable = null;

            var slider = GameObject.Find("HoldSlider(Clone)");
            Destroy(slider);
            playerAnim.SetBool("Collect", false);

            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                for (int i = 0; i <= Player_IntercativeSprites.Length - 1; i++)
                {
                    Player_IntercativeSprites[i].SetActive(false);
                }
                interactable = null;

            }

        }
        else if (other.CompareTag("OnewayItem"))
        {
            interactable = null;
            playerAnim.SetBool("Collect", false);

            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                for (int i = 0; i <= Player_IntercativeSprites.Length - 1; i++)
                {
                    Player_IntercativeSprites[i].SetActive(false);
                }
                interactable = null;

            }
        }
        else if (other.CompareTag("AxTemp"))
        {
            interactableAX = null;

            var slider = GameObject.Find("HoldSlider(Clone)");
            Destroy(slider);
            playerAnim.SetBool("Collect", false);

        }
        else if (other.CompareTag("Store"))
        {

            if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                for (int i = 0; i <= Player_IntercativeSprites.Length - 1; i++)
                {
                    Player_IntercativeSprites[i].SetActive(false);
                }
                interactable = null;

            }
        }


    }


    public void ForceExit()
    {
        interactableAX = null;
        interactable = null;
        var slider = GameObject.Find("HoldSlider(Clone)");
        Destroy(slider);

        if (textInteraction.enabled == true)
            {
                textInteraction.enabled = false;
                for (int i = 0; i <= Player_IntercativeSprites.Length - 1; i++)
                {
                    Player_IntercativeSprites[i].SetActive(false);
                }

                interactable = null;
            }
        
        
    }
}