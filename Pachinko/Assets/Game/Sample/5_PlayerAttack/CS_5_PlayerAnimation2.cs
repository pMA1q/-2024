using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_5_PlayerAnimation2 : MonoBehaviour
{
    Animator anim;
    float StartTime;

    int x = 0;

    CS_HpGuage mHpGuage;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        anim = GetComponent<Animator>();

        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime > 5.2f)
        {
            anim.SetBool("DamageFlag", true);


            if (Time.time - StartTime > 5.8f && x == 0)
            {
                //GĚHPQ[Wđ¸çˇ
                mHpGuage.PlayerHpDown();
                x =+1;
            }

            if (Time.time - StartTime > 6.5f)
            {
                anim.speed = 0;

                //oIšđmçšé
                GameObject rootObject = transform.root.gameObject;
                if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
                {
                    //1băÉođÁˇ
                    rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 2f);
                }
                Debug.Log("oIš");

            }  
        }



    }
}
