//---------------------------------
//�i�ߓ��i��j
//�S���ҁF����
//---------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_Controller : MonoBehaviour
{
    //�p�`���R�̏��
    public enum PACHINKO_PHESE
    {
        SET,    //�����t�F�[�Y
        MISSION,//�~�b�V�����t�F�[�Y
        BOSS,   //�{�X�t�F�[�Y       
        RUSH    //���b�V���t�F�[�Y
    }

    
    [SerializeField, Header("�i�ߓ��R���g���[���[")]
    List<GameObject> mCtrls = new List<GameObject>();

    private PACHINKO_PHESE mNowPhese = PACHINKO_PHESE.SET;//���݂̃t�F�[�Y
    private PACHINKO_PHESE mPrevPhese = PACHINKO_PHESE.SET;//�O�̂̃t�F�[�Y

    private int mStock = 0;//�ۗ��ʐ�

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃t�F�[�Y�̑O�̃t�F�[�Y���Ⴄ�Ȃ玟�̃t�F�[�Y�ɍs��
        if(mNowPhese != mPrevPhese) { GoNextPhese(); }

       
    }

    //���݂̃t�F�[�Y�擾
    public PACHINKO_PHESE GetPhese()
    {
        return mNowPhese;
    }

    //�t�F�[�Y�؂�ւ�
    public void ChangePhase(PACHINKO_PHESE _nextPhese)
    {
        mPrevPhese = mNowPhese;
        mNowPhese = _nextPhese;
    }

    //���̃t�F�[�Y�֍s��
    private void GoNextPhese()
    {
        mPrevPhese = mNowPhese;
        //�i�ߓ�����
        GameObject smallCtrl = Instantiate(mCtrls[(int)mNowPhese], transform.position, transform.rotation);
    }

    //�ۗ��ʂ𑝂₷
    public void AddStock()
    {
        mStock++;
    }

    public void UseStock()
    {
        mStock--;
    }

    //�ۗ��ʂ��擾����
    public int GetStock()
    {
        return mStock;
    }
}
