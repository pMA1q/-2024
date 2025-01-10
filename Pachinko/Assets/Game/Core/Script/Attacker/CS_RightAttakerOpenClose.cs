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

    [NonSerialized]
    public bool IsInV_Spot = false;
    [NonSerialized]
    public bool IsInV_Open = false;

    private float mMoveSpeed = 480f;

    Vector3 defaultRotation;

    GameObject mBigCtrl;
    CS_BonusPhaseData mBdata;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.eulerAngles;
        mBigCtrl = GameObject.Find(CS_CommonData.BigControllerName);
        mBdata = mBigCtrl.GetComponent<CS_BonusPhaseData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttakerEnable)
        {
            
            if(IsInV_Open)
            {
                V_Spot_Update();
            }
        }
    }

    
    public void AttakerStart(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;

        RoundNum = _round;
        StartCoroutine(NextRoundTimer());
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        StartCoroutine(AttakerMove(openRot));//解放位置まで回転させる
        Prize = 0;
        float timer = 0;

        while (timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15個入ってないなら終了
            else
            {
                break;
            }
        }

        if (timer >= 20) { Debug.Log("20秒経ちました"); }

        Debug.Log("次のラウンドへ移行します");

        Prize = 0;
        NowRound++;
        CS_Controller.PACHINKO_PHESE nowPhase = mBigCtrl.GetComponent<CS_Controller>().GetPhese();
        if(nowPhase == CS_Controller.PACHINKO_PHESE.BOUNUS)
        {
            mBdata.RoundCount = NowRound;
        }
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            if (nowPhase == CS_Controller.PACHINKO_PHESE.BOUNUS)
            {
                mBdata.IsBonusFinish = true;
            }
            IsAttakerEnable = false;
            NowRound = 0;
            StartCoroutine(AttakerMove(defaultRotation.z));//初期位置まで回転させる
            yield break;
        }

        //次のラウンド
        StartCoroutine(NextRound());//次のラウンドへ
        yield break;
    }

    private IEnumerator AttakerMove(float _targetRot)
    {
        // 現在のz軸の回転角度を取得
        float currentZRotation = transform.eulerAngles.z;
        // 回転がopenrotまで到達するまで回転を続ける
        while (Mathf.Abs(currentZRotation - _targetRot) > 0.1f)
        {
            // 回転を計算
            float step = mMoveSpeed * Time.deltaTime; // フレームごとの回転量
            float targetZ = Mathf.MoveTowards(currentZRotation, _targetRot, step);

            // 回転を適用
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetZ);

            // 現在の角度を更新
            currentZRotation = transform.eulerAngles.z;

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的に目標角度にスナップ
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, _targetRot);
    }

    private IEnumerator NextRound()
    {
        StartCoroutine(AttakerMove(defaultRotation.z));//初期位置まで回転させる

        yield return new WaitForSeconds(1f);

        //タイマー
        StartCoroutine(NextRoundTimer());
    }

    public void AttakerStart_V_Bounus()
    {
        Prize = 0;
        StartCoroutine(AttakerMove(openRot));//解放位置まで回転させる
        IsAttakerEnable = true;
        IsInV_Open = true;
        IsInV_Spot = false;
    }

    private void V_Spot_Update()
    {
        Debug.Log("V入賞待ち");
        if (Prize >= 1)
        {
            IsInV_Spot = true;
            IsInV_Open = false;
            IsAttakerEnable = false;
            Prize = 0;
            StartCoroutine(AttakerMove(defaultRotation.z));//初期位置まで回転させる
        }
    }
}
