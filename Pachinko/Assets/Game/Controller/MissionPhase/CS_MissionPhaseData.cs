using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MissionPhaseData : MonoBehaviour
{
    [SerializeField, Header("プレイヤーステータス")]
    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get{ return mPlayerStatus; } }


    /// <summary>--------------------------------------------------------------
    //準備フェーズの選択結果の保存、取得
    public enum MISSION_TYPE
    {
        TRAINING = 0,   //トレーニング(ステータス)
        COLLECT ,    //収集
        SUBJUGATION,//討伐
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //ミッションの種類を設定、取得
    public MISSION_TYPE MissionType
    {
        set { mMisisonNumber = value; }
        get { return mMisisonNumber;  }
    }

    private int[] mMissionContentsNums = new int[3];
    //抽せんされたミッション番号を保存
    public void SaveMissionContents(int _count, int _contentNum)
    {
        mMissionContentsNums[_count] = _contentNum;
    }
    public int GetMissionContent(int _contentsNum)
    {
        return mMissionContentsNums[_contentsNum];
    }

    //ステータスミッション番号
    readonly public int sColor = 0;//文字色
    readonly public int sCut = 1;//カットイン
    readonly public int sPreAttack = 2;//先制攻撃
    readonly public int sRev = 3;//復活
    readonly public int sEquipmentRank = 4;//装備ランク

    //イベントミッション番号
    readonly public int eSkill = 0;//スキル獲得

    //チケットミッション
    readonly public int tPreAttack = 0;//先制攻撃
    readonly public int tPreRev = 1;//復活

    /// </summary>-----------------------------------------------------------------


    //選択方式演出の選択成功フラグ(項目番号:11,12,18,19)
    private bool isChoiceSuccess = false;
    public bool GetChoiceSuccess() { return isChoiceSuccess; }
    //選択成功
    public void ChoiceSuccess(bool _success) 
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        bigCtrl.WaitChoice = false;
        isChoiceSuccess = _success; 
    }
    //選択フラグリセット
    public void ChoiceSuccessReset() { isChoiceSuccess = false; }

    //討伐数の合計
    private int mSubjugationSum = 0;
    //討伐数の設定、取得
    public int SubjugationSum
    {
        set { mSubjugationSum = value; }
        get { return mSubjugationSum; }
    }

    //１ミッションで討伐した敵の討伐数（項目番号:18,19）
    private int mSubjugation_OneMission = 0;
    //討伐数の設定、取得
    public int SubjugationOneMission
    {
        set { mSubjugation_OneMission = value; }
        get { return mSubjugation_OneMission; }
    }

    //プレイヤーバフフラグ(項目番号:20,21,29)
    public enum PLAYER_BUFF
    {
        NONE,       //無
        WEAK,       //弱
        STRONG,     //強
    }
    private PLAYER_BUFF mPlayerBuff = PLAYER_BUFF.NONE;
    //プレイヤーバフの種類を設定、取得
    public PLAYER_BUFF PlayerBuff
    {
        set { mPlayerBuff = value; }
        get { return mPlayerBuff; }
    }

    //敵のデバフフラグ(項目番号:22)
    private bool isEnemyDeBuff = false;
    //敵のデバフを設定、取得
    public bool IsEnemyDeBuff
    {
        set { isEnemyDeBuff = value; }
        get { return isEnemyDeBuff; }
    }

    //スキル取得フラグ(項目番号:23)
    private int mSkill = 0;
    //スキル数の設定、取得
    public int Skill
    {
        set { mSkill = value; }
        get { return mSkill; }
    }

    //敵遭遇高確モードフラグ(項目番号:26)
    private bool mHighProbabEnemyMode = false;
    //フラグの設定、取得
    public bool HighProbabEnemyMode
    {
        set { mHighProbabEnemyMode = value; }
        get { return mHighProbabEnemyMode; }
    }

    //ゲーム数延長数(項目番号:27)
    private int mExtensionGameCount = 0;
    //追加するゲームカウント数
    public int ExtensionGameCount
    {
        set { mExtensionGameCount = value; }
        get { return mExtensionGameCount; }
    }

    //報酬UPフラグ(項目番号:30)
    private bool mRewardUp = false;
    //フラグの設定、取得
    public bool RewardUp
    {
        set { mRewardUp = value; }
        get { return mRewardUp; }
    }


    //各フラグやデータをリセットする
    public void ResetMissionData()
    {
        isChoiceSuccess = false;
        mSubjugationSum = 0;
        mSubjugation_OneMission = 0;
        mPlayerBuff = PLAYER_BUFF.NONE;
        isEnemyDeBuff = false;
        mSkill = 0;
        mHighProbabEnemyMode = false;
        mExtensionGameCount = 0;
        mRewardUp = false;
    }

    private void Awake()
    {
        mPlayerStatus.backupStatus = new CSO_PlayerStatus(mPlayerStatus);
        this.GetComponent<CS_BossPhaseData>().Init();
    }

    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        mPlayerStatus.ResetStatus();
#endif
    }
}
