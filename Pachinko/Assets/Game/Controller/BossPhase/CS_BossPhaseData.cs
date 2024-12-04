using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossPhaseData : MonoBehaviour
{
    [SerializeField, Header("ボスステータス")]
    private CSO_BossStatus mBossList;
    [SerializeField, Header("ボスステータスBackUP")]
    private CSO_BossStatus mBossListBackUp;
    public CSO_BossStatus BossStatus { get { return mBossList; } }

    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get { return mPlayerStatus; } }

    //1変動時のプレイヤーの攻撃量(項目番号:4,10,11,14,17,26)
    private float mPlayerOneAttackPow;
    //攻撃量の設定、取得
    public float PlayerOneAttackPow
    {
        set { mPlayerOneAttackPow = value; }
        get { return mPlayerOneAttackPow; }
    }

    //1変動時のボスの攻撃量(項目番号:4,10,11,14,17,26)
    private float mBossOneAttackPow;
    //攻撃量の設定、取得
    public float BossOneAttackPow
    {
        set { mBossOneAttackPow = value; }
        get { return mBossOneAttackPow; }
    }

    //1変動時のプレイヤーの復活(項目番号:5,6,7,8,9,23,30)
    private bool isPlayerRevaival = false;
    //復活フラグの設定、取得
    public bool IsPlayerRevaival
    {
        set { isPlayerRevaival = value; }
        get { return isPlayerRevaival; }
    }

    //選択方式演出の選択成功フラグ(項目番号:14)
    private bool isChoiceSuccess = false;
    public bool GetChoiceSuccess() { return isChoiceSuccess; }
    //選択成功
    public void ChoiceSuccess(bool _success)
    {
        CS_Controller bigCtrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        bigCtrl.WaitChoice = false;
        isChoiceSuccess = _success;
    }

    //連続攻撃数(項目番号:14)
    private int mSuccessionNum = 0;
    //ボス番号の設定、取得
    public int SuccessionNum
    {
        set { mSuccessionNum = value; }
        get { return mSuccessionNum; }
    }

    //バフ、デバフのフラグ(項目番号:18)
    public enum BUFF_DEBUFF
    {
        NONE,
        BUFF_SMALL,//バフ(小)
        BUFF_BIG,//バフ(大)
        DEBUFF//デバフ
    }
    private BUFF_DEBUFF mBuff_Debuff = BUFF_DEBUFF.NONE;
    public BUFF_DEBUFF Buff_Debuff
    {
        set { mBuff_Debuff = value; }
        get { return mBuff_Debuff; }
    }

    //次回攻撃時ダメージUPフラグ(項目番号:21,22)
    private bool isDamageUp = false;
    //ボス討伐フラグの設定、取得
    public bool IsDamageOneRankUp
    {
        set { isSubjugation = value; }
        get { return isSubjugation; }
    }

    //確定成功フラグ(項目番号:25)
    private bool isConfirmationChoice;
    //復活フラグの設定、取得
    public bool IsConfirmationChoice
    {
        set { isConfirmationChoice = value; }
        get { return isConfirmationChoice; }
    }

    //スキル強化フラグ(項目番号:27)
    private bool isSkillStrong = false;
    //復活フラグの設定、取得
    public bool IsSkillStrong
    {
        set { isSkillStrong = value; }
        get { return isSkillStrong; }
    }

    //次回攻撃時仲間参戦フラグ(項目番号:29)
    private bool isPartnereJoin = false;
    //仲間参戦フラグの設定、取得
    public bool IsPartnereJoin
    {
        set { isPartnereJoin = value; }
        get { return isPartnereJoin; }
    }

    //プレイヤーHPのバックアップ
    private float mBackUpHP;
    //バックアップHPの設定、取得
    public float BackUpHP
    {
        set { mBackUpHP = value; }
        get { return mBackUpHP; }
    }

    //プレイヤーの１ブロック当たりのHP量
    private int mPlayerOneBlockHp;
    //HP量の設定、取得
    public int PlayerOneBlockHp
    {
        set { mPlayerOneBlockHp = value; }
        get { return mPlayerOneBlockHp; }
    }

    //ボスの１ブロック当たりのHP量
    private int mBossOneBlockHp;
    //HP量の設定、取得
    public int BossOneBlockHp
    {
        set { mBossOneBlockHp = value; }
        get { return mBossOneBlockHp; }
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


    public void Init()
    {
        mPlayerStatus = this.GetComponent<CS_MissionPhaseData>().PlayerStatus;
        if (mPlayerStatus.backupStatus == null) { Debug.Log("mPlayerStatusはnull"); }
        mBackUpHP = mPlayerStatus.hp;
        mBossList.SaveInitialValues();
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

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        mBossList.ResetToInitialValues();
#endif
    }

}
