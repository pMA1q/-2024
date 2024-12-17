using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_RightAttakerOpenClose : MonoBehaviour
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

    public bool IsInV_Spot = false;
    public bool IsInV_Open = false;

    Quaternion defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.localRotation;
        Debug.Log("初期位置" + this.transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttakerEnable && IsAttackOpen)
        {
            if (IsAttackOpen) 
            {
                //AddDedama();//出玉処理
            }
            if(IsInV_Open)
            {
                V_Spot_Update();
            }
        }
    }

    //出玉処理
    private void AddDedama()
    {
        if (Prize < 15) { return; }

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        StopCoroutine(NextRoundTimer());
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.rotation = defaultRotation;
            return;
        }

        StartCoroutine(NextRound());//次のラウンドへ
    }

    public void AttakerOpen(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;

        RoundNum = _round;
        StartCoroutine(NextRoundTimer());
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(defaultRotation.x, defaultRotation.y, openRot);
        float timer = 0;

        while (timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15個入ってないなら終了
            else
            {
                Debug.Log("15個数入りました");
                break;
            }
        }

        if (timer >= 20) { Debug.Log("20秒経ちました"); }

        Debug.Log("次のラウンドへ移行します");

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

    public void AttakerOpen_V_Bounus()
    {
        Prize = 0;
        IsAttakerEnable = true;
        IsInV_Open = true;
    }

    private void V_Spot_Update()
    {
        if (Prize >= 1)
        {
            IsInV_Spot = true;
            IsInV_Open = false;
            Prize = 0;
            this.transform.rotation = defaultRotation;
        }
    }
}
