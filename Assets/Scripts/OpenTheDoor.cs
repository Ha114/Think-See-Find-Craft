using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    #region singleton
    public static OpenTheDoor _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    #endregion
    public bool isGateOpen = false;
}
