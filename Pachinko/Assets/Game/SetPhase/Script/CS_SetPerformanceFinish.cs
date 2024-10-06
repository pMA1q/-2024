//---------------------------------
//�����t�F�[�Y�̉��o�I����
//�S���ҁF����
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPerformanceFinish : MonoBehaviour
{
    [SerializeField, Header("���o�I���܂ł̎���")]
    private float mTimer = 1f;

    //�I���񍐂܂ł̃^�C�}�[
    public float Timer
    {
        set
        {
            mTimer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FinishWait());
    }

  

    private IEnumerator FinishWait()
    {
        //�w�肵���b���҂�
        yield return new WaitForSeconds(mTimer);

        //�i�ߓ����擾���āA���o���I���������Ƃ�m�点��
        CS_Controller ctrl = GameObject.Find("BigController").GetComponent<CS_Controller>();
        ctrl.PerformanceFinish();
        //CS_SetPheseController spc = CS_SetPheseController.GetCtrl();
        if(ctrl == null) { Debug.LogError("�i�ߓ���������Ȃ�");}

        //spc.PerformanceFinish();
        Destroy(this);
    }
}
