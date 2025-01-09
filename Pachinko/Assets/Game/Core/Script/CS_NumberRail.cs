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
    [SerializeField, Header("0から順に1の図柄から入れる(右は9から)")]
    private GameObject[] mNumberPatterns = new GameObject[9];

    [SerializeField, Header("0から順に1の図柄から入れる(右は9から)")]
    private bool isInversion = false;

    [SerializeField, Header("パネルオブジェクト")]
    private RectTransform mPanelRect;

    private float mSpeed = 10000;//流れるスピード

    private float mStopStartSpeed = 2000;//停止開始時のスピード

    private float mInterval = 420;//間隔

    private bool isVariation = false;

    private bool isStopStart = false;

    private int mStopNumber = 1;

    private float mNowAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
            CheckOutsidePanel(i, rTrans);//パネル外なら透明にする
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(!isVariation) { return; }

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
            CheckOutsidePanel(i, rTrans);//パネル外なら透明にする
        }

        //図柄の停止が開始されているなら停止処理開始
        if (isStopStart) { StopPatterns(); }
    }

    //パネル外に出たらAlpha値を0にする
    private void CheckOutsidePanel(int _index, RectTransform _rTrans)
    {
        
        // PanelのRectTransform情報を取得
        Rect panelRect = mPanelRect.rect;
        Vector3[] panelCorners = new Vector3[4];
        mPanelRect.GetWorldCorners(panelCorners);

        // Panelのbottom位置（ワールド座標）を取得
        float panelBottom = panelCorners[0].y; // 左下のy座標がbottom

        // オブジェクトの現在位置（ワールド座標）を取得
        Vector3 objectPosition = _rTrans.position;

        // オブジェクトのbottomがPanelのbottomより下かをチェック
        bool isBelow = objectPosition.y < panelBottom;

        // Alphaを更新
        Image img = mNumberPatterns[_index].GetComponent<Image>();
        if (img != null)
        {
            img.color = new Color(1, 1, 1, isBelow ? 0.0f : mNowAlpha);
        }
    }

   

    private void StopPatterns()
    {
        int stopPattern = mStopNumber - 1;//止まる図柄

        //反転フラグがあるなら停止番号と逆の番号にアクセスできるように設定
        if(isInversion){ stopPattern = 9 - stopPattern - 1; }

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
                CheckOutsidePanel(i, rTrans);//パネル外なら透明にする
            }

            //このレールの変動をストップさせる
            isStopStart = false;
            isVariation = false;
        }

    }

    //図柄の停止を開始
    public void StopStart(int _stopNum)
    {
        mStopNumber = _stopNum;
        ArrangePatterns(5);
        isStopStart = true;
    }
    public void StopStartRush(int _stopNum)
    {
        mStopNumber = _stopNum;
        Debug.Log("stopnum" + mStopNumber);
        ArrangePatterns(1);
        StopPatterns();
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


    //レールの回転スタート
    public void StartVariation()
    {
        StartCoroutine(StartVal());
    }
   

    //アルファ値を変える
    public void ChangeAlpha(float _alpha)
    {
        mNowAlpha = _alpha;
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
            mNumberPatterns[i].GetComponent<Image>().color = new Color(1, 1, 1, _alpha);
            CheckOutsidePanel(i, rTrans);//パネル外なら透明にする
        }
    }

    private IEnumerator StartVal()
    {
        
        //図柄を少し上げる
        float up = 100;
        float afterpos = mNumberPatterns[0].GetComponent<RectTransform>().localPosition.y + up;
       

        while(mNumberPatterns[0].GetComponent<RectTransform>().localPosition.y <= afterpos)
        {
            for (int i = 0; i < 9; i++)
            {
                RectTransform rectTransform = mNumberPatterns[i].GetComponent<RectTransform>();
                Vector3 numberPos = rectTransform.localPosition;
                numberPos.y += 150 * Time.deltaTime;
                rectTransform.localPosition = numberPos;
                CheckOutsidePanel(i, rectTransform);//パネル外なら透明にする
            }

            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f);

        //下に流れる変動開始
        isVariation = true;
    }

    //図柄の整列
    private void ArrangePatterns(int _bottomNum)
    {
        //num - 4 のインデックスを計算し、範囲外の場合に補正
        int startIdx = mStopNumber - _bottomNum;
        if (isInversion)
        {
            startIdx = 9 - mStopNumber - (_bottomNum-1);
        }
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
            CheckOutsidePanel(i, rectTransform);//パネル外なら透明にする
        }
    }
}
