using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Stock : MonoBehaviour
{
    readonly int MAX_STOCK = 5;
    private int mStockCount = 0;
    
    [SerializeField]
    GameObject[] stockObjects = null;

    public int Count
    {
        get
        {
            return mStockCount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    
    // �X�g�b�N�𖳌������郁�\�b�h
    public void DisableStock()
    {
        mStockCount--;

        // stockObjects�̔z��T�C�Y���`�F�b�N���ă��[�v
        for (int i = stockObjects.Length; i > 0; i--)
        {
            if (i - 1 >= 0 && stockObjects[i - 1].activeInHierarchy)
            {
                stockObjects[i - 1].SetActive(false); // �A�N�e�B�u�ȃI�u�W�F�N�g���A�N�e�B�u�ɂ���
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pachinko Ball"))
        {
            AddStock();
            Destroy(other.gameObject);
        }
            
    }

    public void AddStock()
    {
        if (mStockCount < MAX_STOCK) // == ���� < �ɕύX�BMAX_STOCK�����̏ꍇ�ɒǉ�����
        {
            mStockCount++;
            // ��A�N�e�B�u��stockObject���A�N�e�B�u�ɂ���
            foreach (var v in stockObjects)
            {
                if (!v.activeInHierarchy)
                {
                    v.SetActive(true); // ��A�N�e�B�u�ȃI�u�W�F�N�g���A�N�e�B�u�ɂ���
                    break;
                }
            }
        }
    }
}
