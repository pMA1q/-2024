using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossPhaseData : MonoBehaviour
{
    [SerializeField, Header("�{�X�X�e�[�^�X")]
    private CSO_BossStatus mBossList;
    //[SerializeField, Header("�{�X�X�e�[�^�XBackUP")]
    private CSO_BossStatus mBossListBackUp;
    public CSO_BossStatus BossStatus { get { return mBossList; } }

    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get { return mPlayerStatus; } }

    //�g�����`�P�b�g�̃t���O
    public enum USE_TIKET
    {
        NONE = 0,
        SPECOAL,
        PARTNER,
        PREEMPTIVE_ATTACK

    }
    private USE_TIKET mUseTiket;
    public USE_TIKET UseTiket
    {
        set { mUseTiket = value; }
        get { return mUseTiket; }
    }

    //1�ϓ����̃v���C���[�̍U����(���ڔԍ�:4,10,11,14,17,26)
    private float mPlayerOneAttackPow;
    //�U���ʂ̐ݒ�A�擾
    public float PlayerOneAttackPow
    {
        set { mPlayerOneAttackPow = value; }
        get { return mPlayerOneAttackPow; }
    }

    //1�ϓ����̃{�X�̍U����(���ڔԍ�:4,10,11,14,17,26)
    private float mBossOneAttackPow;
    //�U���ʂ̐ݒ�A�擾
    public float BossOneAttackPow
    {
        set { mBossOneAttackPow = value; }
        get { return mBossOneAttackPow; }
    }

    //1�ϓ����̃v���C���[�̕���(���ڔԍ�:5,6,7,8,9,23,30)
    private bool isPlayerRevaival = false;
    //�����t���O�̐ݒ�A�擾
    public bool IsPlayerRevaival
    {
        set { isPlayerRevaival = value; }
        get { return isPlayerRevaival; }
    }

    //�I��������o�̑I�𐬌��t���O(���ڔԍ�:14)
    private bool isChoiceSuccess = false;
    public bool GetChoiceSuccess() { return isChoiceSuccess; }
    //�I�𐬌�
    public void ChoiceSuccess(bool _success)
    {
        CS_Controller bigCtrl = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_Controller>();
        bigCtrl.WaitChoice = false;
        isChoiceSuccess = _success;
    }

    //�A���U����(���ڔԍ�:14)
    private int mSuccessionNum = 0;
    //�{�X�ԍ��̐ݒ�A�擾
    public int SuccessionNum
    {
        set { mSuccessionNum = value; }
        get { return mSuccessionNum; }
    }

    //�o�t�A�f�o�t�̃t���O(���ڔԍ�:18)
    public enum BUFF_DEBUFF
    {
        NONE,
        BUFF_SMALL,//�o�t(��)
        BUFF_BIG,//�o�t(��)
        DEBUFF//�f�o�t
    }
    //�{�X�̃o�t�A�f�o�t�t���O(���ڔԍ�:18)
    private BUFF_DEBUFF mBossBuff_Debuff = BUFF_DEBUFF.NONE;
    //�o�t�f�o�t�̐ݒ�A�擾
    public BUFF_DEBUFF BossBuff_Debuff
    {
        set { mBossBuff_Debuff = value; }
        get { return mBossBuff_Debuff; }
    }
    //�v���C���[�̃o�t�A�t���O(���ڔԍ�:20,21)
    private BUFF_DEBUFF mPlayerBuff_Debuff = BUFF_DEBUFF.NONE;
    public BUFF_DEBUFF PlayerBuff_Debuff
    {
        set { mPlayerBuff_Debuff = value; }
        get { return mPlayerBuff_Debuff; }
    }

    //����U�����_���[�WUP�t���O(���ڔԍ�:21,22)
    private bool isDamageUp = false;
    //�{�X�����t���O�̐ݒ�A�擾
    public bool IsDamageOneRankUp
    {
        set { isDamageUp = value; }
        get { return isDamageUp; }
    }

    //�m�萬���t���O(���ڔԍ�:25)
    private bool isConfirmationChoice;
    //�����t���O�̐ݒ�A�擾
    public bool IsConfirmationChoice
    {
        set { isConfirmationChoice = value; }
        get { return isConfirmationChoice; }
    }

    //�X�L�������t���O(���ڔԍ�:27)
    private bool isSkillStrong = false;
    //�����t���O�̐ݒ�A�擾
    public bool IsSkillStrong
    {
        set { isSkillStrong = value; }
        get { return isSkillStrong; }
    }

    //����U�������ԎQ��t���O(���ڔԍ�:29)
    private bool isPartnereJoin = false;
    //���ԎQ��t���O�̐ݒ�A�擾
    public bool IsPartnereJoin
    {
        set { isPartnereJoin = value; }
        get { return isPartnereJoin; }
    }

    //�v���C���[HP�̃o�b�N�A�b�v
    private float mBackUpHP;
    //�o�b�N�A�b�vHP�̐ݒ�A�擾
    public float BackUpHP
    {
        set { mBackUpHP = value; }
        get { return mBackUpHP; }
    }

    //�v���C���[�̍U���t���O
    private bool isPlayerAttack = false;
    //�v���C���[�̍U���t���O�̐ݒ�A�擾
    public bool IsPlayerAttack
    {
        set { isPlayerAttack = value; }
        get { return isPlayerAttack; }
    }

    //�{�X�̍U���t���O
    private bool isBossAttack = false;
    //�{�X�̍U���t���O�̐ݒ�A�擾
    public bool IsBossAttack
    {
        set { isBossAttack = value; }
        get { return isBossAttack; }
    }

    //�v���C���[�̂P�u���b�N�������HP��
    private int mPlayerOneBlockHp;
    //HP�ʂ̐ݒ�A�擾
    public int PlayerOneBlockHp
    {
        set { mPlayerOneBlockHp = value; }
        get { return mPlayerOneBlockHp; }
    }

    //�{�X�̂P�u���b�N�������HP��
    private int mBossOneBlockHp;
    //HP�ʂ̐ݒ�A�擾
    public int BossOneBlockHp
    {
        set { mBossOneBlockHp = value; }
        get { return mBossOneBlockHp; }
    }

    //�{�X�ԍ�
    private int mBossNumber = 0;
    //�{�X�ԍ��̐ݒ�A�擾
    public int BossNumber
    {
        set { mBossNumber = value; }
        get { return mBossNumber; }
    }
    
    //�{�X�����t���O
    private bool isSubjugation = false;
    //�{�X�����t���O�̐ݒ�A�擾
    public bool IsSubjugation
    {
        set { isSubjugation = value; }
        get { return isSubjugation; }
    }

    //�����t���O
    private bool isPlayerLose = false;
    //�����t���O�̐ݒ�A�擾
    public bool IsPlayerLose
    {
        set { isPlayerLose = value; }
        get { return isPlayerLose; }
    }


    public void Init()
    {
        mPlayerStatus = this.GetComponent<CS_MissionPhaseData>().PlayerStatus;
        if (mPlayerStatus.backupStatus == null) { Debug.Log("mPlayerStatus��null"); }
        mBackUpHP = mPlayerStatus.hp;
        mBossList.SaveInitialValues();
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
