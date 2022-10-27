using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    public static event Action ladder_active_false;
    public static event Action apple_active;
    public static event Action evil_1;
    public static event Action flame_enemy;
    public static event Action follow_enemy_active;
    public static event Action follow_behind;

    Scene sceneLoaded;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("trigger1"))
        {
            apple_active?.Invoke();
            ladder_active_false?.Invoke();
            gameObject.SetActive(false);            
        }
        if (gameObject.CompareTag("trigger2"))
        {
            follow_enemy_active?.Invoke();
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Evil"))
        {
            evil_1?.Invoke();
        }
        if (gameObject.CompareTag("Flame"))
        {
            flame_enemy?.Invoke();
        }
        if (gameObject.CompareTag("behind1"))
        {
            SceneManager.LoadScene(3);
        }
      
    }

}
