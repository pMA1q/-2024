using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftattakerBallDestroy : MonoBehaviour
{
    [SerializeField]
    private CS_LeftAttakerOpenClose mAttaker;

    private void Start()
    {
        //ƒeƒXƒg
        //mAttaker.AttakerOpen(3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball"))
        {
            mAttaker.Prize++;
            CS_CommonData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
            data.Dedama += 3;
            Destroy(other.gameObject);

        }
    }
}
