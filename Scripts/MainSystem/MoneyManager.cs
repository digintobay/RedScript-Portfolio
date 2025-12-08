using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public int CurrentMoney = 500;      public TextMeshProUGUI storePanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpendMoney(int amount)
    {
        CurrentMoney -= amount;
        if (CurrentMoney < 0) CurrentMoney = 0;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        storePanel.text = CurrentMoney.ToString() + " G";
    }
}
