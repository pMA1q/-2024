//---------------------------------
//�����t�F�[�Y�i�ߓ�
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPheseController : MonoBehaviour
{
    //�����t�F�[�Y�̉��o���
    public enum SET_PERFORMANCE
    {
        VALUE1,
        VALUE2,
        VALUE3,
    }

    //�eEnum�ɑ΂���m��
    [SerializeField]
    List<float> mProbabilities = new List<float> { 50.0f/200.0f, 150f/200f, 20f/200f };

    //���o���I��������ۂ�
    private bool mPerformanceFinish = false;
    
//-------------------------------�C�x���g�n���h��----------------
    public delegate void Performance(SET_PERFORMANCE _performance);

    //���o�𗬂��g���K�[�C�x���g
    public static event Performance OnPlayPerformance;
//-------------------------------------------------------------

    int debugCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //�e�m����%�ɒ���
        for (int i = 0; i < mProbabilities.Count; i++)
        {
            mProbabilities[i] *= 100f;
            Debug.Log((SET_PERFORMANCE)i + "�̊m��" + mProbabilities[i] + "%");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���o���I����Ă��Ȃ��Ȃ�I��
        if (!mPerformanceFinish) { return; }

        //�ۗ��ʂ��g�p

        //���o���I
        SET_PERFORMANCE randomNumber = CS_LotteryFunction.LotPerformance<SET_PERFORMANCE>(mProbabilities);
        mPerformanceFinish = false;
        //���o�J�n�g���K�[��ON
        OnPlayPerformance(randomNumber);

    }

    private void CheckLottery()
    {
        if (debugCount < 10000)
        {
            SET_PERFORMANCE random = CS_LotteryFunction.LotPerformance<SET_PERFORMANCE>(mProbabilities);
            Debug.Log("�����_���ɑI�΂ꂽ�l: " + random);
            debugCount++;

            if (debugCount >= 10000)
            {
                Debug.Log("10000��I��");
            }
        }

    }

    //���o�I���֐�
    public void PerformanceFinish()
    {
        mPerformanceFinish = true;
    }
}
