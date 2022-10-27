using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PotionEffect", menuName = "PotionEffect/baseEffect")]

public class PotionEffects : ScriptableObject
{
    new public string name = "Defulat Item";
    public Sprite icon = null;
    public int time;

    public virtual void Use()
    {
        Debug.Log("Start " + name);
    }
}
