//---------------------------------
//�����t�F�[�Y�i�ߓ�
//�S���ҁF����
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;  // LINQ���g�����߂ɕK�v

public class CS_SetPheseController : MonoBehaviour
{
   
    [SerializeField]
    CS_TestProbabilityStatus mProbabilityStatus;
    List<float> mProbabilities = new List<float>();

    //���o���I��������ۂ�
    private bool mPerformanceFinish = true;


    private int mPrizesNum = 3;//���ܐ�

    private CS_Controller mBigController;//�i�ߓ���

 //-----------------------�C�x���g�n���h��-----------------------
    public delegate void Performance(int _performance);

    //���o�𗬂��g���K�[�C�x���g
    public static event Performance OnPlayPerformance;
 //-------------------------------------------------------------



    int debugCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //�p�t�H�[�}���X���X�g����m�����R�s�[
        for (int i = 0; i < mProbabilityStatus.performances.Count; i++)
        {
            mProbabilities.Add(mProbabilityStatus.performances[i].value);
            Debug.Log(mProbabilityStatus.performances[i].name + "�̊m��" + mProbabilities[i] + "%");
        }

        mBigController = GameObject.Find("BigController").GetComponent<CS_Controller>();//�i�ߓ��i��j���擾
    }

    // Update is called once per frame
    void Update()
    {
        //CheckLottery();
        //���o���I����Ă��Ȃ��Ȃ�I��
        if (!mPerformanceFinish) { return; }

        //�c����ܐ���0�H
        if(mPrizesNum == 0)
        {
            //�~�b�V�����I���̏���������
            return;
        }

        //�ۗ��ʐ���0�Ȃ�I��
        if(mBigController.GetStock() == 0) { return; }

        //�ۗ��ʂ��g�p
        //mBigController.UseStock();

        mPrizesNum--;//���ܐ���1���炷

        //�~�b�V�������I
        //int randomNumber = CS_LotteryFunction.LotPerformance(mProbabilities);
        int randomNumber = CS_LotteryFunction.LotNormalInt(19);//0~19�̔ԍ����I
        mPerformanceFinish = false;//���o�I���t���O��false

        //���o�J�n�g���K�[��ON
        OnPlayPerformance(randomNumber);
    }



    private void CheckLottery()
    {
        if (debugCount < 10000)
        {
            int randomNumber = CS_LotteryFunction.LotPerformance(mProbabilities);
            Debug.Log("�����_���ɑI�΂ꂽ���o: " + mProbabilityStatus.performances[randomNumber].name);
            debugCount++;

            if (debugCount >= 10000)
            {
                Debug.Log("10000��I��");
            }
        }

    }

    //�V�[�����ɂ���I�u�W�F�N�g����CS_SetPheseController�������ĕԂ�
    public static CS_SetPheseController GetCtrl()
    {
        //�V�[�����̑S�Ă�GameObject���擾���āA���O��mPrefabName���܂ރI�u�W�F�N�g������
        GameObject targetObject = FindObjectsOfType<GameObject>()
            .FirstOrDefault(obj => obj.name.Contains("SetPhaseCtrl"));
        if (targetObject != null)
        {
            Debug.Log("�����t�F�[�Y�i�ߓ�����������");
            return targetObject.GetComponent<CS_SetPheseController>();
        }
        return null;
    }

    //���o�I���֐�
    public void PerformanceFinish()
    {
        mPerformanceFinish = true;
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
