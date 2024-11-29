using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Extend : MonoBehaviour
{
    private int minGame = 1;
    private int maxGame = 3;

    private int remainGame = 10; //現在のゲーム数

    //UIの設定(ゲーム数表示)
    public Text remainGameText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRemaiGameUI();
    }

    //延長機能を呼び出す
    public void TriggerExtraGames()
    {
        int extraGame = Random.Range(minGame,maxGame + 1);
        remainGame += extraGame;
        Debug.Log("延長:" + remainGameText + "G追加されました！");

        UpdateRemaiGameUI();
    }

    //残りゲーム数を1減らし,0になった場合は延長を試行
    public void PlayGameRound()
    {
        if (remainGame > 0)
        {
            remainGame--;
            UpdateRemaiGameUI();

            if(remainGame  == 0)
            {
                //ゲーム終了時に延長を試行
                TriggerExtraGames();
            }
        }
        else
        {
            Debug.Log("ゲームオーバーです");
        }
    }

    // 残りゲーム数UIの更新
    void UpdateRemaiGameUI()
    {
        remainGameText.text = "残りG数: " + remainGame;
    }
}
