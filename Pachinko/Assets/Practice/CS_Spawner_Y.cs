using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Spawner_Y : MonoBehaviour
{
    [SerializeField]
    CS_TestProbabilityStatus status;

    // Start is called before the first frame update
    void Start()
    {
        CS_SetPheseController.OnPlayPerformance += SpawnPerformance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPerformance(int _num)
    {
        //ÉvÉåÉnÉuê∂ê¨
        //Instantiate(status.performances[_num].performancePrefab, transform.position, transform.rotation);
    }

}
