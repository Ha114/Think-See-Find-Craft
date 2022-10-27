using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneColor : MonoBehaviour
{
    public GameObject Player;
    public GameObject Stone;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("stone"))
        {
           
        }
    }


}
