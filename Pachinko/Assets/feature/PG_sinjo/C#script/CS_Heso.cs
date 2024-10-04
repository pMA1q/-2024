using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Heso : MonoBehaviour
{
    private const int TotalValues = 65536;
    private const int WinningValues = 6553;

    readonly int MAX_STOCK = 4;
    public List<int[]> stock = new List<int[]>();
    [SerializeField]
    GameObject[] stockObjects = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���I���\�b�h
    void Lottery()
    {
        if (stock.Count < MAX_STOCK) // == ���� < �ɕύX�BMAX_STOCK�����̏ꍇ�ɒǉ�����
        {
            int randomValue = Random.Range(0, TotalValues);
            // �����蔻��
            if (randomValue < WinningValues)
            {
                Debug.Log("������I");
                var i = new int[] { 1, 1, 1, };
                stock.Add(i);
            }
            else
            {
                Debug.Log("�n�Y��");
                var i = new int[] { Random.Range(1, 9), Random.Range(1, 9), Random.Range(1, 9), };
                stock.Add(i);
            }
            

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

    // �X�g�b�N�𖳌������郁�\�b�h
    public void DisableStock()
    {
        //if (stock.Count > 0) // �X�g�b�N������ꍇ�̂ݍ폜
        //{
        //    stock.RemoveAt(0);
        //}
        //else
        //{
        //    Debug.LogWarning("�X�g�b�N����ł�");
        //}

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
            Debug.Log("Ball��Plane��ʉ߂��܂���");
            // �R���[�`�������s����Ă��Ȃ��A����heso.stock.Count��0�ł͂Ȃ��ꍇ�A�R���[�`�����J�n����
            Lottery();
        }
    }
}
