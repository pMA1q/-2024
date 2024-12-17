using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_LeftAttakerOpenClose : MonoBehaviour
{
    [NonSerialized]
    public int Prize = 0;//入賞数

    [NonSerialized]
    public int RoundNum = 1;

    [NonSerialized]
    public bool IsAttakerEnable = false;

    [SerializeField]
    private float openRot;

    private bool IsAttackOpen = false;

    private int NowRound = 1;


    Quaternion defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.localRotation;
        //テスト
        //AttakerOpen(3);
    }

  
    public void AttakerOpen(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;

        StartCoroutine(NextRoundTimer());
        RoundNum = _round;
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(openRot, defaultRotation.y, defaultRotation.z);
        float timer = 0;

        while(timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15個入ってないなら終了
            else 
            {
                break; 
            }
        }

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.rotation = defaultRotation;
            yield break;
        }

        //次のラウンド
        StartCoroutine(NextRound());//次のラウンドへ
        yield break;
    }

    private IEnumerator NextRound()
    {
        this.transform.rotation = defaultRotation;

        yield return new WaitForSeconds(1f);
        //タイマー
        StartCoroutine(NextRoundTimer());
    }
}


