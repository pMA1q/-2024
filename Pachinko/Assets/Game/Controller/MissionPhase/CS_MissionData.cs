using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MissionData : MonoBehaviour
{
    [SerializeField, Header("プレイヤーステータス")]
    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get{ return mPlayerStatus; } }


    /// <summary>--------------------------------------------------------------
    //準備フェーズの選択結果の保存、取得
    public enum MISSION_TYPE
    {
        COLLECT = 0,    //収集
        SUBJUGATION,//討伐
        TRAINING,   //トレーニング
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //ミッションの種類を設定、取得
    public MISSION_TYPE MissionType
    {
        set { mMisisonNumber = value; }
        get { return mMisisonNumber;  }
    }

    //無発展フラグ
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }
    //抽せんされたミッション番号を保存
    public void SaveMissionContents(int _count, int _contentNum)
    {
        mMissionContentsNums[_count] = _contentNum;
    }
    public int GetMissionContent(int _contentsNum)
    {
        return mMissionContentsNums[_contentsNum];
    }
    /// </summary>-----------------------------------------------------------------

    //選択方式演出の選択成功フラグ(項目番号:11,12,18,19)
    private bool isChoiceSuccess = false;
    //選択成功
    public void ChoiceSuccess()
    {
        isChoiceSuccess = true;
    }
    //選択フラグリセット
    public void ChoiceSuccessReset()
    {
        isChoiceSuccess = false;
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
        set { HighProbabEnemyMode = value; }
        get { return HighProbabEnemyMode; }
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
}
