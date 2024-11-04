using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_NumberRail : MonoBehaviour
{
    [SerializeField, Header("停止のポジション")]
    private RectTransform mStopPosition;
    [SerializeField, Header("下のポジション")]
    private RectTransform mBottom;
    [SerializeField, Header("0から順に1の図柄から入れる")]
    private GameObject[] mNumberPatterns = new GameObject[9];

    [SerializeField, Header("流れるスピード")]
    private float mSpeed = 300;
    [SerializeField, Header("流れるスピード（停止開始時）")]
    private float mStopStartSpeed = 2000;

    private float mInterval = 420;//間隔

    private float mVariationSecond = 8f;

    private bool isVariation = false;

    private bool isStopStart = false;

    private int mStopNumber = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartVariation();   
    }

    // Update is called once per frame
    void Update()
    {
        if(!isVariation) { return; }

        //テスト
        if (Input.GetKeyDown(KeyCode.Return)) { StopStart(); }

        float moveSpeed = mSpeed;
        if (isStopStart) { moveSpeed = mStopStartSpeed; }

        MovePatterns(moveSpeed);//図柄の移動
    }

    private void MovePatterns(float _speed)
    {
        //下に向かって図柄を移動
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();

            Vector3 newPos = rTrans.localPosition;
            newPos.y -= _speed * Time.deltaTime;

            rTrans.localPosition = newPos;

            CheckOverBottom(i, rTrans);
        }

        //図柄の停止が開始されているなら停止処理開始
        if (isStopStart) { StopPatterns(); }
    }

    private void StopPatterns()
    {
        int stopPattern = mStopNumber - 1;//止まる図柄

        //止まる図柄のポジション取得
        RectTransform stopPatternRec = mNumberPatterns[stopPattern].GetComponent<RectTransform>();
        Vector3 newStopPos = stopPatternRec.localPosition;

        //停止位置より下に行けば補正して停止
        if(newStopPos.y <= mStopPosition.localPosition.y)
        {
            float offset = Mathf.Abs(newStopPos.y - mStopPosition.localPosition.y);
            for (int i = 0; i < 9; i++)
            {
                RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
                Vector3 newPos = rTrans.localPosition;
                newPos.y += offset;
                rTrans.localPosition = newPos;
            }

            //このレールの変動をストップさせる
            isStopStart = false;
            isVariation = false;
        }

    }

    //図柄の停止を開始
    public void StopStart()
    {
        ArrangePatterns();
        isStopStart = true;
    }

    //BottomPosより下にあるならひとつ前のインデクス+Intervalのポジションにする
    private void CheckOverBottom(int _val, RectTransform _rRtrans)
    {
        Vector3 pos = _rRtrans.position;
        if( pos.y <= mBottom.position.y)
        {
            // numが0のときは最後の要素を参照するためのインデックスを計算
            int previousIndex = (_val == 0) ? mNumberPatterns.Length - 1 : _val - 1;

            // 前のオブジェクトのY座標を取得
            Vector3 previousPos = mNumberPatterns[previousIndex].GetComponent<RectTransform>().localPosition;

            // Y座標を420増やした位置に設定
            Vector3 newPos = previousPos;
            newPos.y += mInterval;

            _rRtrans.localPosition = newPos;
        }
    }
    //変動の時間をセット(秒)
    public void SetVariationSecond(float _second)
    {
        mVariationSecond = _second;
    }

    //レールの回転スタート
    public void StartVariation()
    {
        StartCoroutine(StartVal());
    }

    private IEnumerator StartVal()
    {
        //図柄を少し上げる
        float up = 100;
        for(int i = 0; i < 9; i++)
        {
            RectTransform rectTransform = mNumberPatterns[i].GetComponent<RectTransform>();
            Vector3 numberPos = rectTransform.localPosition;
            numberPos.y += 50;
            rectTransform.localPosition = numberPos;
        }
        yield return new WaitForSeconds(1f);

        //下に流れる変動開始
        isVariation = true;
    }

    private void ArrangePatterns()
    {
        //num - 4 のインデックスを計算し、範囲外の場合に補正
        int startIdx = mStopNumber - 5;
        if (startIdx < 0)
        {
            startIdx += mNumberPatterns.Length; // 負のインデックスを配列の後ろからに補正
        }
        else if (startIdx >= mNumberPatterns.Length)
        {
            startIdx %= mNumberPatterns.Length; // インデックスが配列の長さを超える場合に補正
        }

        // mBottom の位置に配置
        RectTransform startTransform = mNumberPatterns[startIdx].GetComponent<RectTransform>();
        startTransform.position = mBottom.position;

        // 他の図柄を mInterval 間隔で上に整列
        Vector3 newPos = startTransform.localPosition;
        for (int i = 1; i < mNumberPatterns.Length; i++)
        {
            int index = (startIdx + i) % mNumberPatterns.Length;
            newPos.y += mInterval;
            RectTransform rectTransform = mNumberPatterns[index].GetComponent<RectTransform>();
            rectTransform.localPosition = newPos;
        }
    }
}
