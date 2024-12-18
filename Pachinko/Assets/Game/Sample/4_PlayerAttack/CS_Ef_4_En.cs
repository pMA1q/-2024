using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ef_4_En : MonoBehaviour
{
    float StartTime;


    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - StartTime > 3.3f)
        {
            GetComponent<ParticleSystem>().Play();
        }
        if (Time.time - StartTime > 3.5f)
        {
            GetComponent<ParticleSystem>().Stop();
        }

    }
}
