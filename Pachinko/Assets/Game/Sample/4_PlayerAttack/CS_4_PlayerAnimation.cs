using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_4_PlayerAnimation : MonoBehaviour
{
    Animator anim;
    float StartTime;

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
