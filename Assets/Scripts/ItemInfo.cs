using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;

    public void SetUp(string name, string description, int count)
    {
        if (count > 0)
        {
            itemName.text = name + " (" + count + ")";
        }
        else {
            itemName.text = name;
        }
        itemDescription.text = description;
    }


    public void SetUpEffect(string name, float time) {
        itemName.text = name;
        itemDescription.text = time.ToString();
    }
}
