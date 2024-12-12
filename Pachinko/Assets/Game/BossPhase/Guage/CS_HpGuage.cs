using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGuage : MonoBehaviour
{
    [SerializeField, Header("�v���C���[HP")]
    private Image[] mPlayerHpBlocks = new Image[10];
    [SerializeField, Header("�GHP")]
    private Image[] mBossHpBlocks = new Image[10];


    private CS_BossPhaseData mBossData;

    private float mNowHp;
    private float mGoalHp;
    private float mMaxPlayerHp;
    private float mMaxBossHp;

    private Coroutine mPlayerGageCol = null;
    private Coroutine mBossGageCol = null;

    private int mPlayerOneBlockHp = 1;
    private int mBossOneBlockHp = 1;

    public string pefName;

    bool mGuageDownUpdateFinish = true;
    bool mGuageRevUpdateFinish = true;

    public bool HpDownUpdateFinish { get { return mGuageDownUpdateFinish; } }
    public bool HpRevaivalUpdateFinish { get { return mGuageRevUpdateFinish; } }

    public bool HpUpdateFinish { get { return mGuageDownUpdateFinish && mGuageRevUpdateFinish; } }

    public Text testTx; 

    private void Start()
    {
        //Init();
        //Invoke("LateStart", 0.5f);
    }
    // Start is called before the first frame update
    public void Init()
    {
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        if(mBossData.PlayerStatus==null) { Debug.Log("PlayerStatus��null�ł�"); }
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        Debug.Log("�v���C���[HP" + mMaxPlayerHp);
        mPlayerOneBlockHp = (int)mMaxPlayerHp / 10;
        mBossData.PlayerOneBlockHp = mPlayerOneBlockHp;
        mMaxBossHp = mBossData.BossStatus.infomations[mBossData.BossNumber].hp;
        mBossOneBlockHp = (int)mMaxBossHp / 10;
        mBossData.BossOneBlockHp = mBossOneBlockHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mBossData) { return; }
        testTx.text = "";
        //if(mBossData.IsPlayerRevaival)
        //{
        //    testTx.text = pefName + "��������";
        //}
    }

    //�v���C���[��HP�Q�[�W�����炷
    public void PlayerHpDown()
    {
        mBossData.IsBossAttack = true;
        mGuageDownUpdateFinish = false;
        StartCoroutine(PlayerSubGuage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerSubGage()); }

    }

    //�{�X��HP�Q�[�W�����炷
    public void BossHpDown()
    {

        mBossData.IsPlayerAttack = true;
        mGuageDownUpdateFinish = false;
        // if (mBossGageCol == null) { mBossGageCol = StartCoroutine(BossSubGage()); }
        mBossGageCol = StartCoroutine(BossSubGuage());
    }

    public void PlayerHpRevival()
    {
        
        mPlayerGageCol = StartCoroutine(PlayerRevaivalGuage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerRevaivalGage()); }

    }

    public void PlayerHpHeal()
    {

        mPlayerGageCol = StartCoroutine(PlayerHealGage());
        //if (mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerRevaivalGage()); }

    }

    private IEnumerator PlayerSubGuage()
    {
        Debug.Log("�v���C���[�̗͌���");
        int count = 0; // �A�N�e�B�u�𖳌��ɂ�������J�E���g
        float attackPow = Mathf.Ceil(mBossData.BossOneAttackPow / (float)mPlayerOneBlockHp);
        
        int deleteNum = (int)attackPow;

        Debug.Log("������:" + deleteNum + "�{�X�U����:" + mBossData.BossOneAttackPow + "�v���C��1�u���b�NHP" + mPlayerOneBlockHp);
        for (int i = mPlayerHpBlocks.Length�@-1; i >= 0; i--)
        {
            // �A�N�e�B�u���܂� num �𖳌������Ă��Ȃ��ꍇ
            if (mPlayerHpBlocks[i].gameObject.activeSelf && count < deleteNum)
            {
                mPlayerHpBlocks[i].gameObject.SetActive(false);
                count++; // �������J�E���g�𑝂₷
                yield return new WaitForSeconds(0.05f);
            }
            
        }
        int leftoverHp = 0;//�c��HP��
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            if (mPlayerHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        Debug.Log("�c��hp" + leftoverHp);

        if(!mBossData.IsPlayerRevaival && leftoverHp == 0) { mBossData.IsPlayerLose = true; }
        if (mBossData.IsPlayerRevaival)
        {
            Debug.Log("����");
            mGuageRevUpdateFinish = false; 
        }

        mBossData.PlayerStatus.hp = leftoverHp;
        mPlayerGageCol = null;
        mGuageDownUpdateFinish = true;
       
        yield return null;
    }
    private IEnumerator PlayerRevaivalGuage()
    {
        Debug.Log("�v���C���[�̗͉񕜁i�����j");
        int count = 0; // �A�N�e�B�u�𖳌��ɂ�������J�E���g
        int revCount = (int)mBossData.BackUpHP - mBossData.PlayerStatus.hp;
        Debug.Log("�񕜗�" + revCount);
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            // �A�N�e�B�u���܂� num �𖳌������Ă��Ȃ��ꍇ
            if (!mPlayerHpBlocks[i].gameObject.activeSelf && count < revCount)
            {
                mPlayerHpBlocks[i].gameObject.SetActive(true);
                count++; // �������J�E���g�𑝂₷
                yield return new WaitForSeconds(0.05f);
            }

        }
        int leftoverHp = 0;//�c��HP��
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            if (mPlayerHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        mBossData.PlayerStatus.hp = leftoverHp;
        mBossData.IsPlayerRevaival = false;
        mPlayerGageCol = null;
        mGuageRevUpdateFinish = true;
        yield return null;
    }

    private IEnumerator PlayerHealGage()
    {
        Debug.Log("�v���C���[�̗͉�");
        int count = 0; // �A�N�e�B�u�𖳌��ɂ�������J�E���g
        int revCount = (int)Mathf.Ceil((mMaxPlayerHp * 0.3f));
        Debug.Log("�񕜗�" + revCount);
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            // �A�N�e�B�u���܂� num �𖳌������Ă��Ȃ��ꍇ
            if (!mPlayerHpBlocks[i].gameObject.activeSelf && count < revCount)
            {
                mPlayerHpBlocks[i].gameObject.SetActive(true);
                count++; // �������J�E���g�𑝂₷
                yield return new WaitForSeconds(0.05f);
            }

        }
        int leftoverHp = 0;//�c��HP��
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
        {
            if (mPlayerHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        mBossData.PlayerStatus.hp = leftoverHp;
        mBossData.IsPlayerRevaival = false;
        mPlayerGageCol = null;
        mGuageRevUpdateFinish = true;
        yield return null;
    }

    private IEnumerator BossSubGuage()
    {
        Debug.Log("�{�X�̗͌���");
        int count = 0; // �A�N�e�B�u�𖳌��ɂ�������J�E���g
        float attackPow = Mathf.Ceil(mBossData.PlayerOneAttackPow / mBossOneBlockHp);

        int deleteNum = (int)attackPow;
        Debug.Log("������:" + deleteNum + "�v���C���U����:" + mBossData.PlayerOneAttackPow + "�{�X1�u���b�NHP" + mBossOneBlockHp);
        for (int i = 0; i < mBossHpBlocks.Length; i++)
        {
            // �A�N�e�B�u���܂� num �𖳌������Ă��Ȃ��ꍇ
            if (mBossHpBlocks[i].gameObject.activeSelf && count < deleteNum)
            {
                mBossHpBlocks[i].gameObject.SetActive(false);
                count++; // �������J�E���g�𑝂₷
                yield return new WaitForSeconds(0.05f);
            }

        }
        int leftoverHp = 0;//�c��HP��
        for (int i = 0; i < mBossHpBlocks.Length; i++)
        {
            if (mBossHpBlocks[i].gameObject.activeSelf)
            {
                leftoverHp++;
            }
        }

        if(leftoverHp == 0) { mBossData.IsSubjugation = true; }

        mBossData.BossStatus.infomations[mBossData.BossNumber].hp = leftoverHp;
        mBossGageCol = null;
        mGuageDownUpdateFinish = true;
        yield return null;
    }

   
}
