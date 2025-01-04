using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CS_LeftAttakerOpenClose : MonoBehaviour
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

    private float mMoveSpeed = 480f;

    Vector3 defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.eulerAngles;
        Debug.Log("�����ʒu" + defaultRotation);
        //�e�X�g
        //AttakerOpen(3);
    }

  
    public void AttakerOpen(int _round)
    {
        if (!IsAttakerEnable)
        {
           
            IsAttakerEnable = true;
            StartCoroutine(NextRoundTimer());
            RoundNum = _round;
        }
        else 
        {
            RoundNum += _round; 
        }
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        Prize = 0;
        StartCoroutine(AttakerMove(openRot));//����ʒu�܂ŉ�]������
        float timer = 0;

        while(timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15�����ĂȂ��Ȃ�I��
            else 
            {
                yield break; 
            }
        }

        if (timer >= 20) { Debug.Log("20�b�o���܂���"); }

        Debug.Log("���̃��E���h�ֈڍs���܂�");

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            StartCoroutine(AttakerMove(defaultRotation.x));//�����ʒu�܂ŉ�]������
            yield break;
        }

        //���̃��E���h
        StartCoroutine(NextRound());//���̃��E���h��
        yield break;
    }

    private IEnumerator AttakerMove(float _targetRot)
    {
        // ���݂�z���̉�]�p�x���擾
        float currentXRotation = transform.eulerAngles.x;
        // ��]��openrot�܂œ��B����܂ŉ�]�𑱂���
        while (Mathf.Abs(currentXRotation - _targetRot) > 0.1f)
        {
            // ��]���v�Z
            float step = mMoveSpeed * Time.deltaTime; // �t���[�����Ƃ̉�]��
            float targetX = Mathf.MoveTowards(currentXRotation, _targetRot, step);

            // ��]��K�p
            transform.eulerAngles = new Vector3(targetX, transform.eulerAngles.y, transform.eulerAngles.z);

            // ���݂̊p�x���X�V
            currentXRotation = transform.eulerAngles.x;

            // ���̃t���[���܂őҋ@
            yield return null;
        }

        // �ŏI�I�ɖڕW�p�x�ɃX�i�b�v
        transform.eulerAngles = new Vector3(_targetRot, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private IEnumerator NextRound()
    {
        StartCoroutine(AttakerMove(defaultRotation.x));//�����ʒu�܂ŉ�]������
        Debug.Log("�A�^�b�J�[���܂�܂���");

        yield return new WaitForSeconds(1f);
        //�^�C�}�[
        StartCoroutine(NextRoundTimer());
    }
}


