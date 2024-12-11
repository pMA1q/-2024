//---------------------------------
//�����t�F�[�Y�̉��o�I����
//�S���ҁF����
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformanceFinish : MonoBehaviour
{
    [SerializeField, Header("���o�I���܂ł̎���")]
    private float mTimer = 0f;

    public bool IsDestroyObject = true;

    //�I���񍐂܂ł̃^�C�}�[
    public float DestroyTimer
    {
        set
        {
            mTimer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceSemiFinish = true;//���o�I�����t���O��true
        
    }

    public void DestroyConfig(bool _destroy, float _destroyTimer)
    {
        IsDestroyObject = _destroy;
        DestroyTimer = _destroyTimer;

        StartCoroutine(FinishWait());
    }

    private IEnumerator FinishWait()
    {
        //�i�ߓ����擾���āA���o���I���������Ƃ�m�点��
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceSemiFinish = true ;
        //�w�肵���b���҂�
        yield return new WaitForSeconds(mTimer);

        
        ctrl.PerformanceFinish();
        //CS_SetPheseController spc = CS_SetPheseController.GetCtrl();
        if(ctrl == null) { Debug.LogError("�i�ߓ���������Ȃ�");}

        if (!IsDestroyObject) { Destroy(this); yield return null; }

        //spc.PerformanceFinish();
        // ���̃I�u�W�F�N�g��������ŏ�ʂ̐e�I�u�W�F�N�g���擾
        GameObject rootObject = transform.root.gameObject;

        // �ŏ�ʂ̃I�u�W�F�N�g�i�e�v���n�u�j���폜
        Destroy(rootObject);
    }
}
