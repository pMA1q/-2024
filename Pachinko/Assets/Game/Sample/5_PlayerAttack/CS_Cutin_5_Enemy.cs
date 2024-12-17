using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Cutin_5_Enemy : MonoBehaviour
{
    float StartTime;
    public GameObject Cutin;

    int x = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - StartTime > 3.0f && x == 0)
        {
            GameObject go = GameObject.Instantiate(Cutin);
            go.transform.position = new Vector3(11000, 0, 0);
            go.transform.rotation.SetEulerRotation(new Vector3(0, 90, 0));
            x += 1;
        }

    }
}
