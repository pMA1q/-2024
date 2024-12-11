using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_AttackChoice : MonoBehaviour
{
    [SerializeField,Header("選択Images")]
    private List<GameObject> mImages; // 色を移動させるCubeの配列

    [SerializeField, Header("選択中の演出プレハブ")]
    private GameObject　mChoiceTimePrefab;

    [SerializeField, Header("選択成功時の演出プレハブ")]
    private GameObject mChoiceSuccessPrefab;

    [SerializeField, Header("選択失敗時の演出プレハブ")]
    private GameObject mChoiceFailedPrefab;

    [SerializeField, Header("非選択時の色")]
    private Color mNoSelectColor;

    Button[] buttons;

    private GameObject mAfterChoicePrefab;//選択後の演出プレハブ

    private Color mSelectColor = new Color ( 1f, 1f, 1f, 1f);

    private int selectNum = 0;
    private int mSuccessNum;

    private static int successionNum = 0; //成功数

    //
    public static int SuccessionNum
    {
        get { return successionNum; }
    }

    private void Start()
    {
        // ボタンが押されたときのイベントを設定
        FindButtons();
        Init();
        successionNum = 0;
    }

    public void Init()
    {
        mChoiceTimePrefab.SetActive(true);
        if (GetComponent<CS_SetPositionPerfPos>()) { GetComponent<CS_SetPositionPerfPos>().Start(); }
        
        if (mAfterChoicePrefab != null) { Destroy(mAfterChoicePrefab); }
      
        if (mImages[0] != null)
        {
            var imageComponent = mImages[0].GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = mSelectColor;
            }
            else
            {
                Debug.LogError("Image コンポーネントが見つかりません！");
            }
        }
        else
        {
            Debug.LogError("mImages 配列が空、または mImages[0] が null です！");
        }
        mImages[0].GetComponent<Image>().color = mSelectColor;
        //0番目意外の画像を非選択時の色に変更
        for (int i = 1; i < mImages.Count; i++)
        {
            mImages[i].GetComponent<Image>().color = mNoSelectColor;
        }

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        for (int i = 0; i < mImages.Count; i++) { mImages[i].SetActive(true); }
        //選択成功番号の抽せん
        mSuccessNum = CS_LotteryFunction.LotNormalInt(mImages.Count);
        Debug.Log("成功" + mSuccessNum);
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find("ButtonCanvas").GetComponent<Canvas>();
        buttons = canvas.GetComponentsInChildren<Button>();

      
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(MoveLeft); }
            else if (buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(MoveRight); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(AttackDecision); }
        }
    }

    //右ボタン
    private void MoveRight()
    {
        // 最後のCubeの色を保存
        Color lastColors = mImages[mImages.Count - 1].GetComponent<Image>().color;

        // 色を隣のオブジェクトに移動させる
        for (int i = mImages.Count - 1; i > 0; i--)
        {
            Color currentMat = mImages[i - 1].GetComponent<Image>().color;
            mImages[i].GetComponent<Image>().color = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        mImages[0].GetComponent<Image>().color = lastColors;

        //選択番号加算
        selectNum++;
        //selectBackのリストの範囲を超えないようにする
        if (selectNum >= mImages.Count) { selectNum = 0; }
    }

    //左ボタン
    private void MoveLeft()
    {
        // 最後のCubeの色を保存
        Color lastColors = mImages[0].GetComponent<Image>().color;

        // 色を隣のオブジェクトに移動させる
        for (int i = 0; i < mImages.Count - 1; i++)
        {
            Color currentMat = mImages[i + 1].GetComponent<Image>().color;
            mImages[i].GetComponent<Image>().color = currentMat;
        }

        // 最初のCubeの色を最後のCubeの色に設定
        mImages[mImages.Count - 1].GetComponent<Image>().color = lastColors;

        selectNum--;

        //selectBackのリストの範囲を超えないようにする
        if (selectNum < 0) { selectNum = mImages.Count - 1; }
    }

    //Pushボタン
    private void AttackDecision()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        mChoiceTimePrefab.SetActive(false);
        //選ばれてないミッションは消す
        for (int i = 0; i < mImages.Count; i++)
        {
            if (i != selectNum)
            {
                mImages[i].SetActive(false);
            }
        }

        //BossPhaseDataを取得
        CS_BossPhaseData bossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        
        Transform parentObject = transform.root;
        //選択後の演出を生成
        if (selectNum == mSuccessNum)
        {
            //成功
            successionNum++;
            mAfterChoicePrefab = Instantiate(mChoiceSuccessPrefab, Vector3.zero, Quaternion.identity);
            if (successionNum < 3)//連続正解数が3
            {
                bossData.ChoiceSuccess(true);
            }
            else
            {
                bossData.ChoiceSuccess(false);
                Destroy(transform.root.gameObject);
            }
        }
        else
        {
            mAfterChoicePrefab = Instantiate(mChoiceFailedPrefab, Vector3.zero, Quaternion.identity);
            bossData.ChoiceSuccess(false);
            Destroy(transform.root.gameObject);
        }
    }
}
