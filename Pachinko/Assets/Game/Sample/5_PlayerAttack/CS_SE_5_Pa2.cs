using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_SE_5_Pa2 : MonoBehaviour
{
    float StartTime;

    bool played;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - StartTime > 5.8f)
        {
            if (!played)
            {
                GetComponent<AudioSource>().Play();
                played = true;
            }
        }
        if (Time.time - StartTime > 6.8f)
        {
            if (played)
            {
                GetComponent<AudioSource>().Stop();
                played = false;
            }
        }

    }
}
