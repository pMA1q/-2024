using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lottery : MonoBehaviour
{
    public GameObject plane;   // Plane�I�u�W�F�N�g
    public GameObject ball;    // Ball�I�u�W�F�N�g

    private const int TotalValues = 65536;   // �S�̂̒l
    private const int WinningValues = 656;   // ������͈̔�

    void Start()
    {
        // Plane��Ball���ݒ肳��Ă��Ȃ��ꍇ�A�R���|�[�l���g�������擾
        if (plane == null)
            plane = GameObject.Find("Plane");
        if (ball == null)
            ball = GameObject.Find("Ball");
    }

    // Ball��Plane�ƏՓ˂����Ƃ��̏���
    void OnCollisionEnter(Collision collision)
    {
        // Ball��Plane���Փ˂����ꍇ
        if (collision.gameObject == ball)
        {
            StartLottery();
        }
    }

    // ���I���J�n���郁�\�b�h
    void StartLottery()
    {
        // �����_���Ȓl�𐶐�
        int randomValue = Random.Range(0, TotalValues);

        // ���I���ʂ��o��
        if (randomValue < WinningValues)
        {
            Debug.Log("������I (1/99.9)");
        }
        else
        {
            Debug.Log("�n�Y��");
        }
    }
}

