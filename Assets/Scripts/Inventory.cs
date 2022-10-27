using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate { };

    public List<Item> inventoryItemList = new List<Item>();
    public List<Item> craftingItemList = new List<Item>();

    public List<Item> hotbarItemList = new List<Item>();
    public HotbarController hotbarController;
    public GameObject inventorySlot;

    GameObject g;
    GameManager gm;

    public void SwitchHotbarInventory(Item item)
    {
        //inventory to hotbar, CHECK if we have enaugh space
        foreach (Item i in inventoryItemList)
        {
            if (i == item)
            {
                if (hotbarItemList.Count >= hotbarController.HotbarSlotSize)
                {
                    Debug.Log("No more slots available in hotbar");
                }
                else
                {
                    hotbarItemList.Add(item);
                    inventoryItemList.Remove(item);
                    onItemChange.Invoke();
                }
                return;
            }
        }

        //hotbar to inventory
        HotbarToInventory(item);
    }

    
    public void HotbarToInventory(Item item)
    {
        foreach (Item i in hotbarItemList)
        {
            if (i == item)
              {
                hotbarItemList.Remove(item);
                inventoryItemList.Add(item);
                onItemChange.Invoke();
                return;
           }
        }


    }

    public void AddItem(Item item)
    {
        inventoryItemList.Add(item);
        item.count_item = 1;
      //  Debug.Log("1 Name = " + item.name + ", count = " + item.count_item);
        onItemChange.Invoke();
    }

    public void AddStackebleItem(Item currentItem)
    {
        for (int i = 0; i < inventoryItemList.Count; i++) {
            if (inventoryItemList[i].name == currentItem.name)
            {
                inventoryItemList[i].count_item++;

               // Debug.Log("2 Name = " + inventoryItemList[i].name + ", count = " + inventoryItemList[i].count_item);
                onItemChange.Invoke();
                return;
            }
        }
        for (int i = 0; i < hotbarItemList.Count; i++)
        {
            if (hotbarItemList[i].name == currentItem.name)
            {
                hotbarItemList[i].count_item++;

                onItemChange.Invoke();
                return;
            }
        }

        AddItem(currentItem);
    }


    public bool Check(Item currentItem)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i].name == currentItem.name)
            {
                inventoryItemList[i].count_item++;

                return false;
            }
        }
        for (int i = 0; i < hotbarItemList.Count; i++)
        {
            if (hotbarItemList[i].name == currentItem.name)
            {
                hotbarItemList[i].count_item++;

                return true;
            }
        }
        return false;
    }


    public Item forMassage(Item item)
    {
        return item;
    }


    public void RemoveItem(Item item)
    {
        if (inventoryItemList.Contains(item))
        {
            CheckOnStatItem(item, inventoryItemList);
        }
        else if (hotbarItemList.Contains(item))
        {
            CheckOnStatItem(item, hotbarItemList);
        }

        onItemChange.Invoke();
    }
    public void RemoveItem2(Item item)
    {
        g = GameObject.Find("GameManager");
        gm = g.GetComponent<GameManager>();

        gm.BagSet(item);

        if (inventoryItemList.Contains(item))
        {
            CheckOnStatItem(item, inventoryItemList);
        }
        else if (hotbarItemList.Contains(item))
        {
            CheckOnStatItem(item, hotbarItemList);
        }

        onItemChange.Invoke();
    }

    public bool ContainsItem(string itemName, int amount)
    {
        int itemCounter = 0;

        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }
        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }

        if (itemCounter >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItems(string itemName, int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            RemoveItemType(itemName);
        }
    }
    public void RemoveItemType(string itemName)
    {
        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                if (i.count_item > 1)
                {
                    i.count_item -= 1;
                    return;
                }
                else {
                    inventoryItemList.Remove(i);
                    return;
                }
            }
        }

        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                if(i.count_item > 1)
                {
                    i.count_item -= 1;
                    return;
                } else {
                    hotbarItemList.Remove(i);
                    return;
                }
            }
        }
    }

    //check on by item
    void CheckOnStatItem(Item currentItem, List<Item> itemList) {
        foreach (Item i in itemList)
        {
            if (i.name == currentItem.name)
            {
                if (i.count_item > 1)
                {
                    i.count_item -= 1;
                    return;
                }
                else
                {
                    itemList.Remove(i);
                    return;
                }
            }
        }
    }
}
