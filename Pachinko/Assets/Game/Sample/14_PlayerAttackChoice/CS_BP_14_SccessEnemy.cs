using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_14_SccessEnemy : MonoBehaviour
{
    CS_HpGuage mHpGuage;
    // Start is called before the first frame update
    void Start()
    {
        mHpGuage = GameObject.Find("HpGuage").GetComponent<CS_HpGuage>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        //�Q�[���I�u�W�F�N�g�̃^�O��Player?
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�v���C���[����������");
            //�U������������
            PlayerAttackHit();
        }
    }

    private void PlayerAttackHit()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.right * 1000); // ���ɓ|���͂�������

        //�G��HP�Q�[�W�����炷
        mHpGuage.BossHpDown();

        //�I������
        StartCoroutine(Finish());
    }


    private IEnumerator Finish()
    {
        //�̗̓Q�[�W�̏������I������܂Ń��[�v
        while (!mHpGuage.HpUpdateFinish) { yield return null; }

        yield return new WaitForSeconds(2f);

       
        CS_BossPhaseData data = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
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
       
        Debug.Log("���o�I��");
    }
}
