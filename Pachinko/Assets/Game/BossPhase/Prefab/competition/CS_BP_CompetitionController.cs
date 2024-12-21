using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_CompetitionController : MonoBehaviour
{
    private CS_BossPhaseData mBossData;
    private CSO_PlayerStatus mPlayerStatus;
    [SerializeField]
    List<GameObject> mTikets;

    [SerializeField, Header("非選択時の色")]
    private Color mNoSelectColor;

    List<int> mTiketNum = new List<int> { 0,0,0};
    List<int> mTiketNumbers = new List<int> { 1,2,3};

    private Color mSelectColor = new Color(1f, 1f, 1f, 1f);

    private int selectNum = 0;

    bool IsOnePush = false;
    Button[] buttons;

    public bool NoHaveTikets() { return mTikets.Count == 0; }
    // Start is called before the first frame update
    private void OnEnable()
    {
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        if (mBossData == null) { Debug.LogWarning("CS_BossPhaseDataが無い"); }
        mPlayerStatus = mBossData.PlayerStatus;
        mTiketNum[0] = mPlayerStatus.ticket.special;
        mTiketNum[1] = mPlayerStatus.ticket.partner;
        mTiketNum[2] = mPlayerStatus.ticket.preemptiveAttack;

        for (int i = 0; i < mTikets.Count; i++)
        {
            if (mTiketNum[i] <= 0)
            {
                //mTikets[i]とmTiketNumbers[i]を消し後ろを詰める
                Destroy(mTikets[i]);
                mTikets.RemoveAt(i);
                mTiketNum.RemoveAt(i);
                mTiketNumbers.RemoveAt(i);
                i --;
            }
            
        }
        Debug.Log("チケットカウント" + mTikets.Count);
        //0番目意外の画像を非選択時の色に変更
        for (int i = 1; i < mTikets.Count; i++)
        {
            mTikets[i].GetComponent<Image>().color = mNoSelectColor;
        }

        if (mTikets.Count <= 0)
        {
            mBossData.UseTiket = CS_BossPhaseData.USE_TIKET.NONE;
            StartCoroutine(AfterPush());
        }
        else
        {
            FindButtons();
            StartCoroutine(NoPush());
        }
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find(CS_CommonData.MainCanvasName).GetComponent<Canvas>();
        buttons = canvas.GetComponentsInChildren<Button>();


        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(LeftButton); }
            else if (buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(RightButton); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(PushButton); }
        }
    }

    //右ボタンが押されたときの処理
    private void RightButton()
    {
         // 最後のCubeの色を保存
        Color lastColors = mTikets[mTikets.Count - 1].GetComponent<Image>().color;

        // 色を隣のオブジェクトに移動させる
        for (int i = mTikets.Count - 1; i > 0; i--)
        {
            Color currentMat = mTikets[i - 1].GetComponent<Image>().color;
            mTikets[i].GetComponent<Image>().color = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        mTikets[0].GetComponent<Image>().color = lastColors;

        //選択番号加算
        selectNum++;
        //selectBackのリストの範囲を超えないようにする
        if (selectNum >= mTikets.Count) { selectNum = 0; }
    }

    //左ボタンが押されたときの処理
    private void LeftButton()
    {
        // 最後のCubeの色を保存
        Color lastColors = mTikets[0].GetComponent<Image>().color;

        // 色を隣のオブジェクトに移動させる
        for (int i = 0; i < mTikets.Count - 1; i++)
        {
            Color currentMat = mTikets[i + 1].GetComponent<Image>().color;
            mTikets[i].GetComponent<Image>().color = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        mTikets[mTikets.Count - 1].GetComponent<Image>().color = lastColors;
        selectNum--;
        //selectBackのリストの範囲を超えないようにする
        if (selectNum < 0) { selectNum = mTikets.Count - 1; }
    }

    //Pushボタンが押されたときの処理
    private void PushButton()
    {
        if (IsOnePush) { return; }
        IsOnePush = true;

        //ボタンを無効にする
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        //選んだもの意外を消す
        for (int i = 0; i < mTikets.Count; i++)
        {
            if (i != selectNum)
            {
                mTikets[i].SetActive(false);
            }
        }

        //選んだチケットの枚数を減らす
        mTiketNum[selectNum]--;
        //プレイヤーのチケットステータス更新
        switch(mTiketNumbers[selectNum])
        {
            case 1:
                mPlayerStatus.ticket.special = mTiketNum[selectNum];
                break;
            case 2:
                mPlayerStatus.ticket.partner = mTiketNum[selectNum];
                break;
            case 3:
                mPlayerStatus.ticket.preemptiveAttack = mTiketNum[selectNum];
                break;
        }

        //使ったチケットの種類を更新
        mBossData.UseTiket = (CS_BossPhaseData.USE_TIKET)mTiketNumbers[selectNum];

        StartCoroutine(AfterPush());
    }
    
    private IEnumerator NoPush()
    {
        float t = 0f;

        while(t <= 5f)
        {
            if(IsOnePush)
            {
                yield break;
            }

            t += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Pushボタンが押されなかったため強制選択");
        PushButton();

        yield return null;
    }

    private IEnumerator AfterPush()
    {

        yield return  new WaitForSeconds(2f);


        //Destroy(transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
