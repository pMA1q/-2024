//---------------------------------
//�}���̉摜���Z�b�g����
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_NumberSprite : MonoBehaviour
{
    public enum Pattern
    {
        LEFT,
        RIGHT,
        CENTER
    };
    [SerializeField, Header("�}����\������Image")]
    private Image[] mNumberImage;

    [SerializeField, Header("�}���̉摜")]
    private Sprite[] mNumberSprite;


    
    // �������Z�b�g���郁�\�b�h
    public void SetNumber(int _number, Pattern _pattern)
    {
        int pattern = (int)_pattern;
        if (_number >= 0 && _number <= 9)
        {
            mNumberImage[pattern].sprite = mNumberSprite[_number];  // �Y�����鐔���̉摜���Z�b�g
        }
        else
        {
            Debug.LogError("0����9�͈͓̔��Ő������w�肵�Ă�������");
        }
    }
}
