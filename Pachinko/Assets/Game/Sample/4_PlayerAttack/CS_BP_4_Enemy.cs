//-----------------------------
//�G���炩�猩���v���C���[�U���������������̏���(�{�X�t�F�[�Y���ڔԍ�:5)
//-----------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_4_Enemy : MonoBehaviour
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
        //���o�I����m�点��
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            //3�b��ɉ��o������
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f);
        }
        Debug.Log("���o�I��");
    }
}
