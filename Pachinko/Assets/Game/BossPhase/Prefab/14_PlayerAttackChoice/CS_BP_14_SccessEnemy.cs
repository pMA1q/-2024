using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_14_SccessEnemy : MonoBehaviour
{
    [SerializeField, Header("�|���܂ł̎���")]
    private float mStartDown = 0.3f;
    CS_HpGuage mHpGuage;
    // Start is called before the first frame update
    void Start()
    {
        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
        StartCoroutine(PlayerAttackHit());
    }

   

    private IEnumerator PlayerAttackHit()
    {
        yield return new WaitForSeconds(mStartDown);

        GetComponent<CS_SetCharaAnimState>().SetState(CHARACTER_ANIMATION_STATE.DAMAGE);

        //�G��HP�Q�[�W�����炷
        mHpGuage.BossHpDown();

        //�I������
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        //�̗̓Q�[�W�̏������I������܂Ń��[�v
        while (!mHpGuage.HpUpdateFinish) { yield return null; }

        yield return new WaitForSeconds(5f);

       
        CS_BossPhaseData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        //���o�I����m�点��
        GameObject rootObject = transform.root.gameObject;
        if (data.GetChoiceSuccess())
        {
            GameObject BossPerf14 = GameObject.Find("14_PlayerAttackChoice");
            if (!BossPerf14) { Debug.LogError("���o14���Ȃ�(���O�m�F)"); }
            BossPerf14.GetComponent<CS_BP_AttackChoice>().Init();
            Destroy(rootObject);
        }
        else
        {
           
            if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
            {
                //3�b��ɉ��o������
                rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f);
            }
        }
       
    }
}
