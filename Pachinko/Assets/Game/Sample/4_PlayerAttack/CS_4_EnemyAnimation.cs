using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_4_EnemyAnimation : MonoBehaviour
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
        if (Time.time - StartTime > 3.0f)
        {
            anim.SetBool("DamageFlag", true);

            if (Time.time - StartTime > 4.6f)
            {
                anim.speed = 0;
            }

        }
    }
}
