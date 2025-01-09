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
    public static�@bool LotJackpot(int _denominator)
    {
        if (_denominator <= 0)
        {
            Debug.LogError("�����1�ȏ�ł���K�v������܂��B");
            return false;
        }

        Debug.Log("����" + _denominator);

        // 0����(denominator - 1)�܂ł̃����_���Ȑ����𐶐����A���̒l��0�Ȃ�u������v
        int randomValue = UnityEngine.Random.Range(0, _denominator);
        return randomValue == 0;
    }

    public static bool LotJackpotFloat(float _bigWinProbability)
    {
       
        float randomValue = UnityEngine.Random.Range(0.0f, 1.0f); // 0.0�ȏ�1.0�����̃����_���Ȓl
        return randomValue < _bigWinProbability;
    }

    //�}�������������
    public static int[] PatternLottery()
    {
        const int TotalValues = 65536;
   
        const int WinningValues = 10000;
        int[] i = { 7, 7, 7 };

        bool jp = LotJackpot((int)TotalValues / WinningValues);

        // �����蔻��
        if (jp)
        {
            Debug.Log("������I");
            int num = CS_LotteryFunction.LotNormalInt(8) + 1;
            i = new int[] { num, num, num };  // ������̂Ƃ��͌Œ�l
        }
        else
        {
            Debug.Log("�n�Y��");
            // �ʏ�̓����_���ɐ���
            i = new int[] { CS_LotteryFunction.LotNormalInt(8) + 1, CS_LotteryFunction.LotNormalInt(8) + 1, CS_LotteryFunction.LotNormalInt(8) + 1 };

            // ���[�`���� (���E�̐�������v���Ă��邩�ǂ���)
            if (i[0] == i[2])
            {
                Debug.Log("���[�`�����I");

                // �^�񒆂̐��������[�`����+1�ɕύX
                i[1] = (i[0] + 1) % 10;  // ���[�`�̐���+1�ɂ��āA�͈͂�1-9�ɐ���
            }

        }
        return i;
    }

    //���o���ʂ����Ƃɐ}�������߂�
    public static int[] PatternLottery2(WIN_LOST _win_lost)
    {
        
        int[] i = { 7, 7, 7 };

        int res = 1;
        switch(_win_lost)
        {
            case WIN_LOST.LOST:
            case WIN_LOST.RANDOM:
                //�O�̐������o���o���ɂȂ�܂Ń��[�v
                while(i[0] == i[2])
                {
                    // �ʏ�̓����_���ɐ���//0~8�Œ����񂵁A+1�����l�ɂ���
                    i = new int[] { CS_LotteryFunction.LotNormalInt(9) + 1, CS_LotteryFunction.LotNormalInt(9) + 1, CS_LotteryFunction.LotNormalInt(8) + 1 };
                }
                break;
            case WIN_LOST.SMALL_WIN:
                int[] evenNumbers = { 2, 4, 6, 8 };
                res = evenNumbers[CS_LotteryFunction.LotNormalInt(4)];
                // i��0~2��2, 4, 6, 8�̋����ԍ��ɂȂ�悤�ɒ�����
                for (int j = 0; j < 3; j++)
                {

                    i[j] = res;
                }
                break;

            case WIN_LOST.MIDDLE_WIN:
                int[] oddNumbers = { 1, 3, 5, 9 };
                res = oddNumbers[CS_LotteryFunction.LotNormalInt(4)];
                // i��0~2��1, 3, 5, 9�̊�ԍ��ɂȂ�悤�ɒ�����
                for (int j = 0; j < 3; j++)
                {

                    i[j] = res;
                }
                break;
        }
       // Debug.Log("���I"+_win_lost +"�}��" + i[0] + i[1] + i[2]);
        return i;
    }

    //�m�[�}�����I int�^�̔ԍ���Ԃ��B
    //�͈́F0~
    public static int LotNormalInt(int _max)
    {
        return UnityEngine.Random.Range(0, _max);
    }

    //�ݐόv�Z���g�����m�����I
    public static int LotPerformance(List<float> _probabilities)
    {
        //�m���̍��v���擾
        float totalProbability = 0f;
        foreach (float probability in _probabilities)
        {
            totalProbability += probability;
        }

        //totalProbability ��0�̂Ƃ��̓G���[��Ԃ�
        if (totalProbability == 0f)
        {
            Debug.LogError("�m���̍��v��0�ł��B");
            return -1; // �G���[�Ƃ��� -1 ��Ԃ�
        }

        //�����_���Ȓl�𐶐� (0�`totalProbability�͈̔�)
        float randomValue = UnityEngine.Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        //�m���ɏ]���Ē��I
        for (int i = 0; i < _probabilities.Count; i++)
        {
            cumulativeProbability += _probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                return i; // �m���ɏ]���đI�΂ꂽ���X�g�̃C���f�b�N�X��Ԃ�
            }
        }

        //�t�H�[���o�b�N�Ƃ��āA�Ō�̃C���f�b�N�X��Ԃ�
        return _probabilities.Count - 1;
    }

/*
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
*/

}
