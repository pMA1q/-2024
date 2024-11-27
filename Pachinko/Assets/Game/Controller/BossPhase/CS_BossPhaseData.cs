using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossPhaseData : MonoBehaviour
{
    [SerializeField, Header("ボスステータス")]
    private CSO_BossStatus mBossList;
    public CSO_BossStatus BossStatus { get { return mBossList; } }

    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get { return mPlayerStatus; } }

    //プレイヤーHPのバックアップ
    private float mBackUpHP;
    //バックアップHPの設定、取得
    public float BackUpHP
    {
        set { mBackUpHP = value; }
        get { return mBackUpHP; }
    }

    //1変動時のプレイヤーの攻撃量
    private float mPlayerOneAttackPow;
    //バックアップHPの設定、取得
    public float PlayerOneAttackPow
    {
        set { mPlayerOneAttackPow = value; }
        get { return mPlayerOneAttackPow; }
    }

    //1変動時のプレイヤーの復活
    private bool isPlayerRevaival = false;
    //復活フラグの設定、取得
    public bool IsPlayerRevaival
    {
        set { isPlayerRevaival = value; }
        get { return isPlayerRevaival; }
    }

    //確定成功フラグ
    private bool isConfirmationChoice;
    //復活フラグの設定、取得
    public bool IsConfirmationChoice
    {
        set { isConfirmationChoice = value; }
        get { return isConfirmationChoice; }
    }

    //選択成功フラグ
    private bool isChoiceSuccess = false;
    //復活フラグの設定、取得
    public bool IsChoiceSuccess
    {
        set { isChoiceSuccess = value; }
        get { return isChoiceSuccess; }
    }

    //連続攻撃数
    private int mSuccessionNum = 0;
    //ボス番号の設定、取得
    public int SuccessionNum
    {
        set { mSuccessionNum = value; }
        get { return mSuccessionNum; }
    }


    //1変動時のプレイヤーの攻撃量
    private float mBossOneAttackPow;
    //バックアップHPの設定、取得
    public float BossOneAttackPow
    {
        set { mBossOneAttackPow = value; }
        get { return mBossOneAttackPow; }
    }

    //ボス番号
    private int mBossNumber = 0;
    //ボス番号の設定、取得
    public int BossNumber
    {
        set { mBossNumber = value; }
        get { return mBossNumber; }
    }
    
    //ボス討伐フラグ
    private bool isSubjugation = false;
    //ボス討伐フラグの設定、取得
    public bool IsSubjugation
    {
        set { isSubjugation = value; }
        get { return isSubjugation; }
    }

    //負けフラグ
    private bool isPlayerLose = false;
    //負けフラグの設定、取得
    public bool IsPlayerLose
    {
        set { isPlayerLose = value; }
        get { return isPlayerLose; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mPlayerStatus = this.GetComponent<CS_MissionPhaseData>().PlayerStatus;
        mBackUpHP = mPlayerStatus.hp;
    }

    //無発展フラグ
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }

    public void ResetData()
    {

    }

}
