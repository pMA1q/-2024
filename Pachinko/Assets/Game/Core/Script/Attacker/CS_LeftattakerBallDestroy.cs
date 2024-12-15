using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftattakerBallDestroy : MonoBehaviour
{
    [SerializeField]
    private CS_LeftAttakerOpenClose leftAttaker;


    private void Start()
    {
        //�e�X�g
        leftAttaker.AttakerOpen(3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball"))
        {
            leftAttaker.Prize++;
            Destroy(other.gameObject);
        }
    }
}
