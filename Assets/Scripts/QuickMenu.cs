using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMenu : MonoBehaviour
{
    public GameObject OptionsPanel;

    public void Go_to_menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        OptionsPanel.SetActive(false);
        ChangeCursorState(true);
    }

    void Start()
    {
        OptionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    private void ChangeCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
