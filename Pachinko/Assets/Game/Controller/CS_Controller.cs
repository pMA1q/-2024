//---------------------------------
//�i�ߓ��i�j
//�S���ҁF����
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Controller : MonoBehaviour
{
    //�p�`���R�̏��
    public enum PACHINKO_PHESE
    {
        SET,    //�����t�F�[�Y
        BOSS,   //�{�X�t�F�[�Y       
        RUSH    //���b�V���t�F�[�Y
    }

    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//���݂̃t�F�[�Y
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//�O�̂̃t�F�[�Y

    private int mStock = 0;//�ۗ��ʐ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���݂̃t�F�[�Y�擾
    public PACHINKO_PHESE GetPhese()
    {
        return mNowPhese;
    }

    //�ۗ��ʂ𑝂₷
    public void AddStock()
    {
        mStock++;
    }
}
