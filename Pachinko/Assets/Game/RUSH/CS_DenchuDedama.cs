using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_DenchuDedama : MonoBehaviour
{
    [SerializeField]
    CS_Stock mStock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball"))
        {
            CS_CommonData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
            data.Dedama += 1;
            mStock.AddStock();
            Destroy(other.gameObject);
        }
    }
}
