using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_RightAttakerOpenClose : MonoBehaviour
{
    [NonSerialized]
    public int Prize = 0;//���ܐ�

    [NonSerialized]
    public int RoundNum = 1;

    [NonSerialized]
    public bool IsAttakerEnable = false;

    [SerializeField]
    private float openRot;

    private bool IsAttackOpen = false;

    private int NowRound = 1;

    [NonSerialized]
    public bool IsInV_Spot = false;
    [NonSerialized]
    public bool IsInV_Open = false;

    private float mMoveSpeed = 480f;

    Vector3 defaultRotation;

    GameObject mBigCtrl;
    CS_BonusPhaseData mBdata;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.eulerAngles;
        mBigCtrl = GameObject.Find(CS_CommonData.BigControllerName);
        mBdata = mBigCtrl.GetComponent<CS_BonusPhaseData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttakerEnable)
        {
            
            if(IsInV_Open)
            {
                V_Spot_Update();
            }
        }
    }

    
    public void AttakerStart(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;

        RoundNum = _round;
        StartCoroutine(NextRoundTimer());
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        StartCoroutine(AttakerMove(openRot));//����ʒu�܂ŉ�]������
        Prize = 0;
        float timer = 0;

        while (timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15�����ĂȂ��Ȃ�I��
            else
            {
                break;
            }
        }

        if (timer >= 20) { Debug.Log("20�b�o���܂���"); }

        Debug.Log("���̃��E���h�ֈڍs���܂�");

        Prize = 0;
        NowRound++;
        CS_Controller.PACHINKO_PHESE nowPhase = mBigCtrl.GetComponent<CS_Controller>().GetPhese();
        if(nowPhase == CS_Controller.PACHINKO_PHESE.BOUNUS)
        {
            mBdata.RoundCount = NowRound;
        }
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            if (nowPhase == CS_Controller.PACHINKO_PHESE.BOUNUS)
            {
                mBdata.IsBonusFinish = true;
            }
            IsAttakerEnable = false;
            NowRound = 0;
            StartCoroutine(AttakerMove(defaultRotation.z));//�����ʒu�܂ŉ�]������
            yield break;
        }

        //���̃��E���h
        StartCoroutine(NextRound());//���̃��E���h��
        yield break;
    }

    private IEnumerator AttakerMove(float _targetRot)
    {
        // ���݂�z���̉�]�p�x���擾
        float currentZRotation = transform.eulerAngles.z;
        // ��]��openrot�܂œ��B����܂ŉ�]�𑱂���
        while (Mathf.Abs(currentZRotation - _targetRot) > 0.1f)
        {
            // ��]���v�Z
            float step = mMoveSpeed * Time.deltaTime; // �t���[�����Ƃ̉�]��
            float targetZ = Mathf.MoveTowards(currentZRotation, _targetRot, step);

            // ��]��K�p
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetZ);

            // ���݂̊p�x���X�V
            currentZRotation = transform.eulerAngles.z;

            // ���̃t���[���܂őҋ@
            yield return null;
        }

        // �ŏI�I�ɖڕW�p�x�ɃX�i�b�v
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, _targetRot);
    }

    private IEnumerator NextRound()
    {
        StartCoroutine(AttakerMove(defaultRotation.z));//�����ʒu�܂ŉ�]������

        yield return new WaitForSeconds(1f);

        //�^�C�}�[
        StartCoroutine(NextRoundTimer());
    }

    public void AttakerStart_V_Bounus()
    {
        Prize = 0;
        StartCoroutine(AttakerMove(openRot));//����ʒu�܂ŉ�]������
        IsAttakerEnable = true;
        IsInV_Open = true;
        IsInV_Spot = false;
    }

    private void V_Spot_Update()
    {
        Debug.Log("V���ܑ҂�");
        if (Prize >= 1)
        {
            IsInV_Spot = true;
            IsInV_Open = false;
            IsAttakerEnable = false;
            Prize = 0;
            StartCoroutine(AttakerMove(defaultRotation.z));//�����ʒu�܂ŉ�]������
        }
    }
}
