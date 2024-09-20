//---------------------------------
//���I�֐�
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_LotteryFunction : MonoBehaviour
{
    
    // �m�����I�֐�: ����������ɂƂ�A1/����̊m���œ������Ԃ�
    public static�@bool LotMain(int _denominator)
    {
        if (_denominator <= 0)
        {
            Debug.LogError("�����1�ȏ�ł���K�v������܂��B");
            return false;
        }

        // 0����(denominator - 1)�܂ł̃����_���Ȑ����𐶐����A���̒l��0�Ȃ�u������v
        int randomValue = UnityEngine.Random.Range(0, _denominator);
        return randomValue == 0;
    }

    //���o���I
    //�W�F�l���b�N�֐��ŔC�ӂ�Enum�^���烉���_���ɒl�𒊑I
    public static T LotPerformance<T>() where T : Enum
    {
        T[] enumValues = (T[])Enum.GetValues(typeof(T)); //Enum�̑S�Ă̒l��z��Ŏ擾
        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length); //�����_���C���f�b�N�X
        return enumValues[randomIndex]; //�����_���ɑI�΂ꂽEnum�̒l��Ԃ�
    }

    public static T LotPerformance<T>(List<float> _probabilities) where T : Enum
    {
        // Enum�̑S�Ă̒l��z��Ŏ擾
        T[] enumValues = (T[])Enum.GetValues(typeof(T));

        // �m���̐���Enum�̐�����v���Ă��邩�m�F
        if (_probabilities.Count != enumValues.Length)
        {
            Debug.LogError("�m���̐���Enum�̐�����v���Ă��܂���B");
            return default(T);
        }

        //�m���̍��Z�l��ݒ�
        float totalProbability = 0f;
        foreach (float probability in _probabilities)
        {
            totalProbability += probability;
        }
        // �����_���Ȓl�𐶐� (0�`�͈̔�)
        float randomValue = UnityEngine.Random.Range(0f, totalProbability);


        float cumulativeProbability = 0f;

        //Debug.Log("randomValue" + randomValue);

        //�m���ɏ]���Ē��I
        for (int i = 0; i < _probabilities.Count; i++)
        {
            cumulativeProbability += _probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return enumValues[i]; // �m���ɏ]���đI�΂ꂽEnum�̒l��Ԃ�
            }
        }

        // �t�H�[���o�b�N�Ƃ��āA�Ō��Enum�l��Ԃ�
        return default(T);
    }
}
