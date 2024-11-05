using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heso : MonoBehaviour
{
    private const int TotalValues = 65536;
    //private const int WinningValues = 36553;
    private const int WinningValues = 10000;
    private CS_Controller csController;

    readonly int MAX_STOCK = 5;
    public List<int[]> stock = new List<int[]>();
    [SerializeField]
    GameObject[] stockObjects = null;

    // Start is called before the first frame update
    void Start()
    {
        // CS_Controller���A�^�b�`����Ă���I�u�W�F�N�g���擾
        GameObject controllerObject = GameObject.Find("BigController");

        // CS_Controller�R���|�[�l���g���擾
        if (controllerObject != null)
        {
            csController = controllerObject.GetComponent<CS_Controller>();
        }
        else
        {
            Debug.LogError("CS_ControllerObject��������܂���I");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���I���\�b�h
    // ���I���\�b�h
    void Lottery()
    {
        if (stock.Count < MAX_STOCK) // == ���� < �ɕύX�BMAX_STOCK�����̏ꍇ�ɒǉ�����
        {
            int randomValue = Random.Range(0, TotalValues);
            int[] i;

            // �����蔻��
            if (randomValue < WinningValues)
            {
                Debug.Log("������I");
                i = new int[] { 1, 1, 1 };  // ������̂Ƃ��͌Œ�l
            }
            else
            {
                Debug.Log("�n�Y��");
                // �ʏ�̓����_���ɐ���
                i = new int[] { Random.Range(1, 8), Random.Range(1, 8), Random.Range(1, 8) };

                // ���[�`���� (���E�̐�������v���Ă��邩�ǂ���)
                if (i[0] == i[2])
                {
                    Debug.Log("���[�`�����I");

                    // �^�񒆂̐��������[�`����+1�ɕύX
                    i[1] = (i[0] + 1) % 10;  // ���[�`�̐���+1�ɂ��āA�͈͂�1-9�ɐ���
                }
            }

            // �X�g�b�N�ɒǉ�
            stock.Add(i);
            Debug.Log("Stock added. Current stock: " + csController.GetStock());

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
            //Debug.Log("Ball��Plane��ʉ߂��܂���");
            // �R���[�`�������s����Ă��Ȃ��A����heso.stock.Count��0�ł͂Ȃ��ꍇ�A�R���[�`�����J�n����
            Lottery();
        }
    }
}
