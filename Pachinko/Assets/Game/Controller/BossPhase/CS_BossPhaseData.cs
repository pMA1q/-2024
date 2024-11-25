using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BossPhaseData : MonoBehaviour
{
    [SerializeField, Header("�{�X�X�e�[�^�X")]
    private CSO_BossStatus mBossList;
    public CSO_BossStatus BossStatus { get { return mBossList; } }

    private CSO_PlayerStatus mPlayerStatus;
    public CSO_PlayerStatus PlayerStatus { get { return mPlayerStatus; } }

    //�v���C���[HP�̃o�b�N�A�b�v
    private float mBackUpHP;
    //�o�b�N�A�b�vHP�̐ݒ�A�擾
    public float BackUpHP
    {
        set { mBackUpHP = value; }
        get { return mBackUpHP; }
    }

    //1�ϓ����̃v���C���[�̍U����
    private float mPlayerOneAttackPow;
    //�o�b�N�A�b�vHP�̐ݒ�A�擾
    public float PlayerOneAttackPow
    {
        set { mPlayerOneAttackPow = value; }
        get { return mPlayerOneAttackPow; }
    }

    //1�ϓ����̃v���C���[�̕���
    private bool isPlayerRevaival;
    //�����t���O�̐ݒ�A�擾
    public bool IsPlayerRevaival
    {
        set { isPlayerRevaival = value; }
        get { return isPlayerRevaival; }
    }

    //�m�萬���t���O
    private bool isConfirmationChoice;
    //�����t���O�̐ݒ�A�擾
    public bool IsConfirmationChoice
    {
        set { isConfirmationChoice = value; }
        get { return isConfirmationChoice; }
    }

    //�I�𐬌��t���O
    private bool isChoiceSuccess;
    //�����t���O�̐ݒ�A�擾
    public bool IsChoiceSuccess
    {
        set { isChoiceSuccess = value; }
        get { return isChoiceSuccess; }
    }

    //1�ϓ����̃v���C���[�̍U����
    private float mBossOneAttackPow;
    //�o�b�N�A�b�vHP�̐ݒ�A�擾
    public float BossOneAttackPow
    {
        set { mBossOneAttackPow = value; }
        get { return mBossOneAttackPow; }
    }

    //�{�X�ԍ�
    private int mBossNumber;
    //�{�X�ԍ��̐ݒ�A�擾
    public int BossNumber
    {
        set { mBossNumber = value; }
        get { return mBossNumber; }
    }
    
    //�{�X�����t���O
    private bool isSubjugation;
    //�{�X�����t���O�̐ݒ�A�擾
    public bool IsSubjugation
    {
        set { isSubjugation = value; }
        get { return isSubjugation; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mPlayerStatus = this.GetComponent<CS_MissionPhaseData>().PlayerStatus;
        mBackUpHP = mPlayerStatus.hp;
    }

    //�����W�t���O
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
