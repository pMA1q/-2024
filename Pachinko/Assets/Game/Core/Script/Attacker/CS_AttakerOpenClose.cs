using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_AttakerOpenClose : MonoBehaviour
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

    Vector3 defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAttakerEnable && IsAttackOpen)
        {
            AddDedama();//出玉処理
        }
    }

    //出玉処理
    private void AddDedama()
    {
        if(Prize < 15) { return; }

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.eulerAngles = defaultRotation;
            StopCoroutine(NextRoundTimer());
            return;
        }

        StartCoroutine(NextRound());//次のラウンドへ
    }

    public void AttakerOpen(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;
        StartCoroutine(NextRound());
        StartCoroutine(NextRoundTimer());
        RoundNum = _round;
    }

    public void AttakerOpen_V_Bounus()
    {
        Prize = 0;
        IsAttakerEnable = true;
    }

    private void V_Spot_Update()
    {
        if(Prize >= 1)
        {
            IsInV_Spot = true;
            Prize = 0;
            this.transform.eulerAngles = defaultRotation;
        }
    }

    private IEnumerator NextRoundTimer()
    {
        yield return new WaitForSeconds(20f);
        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.eulerAngles = defaultRotation;
            yield break;
        }

        StartCoroutine(NextRound());//次のラウンドへ
    }

    private IEnumerator NextRound()
    {
        this.transform.eulerAngles = defaultRotation;

        yield return new WaitForSeconds(1f);

        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(openRot, defaultRotation.y, defaultRotation.z);
    }
}
