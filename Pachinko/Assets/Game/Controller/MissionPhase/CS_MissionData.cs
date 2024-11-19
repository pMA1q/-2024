using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MissionData : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�X�e�[�^�X")]
    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get{ return mPlayerStatus; } }


    /// <summary>--------------------------------------------------------------
    //�����t�F�[�Y�̑I�����ʂ̕ۑ��A�擾
    public enum MISSION_TYPE
    {
        COLLECT = 0,    //���W
        SUBJUGATION,//����
        TRAINING,   //�g���[�j���O
    }
    private MISSION_TYPE mMisisonNumber = MISSION_TYPE.COLLECT;
    
    //�~�b�V�����̎�ނ�ݒ�A�擾
    public MISSION_TYPE MissionNumber
    {
        set{ mMisisonNumber = value;}
        get{ return mMisisonNumber;}
    }

    //�����t�F�[�Y�őI�����ꂽ�~�b�V�������e�̔ԍ�
    private int[] mMissionContentsNums = new int[3];
    //�����񂳂ꂽ�~�b�V�����ԍ���ۑ�
    public void SaveMissionContents(int _count, int _contentNum)
    {
        mMissionContentsNums[_count] = _contentNum;
    }
    public int GetMissionContent(int _contentsNum)
    {
        return mMissionContentsNums[_contentsNum];
    }
    /// </summary>-----------------------------------------------------------------

    //�I��������o�̑I�𐬌��t���O(���ڔԍ�:11,12,18,19)
    private bool isChoiceSuccess = false;
    //�I�𐬌�
    public void ChoiceSuccess()
    {
        isChoiceSuccess = true;
    }
    //�I���t���O���Z�b�g
    public void ChoiceSuccessReset()
    {
        isChoiceSuccess = false;
    }

    //�v���C���[�o�t�t���O(���ڔԍ�:20,21,29)
    public enum PLAYER_BUFF
    {
        NONE,       //��
        WEAK,       //��
        STRONG,     //��
    }
    private PLAYER_BUFF mPlayerBuff = PLAYER_BUFF.NONE;
    //�v���C���[�o�t�̎�ނ�ݒ�A�擾
    public PLAYER_BUFF PlayerBuff
    {
        set { mPlayerBuff = value; }
        get { return mPlayerBuff; }
    }

    //�G�̃f�o�t�t���O(���ڔԍ�:22)
    private bool isEnemyDeBuff = false;
    //�G�̃f�o�t��ݒ�A�擾
    public bool IsEnemyDeBuff
    {
        set { isEnemyDeBuff = value; }
        get { return isEnemyDeBuff; }
    }

    //�X�L���擾�t���O(���ڔԍ�:23)
    private int mSkill = 0;
    //�X�L�����̐ݒ�A�擾
    public int Skill
    {
        set { mSkill = value; }
        get { return mSkill; }
    }

    //�G�������m���[�h�t���O(���ڔԍ�:26)
    private bool mHighProbabEnemyMode = false;
    //�t���O�̐ݒ�A�擾
    public bool HighProbabEnemyMode
    {
        set { HighProbabEnemyMode = value; }
        get { return HighProbabEnemyMode; }
    }

    //�Q�[����������(���ڔԍ�:27)
    private int mExtensionGameCount = 0;
    //�ǉ�����Q�[���J�E���g��
    public int ExtensionGameCount
    {
        set { mExtensionGameCount = value; }
        get { return mExtensionGameCount; }
    }

    //��VUP�t���O(���ڔԍ�:30)
    private bool mRewardUp = false;
    //�t���O�̐ݒ�A�擾
    public bool RewardUp
    {
        set { mRewardUp = value; }
        get { return mRewardUp; }
    }
}
