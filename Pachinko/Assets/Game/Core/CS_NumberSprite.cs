//---------------------------------
//図柄の画像をセットする
//担当者：中島
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
    [SerializeField, Header("図柄を表示するImage")]
    private Image[] mNumberImage;

    [SerializeField, Header("図柄の画像")]
    private Sprite[] mNumberSprite;


    
    // 数字をセットするメソッド
    public void SetNumber(int _number, Pattern _pattern)
    {
        int pattern = (int)_pattern;
        if (_number >= 0 && _number <= 9)
        {
            mNumberImage[pattern].sprite = mNumberSprite[_number];  // 該当する数字の画像をセット
        }
        else
        {
            Debug.LogError("0から9の範囲内で数字を指定してください");
        }
    }
}
