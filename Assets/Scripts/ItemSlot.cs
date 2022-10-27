using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    public Image icon;
    private Item item;
    public Text _count;
    public bool isBeingDraged = false;

    public Item Item => item;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
    }

    public void UseItem()
    {
        if (item == null || isBeingDraged == true) return;

        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Trying to switch");
            Inventory.instance.SwitchHotbarInventory(item);
            GameManager.instance.DestroyItemInfo();
        }
        else
        {
            item.Use();
            GameManager.instance.DestroyItemInfo();
        }
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }

    public void OnRemoveButtonClicked()
    {
        if (item != null)
        {
            Inventory.instance.RemoveItem2(item);
            GameManager.instance.DestroyItemInfo();
        }
    }


    public void OnCursorEnter()
    {
        if (item == null || isBeingDraged == true) return;

        //display item info
        GameManager.instance.DisplayItemInfo(item.name, item.GetItemDescription(), item.count_item, transform.position);
    }

    public void OnCursorExit()
    {
        if (item == null) return;

        GameManager.instance.DestroyItemInfo();
    }
}