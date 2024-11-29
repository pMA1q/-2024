using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGage : MonoBehaviour
{
    [SerializeField, Header("�v���C���[HP")]
    private Image[] mPlayerHpBlocks = new Image[10];
    [SerializeField, Header("�GHP")]
    private Image[] mBossHpGu = new Image[10];


    private CS_BossPhaseData mBossData;

    private float mNowHp;
    private float mGoalHp;
    private float mMaxPlayerHp;

    private Coroutine mPlayerGageCol = null;

    private int mPlayerOneBlockHp = 1;

    int test = 1;

    private void Start()
    {
        Invoke("LateStart", 0.5f);
    }
    // Start is called before the first frame update
    void LateStart()
    {
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        if(mBossData.PlayerStatus==null) { Debug.Log("PlayerStatus��null�ł�"); }
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        mPlayerOneBlockHp = (int)mMaxPlayerHp / 10;
        mPlayerOneBlockHp = 1;
        mBossData.PlayerOneBlockHp = mPlayerOneBlockHp;
        //mPlayerHpGage.value = mMaxPlayerHp/mMaxPlayerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayerGageCol == null) {  }
        else { Debug.Log("�R���[�`����null�ł͂Ȃ�"); }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerHpDown();
        }
    }

    //�v���C���[��HP�Q�[�W�����炷
    public void PlayerHpDown()
    {
        if(mPlayerGageCol == null) { mPlayerGageCol = StartCoroutine(PlayerSubGage()); }
        
    }
    private IEnumerator PlayerSubGage()
    {

        int count = 0; // �A�N�e�B�u�𖳌��ɂ�������J�E���g
        float attackPow = test / mPlayerOneBlockHp;
        
        int deleteNum = (int)attackPow;
        for (int i = 0; i < mPlayerHpBlocks.Length; i++)
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

        test++;
        mBossData.PlayerStatus.hp = leftoverHp;
        mPlayerGageCol = null;
        Debug.Log("�Q�[�W�����I��");
        yield return null;
    }
}
