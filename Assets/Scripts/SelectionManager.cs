using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private Text PressE;
    private void Start()
    {
        PressE = GameObject.FindGameObjectWithTag("Press").GetComponent<Text>();
    }

    private void Update()
    {
     
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                PressE.text = "Press E";
            }
            else {
                PressE.text = "";
            }
           
        }
    
    }



}
