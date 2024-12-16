using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RightAttckerBallDestroy : MonoBehaviour
{
    [SerializeField]
    private CS_RightAttakerOpenClose mAttaker;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball"))
        {
            mAttaker.Prize++;
            CS_CommonData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
            data.Dedama += 15;
            Destroy(other.gameObject);

        }
    }
}
