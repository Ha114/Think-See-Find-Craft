using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float health;
    public Slider slider;
    public Text text;
    GameObject player;

    [System.Obsolete]
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CollisionManager.evil_1 += updateHealth;
        CollisionManager.flame_enemy += updateHealth2;
        AI_enemy._Damage += updateHealth3;
        CollisionManager.follow_behind += updateHealth4;

    }

    public void Update()
    {
        slider.value = health;
        text.text = "Health : " + health;
    }


    public void SetHealth(float hp)
    {
        health = health + hp;
        if (health > 100)
        {
            float tmp = health;
            tmp -= 100f;
            health = health - tmp;
            Update();
        }
        else
        {
            Update();
        }
    }

    [System.Obsolete]
    void updateHealth() {
        UpdateH(30f);
    }

    void updateHealth2() {
        UpdateH(45f);
    }
    void updateHealth3()
    {
        UpdateH(1f);
    }
    void updateHealth4() {
        UpdateH(20f);
    }

    void UpdateH(float damage) {
        health = health - damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    [System.Obsolete]
    private void OnDisable()
    {
        CollisionManager.evil_1 -= updateHealth;
        CollisionManager.flame_enemy -= updateHealth2;
        AI_enemy._Damage -= updateHealth3;
        CollisionManager.follow_behind -= updateHealth4;
    }

}

