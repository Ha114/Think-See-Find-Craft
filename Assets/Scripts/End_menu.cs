using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class End_menu : MonoBehaviour
{
    void Start()
    {
        ChangeCursorState(false);
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

    public void GoToMenu()
    {

        SceneManager.LoadScene(0);

    }

    public void ExitGame()
    {
        Application.Quit();

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
