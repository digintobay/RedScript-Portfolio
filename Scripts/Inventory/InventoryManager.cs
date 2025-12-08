using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

[System.Serializable]
public class ItemData
{
    public string itemName; public bool isActive; public int count;
    public ItemData(string name, bool active = false, int count = 0)
    {
        itemName = name;
        isActive = active;
        this.count = count;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<ItemData> inventory = new List<ItemData>();

    [Header("아이템 관련 변수들")]
    public GameObject potionUI;
    public TextMeshProUGUI potionText;
    public bool shield = false;
    public bool attackPower = false;
    public GameObject firefly00;
    public GameObject firefly01;


    public void AddItem(string itemName, bool isActive = false)
    {
        ItemData existingItem = inventory.Find(item => item.itemName == itemName);

        if (existingItem == null)
        {
            if (isActive)
                inventory.Add(new ItemData(itemName, true, 1));
        }
        else
        {
            if (isActive)
            {
                existingItem.count++;
                existingItem.isActive = true;
            }
            else
            {
                existingItem.count--;
                if (existingItem.count <= 0)
                {
                    inventory.Remove(existingItem);
                }
                else
                {
                    existingItem.isActive = true;
                }
            }
        }
    }

    public bool IsItemActive(string itemName)
    {
        ItemData item = inventory.Find(i => i.itemName == itemName);
        return item != null && item.isActive;

    }

    public void SetItemActive(string itemName, bool active)
    {
        ItemData item = inventory.Find(i => i.itemName == itemName);
        if (item != null)
        {
            item.isActive = active;
        }
    }

    public int GetItemCount(string itemName)
    {
        ItemData item = inventory.Find(i => i.itemName == itemName);
        return item != null ? item.count : 0;
    }

    public void PrintInventory()
    {
        foreach (var item in inventory)
        {
            Debug.Log($"{item.itemName} : {(item.isActive ? "Active" : "Inactive")} x{item.count}");
        }
    }

    public void FlyOn()
    {
        firefly00.SetActive(true);
    }

    public void Fly2On()
    {
        firefly01.SetActive(true);
    }
}