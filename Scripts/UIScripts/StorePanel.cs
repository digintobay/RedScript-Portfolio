using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    public TextMeshProUGUI storeText;
    private string[] itemNames = { "체력을 회복시켜주는 포션\r\n빨리 마시면 체할 수 있으니 천천히!",
        "몸을 지켜주는 티셔츠\r\n네버의 1호 팬이 되어보세요!",
        "공격력을 올려준다",
        "이 안에 무엇이 있을까요?\r\n자고로 선물이란 개봉 전까지 모르는 법!\r\n그렇지만 개봉도 구매 전까지 불가능한 거 아시죠?",
        "길을 밝혀주는 반딧불이\r\n길잡이는 아니지만 랜턴 정도는 되어줄 것 같다" };

    public TextMeshProUGUI moneyText;
    public Image storeImage;
    public Sprite[] sprites;
    public Sprite[] particles;
    public TextMeshProUGUI[] itemtexts = new TextMeshProUGUI[5];

    public Button buyButton;   // 구매 버튼
    public int[] itemPrices = { 3, 5, 9, 4, 2 }; // 각 아이템 가격

    private int currentIndex = -1;  // 현재 선택된 아이템 인덱스
    private bool stopCoroutine = false;
    private Sprite currentSprite; // storeImage 시작 시 저장

    public PlayerMove playerMove;
    public GameObject mainCamera;
    private CameraController cameraController;



    private Dictionary<string, Button> itemButtons = new Dictionary<string, Button>();

    private void Awake()
    {
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    private void OnEnable()
    {
        DontMove();
    }

    private void Start()
    {
 
        MoneyUIChanger();

        currentSprite = storeImage.sprite; // 초기 스프라이트 저장

        // 씬 안의 버튼들을 이름으로 자동 탐색 후 클릭 이벤트 연결
        BindButton("potion", 0);
        BindButton("shield", 1);
        BindButton("attackpower", 2);
        BindButton("forwolf", 3);
        BindButton("forGrandma", 4);

        // 구매 버튼 클릭 시
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        buyButton.interactable = false; // 시작 시 비활성화
    }


    public void DontMove()
    {
        playerMove.moveStop = true;
        playerMove.PlayerIdle();
        cameraController.enabled = false;
    }

    public void ExitStore()
    {
        playerMove.moveStop = false;
        cameraController.enabled = true;

    }

    // 이름으로 버튼 찾아서 클릭 이벤트 코드로 연결
    private void BindButton(string objName, int index)
    {
        GameObject obj = GameObject.Find(objName);
        if (obj == null)
        {
            Debug.LogWarning($"'{objName}' 오브젝트를 찾을 수 없음");
            return;
        }

        Button btn = obj.GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogWarning($"'{objName}' 오브젝트에 Button 컴포넌트가 없음");
            return;
        }

        itemButtons[objName] = btn;

        TextMeshProUGUI text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            if (index < itemtexts.Length)
                itemtexts[index] = text;
            else
                Debug.LogWarning($"itemtexts 배열 크기가 부족함 (index: {index})");
        }
        else
        {
            Debug.LogWarning($"'{objName}'의 첫 번째 자식에서 TextMeshProUGUI를 찾을 수 없음");
        }

        // 기존 리스너 제거 후 새로 연결
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => UpdateStoreUI(index));
    }

    private void UpdateStoreUI(int index)
    {
        if (index < 0 || index >= itemNames.Length || index >= sprites.Length)
        {
            Debug.LogWarning("잘못된 인덱스 접근");
            return;
        }

        currentIndex = index;
        string itemName = GetItemNameByIndex(index);

        // 선택한 아이템이 품절이면
        if (itemButtons[itemName] != null && !itemButtons[itemName].interactable)
        {
            storeText.text = "해당 상품은 더 이상 팔지 않습니다.";
            storeImage.sprite = currentSprite;
            buyButton.interactable = false;
        }
        else
        {
            // 품절되지 않은 아이템
            storeText.text = itemNames[index];
            storeImage.sprite = (index == 4) ? storeImage.sprite : sprites[index];
            buyButton.interactable = true; // 여기서 항상 활성화
        }

        if (index == 4)
        {
            stopCoroutine = false;
            StartCoroutine(UpdateParticleUI());
        }
        else
        {
            stopCoroutine = true;
        }

        Debug.Log($"{itemNames[index]} 선택됨");
    }

   

    IEnumerator UpdateParticleUI()
    {
    
        for (int i = 0; i < particles.Length; i++)
        {
            if (stopCoroutine)
                yield break;

            storeImage.sprite = particles[i];

            yield return new WaitForSeconds(0.3f);

            if (i == particles.Length-1)
            {
                i = 0;
            }

        }

    }

    //구매 버튼 눌렸을 때 처리
    private void OnBuyButtonClicked()
    {
        if (currentIndex == -1)
        {
            Debug.Log("아이템을 먼저 선택하세요.");
            return;
        }

        int price = itemPrices[currentIndex];
        string itemName = GetItemNameByIndex(currentIndex);

        // 돈 부족 시
        if (MoneyManager.Instance.CurrentMoney < price)
        {
            Debug.Log("돈이 부족합니다!");
            return;
        }


        // 현재 아이템 상태 확인
        ItemData existingItem = InventoryManager.Instance.inventory.Find(i => i.itemName == itemName);


        //구매 제한 처리
        if (itemName == "potion")
        {
            // 무제한 구매 가능
            MoneyManager.Instance.SpendMoney(price);
            InventoryManager.Instance.AddItem(itemName, true);
            MoneyUIChanger();
            InventoryManager.Instance.potionUI.SetActive(true);
            //포션 ui 개수 변경
            InventoryManager.Instance.potionText.text =
                InventoryManager.Instance.GetItemCount("potion").ToString();

            Debug.Log("포션 구매 완료!");
        }
        else if (itemName == "forGrandma")
        {
            if (existingItem != null && existingItem.count >= 2)
            {
                Debug.Log("forGrandma는 2번까지만 구매할 수 있습니다!");
                return;
            }

            MoneyManager.Instance.SpendMoney(price);
            MoneyUIChanger();
            InventoryManager.Instance.AddItem(itemName, true);
            InventoryManager.Instance.FlyOn();

        


            if (existingItem != null && existingItem.count >= 2)
            {
                InventoryManager.Instance.Fly2On();
                DisableItemButton(itemName);
            }

            Debug.Log("forGrandma 구매 완료!");


        }
        else if (itemName == "shield")
        {


            // 1회만 구매 가능
            if (existingItem != null)
            {
                Debug.Log("이미 구매한 아이템입니다!");
                return;
            }

            MoneyManager.Instance.SpendMoney(price);
            InventoryManager.Instance.AddItem(itemName, true);
            MoneyUIChanger();

            // 버튼 비활성화
            DisableItemButton(itemName);

            Debug.Log($"{itemName} 구매 완료!");

            InventoryManager.Instance.shield = true; // 방어구 추가
        }
        else if (itemName == "forwolf")
        {
            // 1회만 구매 가능
            if (existingItem != null)
            {
                Debug.Log("이미 구매한 아이템입니다!");
                return;
            }
            int num = 1;
            QuestSystem.Instance.FoodMissionCheck(num);
            MoneyManager.Instance.SpendMoney(price);
            InventoryManager.Instance.AddItem(itemName, true);
            MoneyUIChanger();

            // 버튼 비활성화
            DisableItemButton(itemName);

            Debug.Log($"{itemName} 구매 완료!");

          
        }
        else if (itemName== "attackpower")
        {
            // 1회만 구매 가능
            if (existingItem != null)
            {
                Debug.Log("이미 구매한 아이템입니다!");
                return;
            }

            MoneyManager.Instance.SpendMoney(price);
            InventoryManager.Instance.AddItem(itemName, true);
            MoneyUIChanger();

            // 버튼 비활성화
            DisableItemButton(itemName);

            Debug.Log($"{itemName} 구매 완료!");

            InventoryManager.Instance.attackPower = true; // 도끼 증강 추가

        }

        Debug.Log($"남은 돈: {MoneyManager.Instance.CurrentMoney}");
    }


    private void DisableItemButton(string itemName)
    {
        if (itemButtons.ContainsKey(itemName))
        {
            itemButtons[itemName].interactable = false;
            Debug.Log($"{itemName} 버튼 비활성화됨");
        }

        int index = -1;
        switch (itemName)
        {
            case "potion": index = 0; break;
            case "shield": index = 1; break;
            case "attackpower": index = 2; break;
            case "forwolf": index = 3; break;
            case "forGrandma": index = 4; break;
        }

        if (index >= 0 && index < itemtexts.Length && itemtexts[index] != null)
        {
            itemtexts[index].text = "품 절";
            itemtexts[index].color = new Color(0.8f, 0.8f, 0.8f);
        }

        storeText.text = "해당 상품은 더 이상 팔지 않습니다.";
        storeImage.sprite = currentSprite; // 원래 스프라이트로 되돌리기
        // 품절되면 구매 버튼 비활성화
        buyButton.interactable = false;
    }

    private string GetItemNameByIndex(int index)
    {
        switch (index)
        {
            case 0: return "potion";
            case 1: return "shield";
            case 2: return "attackpower";
            case 3: return "forwolf";
            case 4: return "forGrandma";
            default: return "";
        }
    }

    private void MoneyUIChanger()
    {
        moneyText.text = MoneyManager.Instance.CurrentMoney.ToString() + " G";
    }
}
