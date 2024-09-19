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
    public enum SET_DIRECTING
    {
        VALUE1,
        VALUE2,
        VALUE3,
    }

    //�eEnum�ɑ΂���m��
    List<float> mProbabilities = new List<float> { 50.0f/200.0f, 150f/200f, 20f/200f };

    int debugCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //�e�m����%�ɒ���
        for (int i = 0; i < mProbabilities.Count; i++)
        {
            mProbabilities[i] *= 100f;
            Debug.Log((SET_DIRECTING)i + "�̊m��" + mProbabilities[i] + "%");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(debugCount < 10000)
        {
            SET_DIRECTING random = CS_LotteryFunction.LotDirecting<SET_DIRECTING>(mProbabilities);
            Debug.Log("�����_���ɑI�΂ꂽ�l: " + random);
            debugCount++;

            if(debugCount >= 10000)
            {
                Debug.Log("10000��I��");
            }
        }

    }
}
