using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI_enemy : MonoBehaviour
{
    public Transform Pl;
    public float speed = 4f;
    Rigidbody rb;
    bool damage = false;
    public static event Action _Damage;

    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, Pl.position, speed * Time.deltaTime);

      

        rb.MovePosition(pos);
        transform.LookAt(Pl);
    }

   void Atak()
    {
        var dist = Vector3.Distance(transform.position, Pl.transform.position);
        if (dist <= 1 && damage == false)
        {
            //Debug.Log("DAMAGE");
            _Damage?.Invoke();
            damage = true;
            Invoke("recharge", 1);
        }
    }
    void recharge()
    {
        damage = false;
    }


    private void Update()
    {
        Atak();
    }
}
