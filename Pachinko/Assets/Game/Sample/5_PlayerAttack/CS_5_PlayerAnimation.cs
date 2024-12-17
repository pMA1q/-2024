using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_5_PlayerAnimation : MonoBehaviour
{
    Animator anim;
    float StartTime;

    int x = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime > 1.0f)
        {
            anim.SetBool("AttackFlag", true);

            if (Time.time - StartTime > 2.0f)
            {
                anim.speed = 0;
            }
               
        }

    }
}
