using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LastUI_Controller : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> mUIs; // 移動させるUIのリスト

    [SerializeField]
    private List<RectTransform> mGorlTtans; // ゴール位置のリスト

    [SerializeField]
    private float mTimer = 1f; // 移動にかける時間

    [SerializeField]
    private CS_LastAttackPlayer mLastAttack;// とどめ

    private List<Vector2> mStartPositions = new List<Vector2>(); // 開始位置リスト
    private List<Vector2> mGoalPositions = new List<Vector2>();  // ゴール位置リスト
    private float elapsedTime = 0.0f; // 経過時間
    private int index = 0; // 現在処理中のUIインデックス

    private CS_CommonData mData;

    // Start is called before the first frame update
    void Start()
    {
        if (mUIs.Count != mGorlTtans.Count)
        {
            Debug.LogError("mUIsとmGorlTtansの数が一致していません。");
            return;
        }

        // 開始位置とゴール位置を初期化
        for (int i = 0; i < mUIs.Count; i++)
        {
            mStartPositions.Add(mUIs[i].anchoredPosition);
            mGoalPositions.Add(mGorlTtans[i].anchoredPosition);
        }

        // 共通データを見つける
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動が完了したならV入賞待機処理
        if (index >= mUIs.Count)
        {
            InV_Spot();
            return;
        }
        elapsedTime += Time.deltaTime;

        // 経過時間の割合を計算
        float t = Mathf.Clamp01(elapsedTime / mTimer);

        // 現在の位置を線形補間で計算
        Vector2 newPosition = Vector2.Lerp(mStartPositions[index], mGoalPositions[index], t);
        mUIs[index].anchoredPosition = newPosition;

        // 移動が完了したら次のUIに進む
        if (t >= 1.0f)
        {
            elapsedTime = 0.0f; // 経過時間をリセット
            index++; // 次のUIへ
        }

        if (index >= mUIs.Count) { mData.V_SpotOpen(); }
    }

    void InV_Spot()
    {
        bool In_V = mData.RightAttaker.IsInV_Spot;
        if(In_V)
        {
            mLastAttack.StartLastAttack();
            Destroy(this.gameObject);
        }
    }
}
