//---------------------------------
//�{�X�t�F�[�Y
//�S���ҁF���
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class CS_BossPheseController : MonoBehaviour
{
    //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);//�����F���ڔԍ�-1

    //�o�^���Ɏg�p
    public static event Performance OnPlayPerformance;
    //-------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }




    //�o�^����Ă���C�x���g�n���h�������ׂč폜
    public static void RemoveAllHandlers()
    {
        // OnPlayPerformance �ɉ�������̃n���h�����o�^����Ă���ꍇ
        if (OnPlayPerformance != null)
        {
            // OnPlayPerformance �ɓo�^����Ă���S�Ẵn���h�����擾
            Delegate[] handlers = OnPlayPerformance.GetInvocationList();

            // ���ׂẴn���h��������
            foreach (Delegate handler in handlers)
            {
                OnPlayPerformance -= (Performance)handler;
            }
        }
    }
}
