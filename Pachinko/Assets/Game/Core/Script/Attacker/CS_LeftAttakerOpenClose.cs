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

    private float mMoveSpeed = 480f;

    Vector3 defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.eulerAngles;
        Debug.Log("初期位置" + defaultRotation);
        //テスト
        //AttakerOpen(3);
    }

  
    public void AttakerOpen(int _round)
    {
        if (!IsAttakerEnable)
        {
           
            IsAttakerEnable = true;
            StartCoroutine(NextRoundTimer());
            RoundNum = _round;
        }
        else 
        {
            RoundNum += _round; 
        }
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        Prize = 0;
        StartCoroutine(AttakerMove(openRot));//解放位置まで回転させる
        float timer = 0;

        while(timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15個入ってないなら終了
            else 
            {
                yield break; 
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
            StartCoroutine(AttakerMove(defaultRotation.x));//初期位置まで回転させる
            yield break;
        }

        //次のラウンド
        StartCoroutine(NextRound());//次のラウンドへ
        yield break;
    }

    private IEnumerator AttakerMove(float _targetRot)
    {
        // 現在のz軸の回転角度を取得
        float currentXRotation = transform.eulerAngles.x;
        // 回転がopenrotまで到達するまで回転を続ける
        while (Mathf.Abs(currentXRotation - _targetRot) > 0.1f)
        {
            // 回転を計算
            float step = mMoveSpeed * Time.deltaTime; // フレームごとの回転量
            float targetX = Mathf.MoveTowards(currentXRotation, _targetRot, step);

            // 回転を適用
            transform.eulerAngles = new Vector3(targetX, transform.eulerAngles.y, transform.eulerAngles.z);

            // 現在の角度を更新
            currentXRotation = transform.eulerAngles.x;

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的に目標角度にスナップ
        transform.eulerAngles = new Vector3(_targetRot, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private IEnumerator NextRound()
    {
        StartCoroutine(AttakerMove(defaultRotation.x));//初期位置まで回転させる
        Debug.Log("アタッカーが閉まりました");

        yield return new WaitForSeconds(1f);
        //タイマー
        StartCoroutine(NextRoundTimer());
    }
}


