using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    void Start()
    { }

    void Update()
    { }

    private void OnCollisionEnter(Collision collision) //�Ԃ�����������閽�ߕ��J�n
    {
        if (collision.gameObject.CompareTag("DestroyWall"))//����������Tagutukeru�Ƃ����^�O������I�u�W�F�N�g����Ł`�Ƃ��������̉�
        {
            Destroy(gameObject);//���̃Q�[���I�u�W�F�N�g�����ł�����
        }
    }
}