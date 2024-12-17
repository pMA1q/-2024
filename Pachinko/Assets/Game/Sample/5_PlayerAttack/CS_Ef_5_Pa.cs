using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Ef_5_Pa : MonoBehaviour
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

        if (Time.time - StartTime > 1.6f)
        {
            GetComponent<ParticleSystem>().Play();
        }
        if (Time.time - StartTime > 1.8f)
        {
            GetComponent<ParticleSystem>().Stop();
        }

    }
}
