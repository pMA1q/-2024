using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HpGage : MonoBehaviour
{
    [SerializeField, Header("�v���C���[HP")]
    private Slider mPlayerHpGage;
    [SerializeField, Header("�GHP")]
    private Slider mBossHpGage;


    private CS_BossPhaseData mBossData;

    private float mNowHp;
    private float mGoalHp;
    private float mMaxPlayerHp;

    private Coroutine mPlayerGageCol = null;

    // Start is called before the first frame update
    void Start()
    {
        mBossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        mMaxPlayerHp = mBossData.PlayerStatus.backupStatus.hp;
        //mPlayerHpGage.value = mMaxPlayerHp/mMaxPlayerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayerGageCol == null) { Debug.Log("�R���[�`����null�ł�"); }
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
        mNowHp = mBossData.PlayerStatus.hp;
        Debug.Log("���݂̃v���C���[HP" + mNowHp);
        mGoalHp = (mNowHp - mBossData.PlayerOneAttackPow);
        Debug.Log("�S�[��" + mGoalHp);
        float subSpeed = mMaxPlayerHp * 0.1f;

        while(mNowHp > mGoalHp)
        {
            mNowHp -= subSpeed * Time.deltaTime;
            //if(mNowHp <= mGoalHp) { mNowHp = mGoalHp; }
            mPlayerHpGage.value = mNowHp / mMaxPlayerHp;
            yield return null; // 1�t���[���ҋ@

        }

        //�e�X�g
        mBossData.PlayerStatus.hp = mNowHp;

        mPlayerGageCol = null;

        Debug.Log("�Q�[�W�������I��");
        yield return null;
    }
}
