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
            return;
        }

        StartCoroutine(NextRound());//次のラウンドへ
    }

    public void AttakerOpen(int _round)
    {
        IsAttakerEnable = true;
        StartCoroutine(NextRound());
        RoundNum = _round;
    }

    private IEnumerator NextRound()
    {
        this.transform.eulerAngles = defaultRotation;

        yield return new WaitForSeconds(1f);

        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(openRot, 0f, 0f);
    }
}
