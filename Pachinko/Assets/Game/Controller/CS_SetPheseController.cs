//---------------------------------
//�����t�F�[�Y�i�ߓ�
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPheseController : MonoBehaviour
{
    //�����t�F�[�Y�̉��o���
    public enum SET_DIRECTING
    {
        VALUE1,
        VALUE2,
        VALUE3,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SET_DIRECTING random = CS_LotteryFunction.LotDirecting<SET_DIRECTING>();
            Debug.Log("�����_���ɑI�΂ꂽEN�̒l: " + random);
        }
    }
}
