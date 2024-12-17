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

    public bool IsInV_Spot = false;
    public bool IsInV_Open = false;

    Quaternion defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.localRotation;
        Debug.Log("�����ʒu" + this.transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttakerEnable && IsAttackOpen)
        {
            if (IsAttackOpen) 
            {
                //AddDedama();//�o�ʏ���
            }
            if(IsInV_Open)
            {
                V_Spot_Update();
            }
        }
    }

    //�o�ʏ���
    private void AddDedama()
    {
        if (Prize < 15) { return; }

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        StopCoroutine(NextRoundTimer());
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.rotation = defaultRotation;
            return;
        }

        StartCoroutine(NextRound());//���̃��E���h��
    }

    public void AttakerOpen(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;

        RoundNum = _round;
        StartCoroutine(NextRoundTimer());
    }

    private IEnumerator NextRoundTimer()
    {
        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(defaultRotation.x, defaultRotation.y, openRot);
        float timer = 0;

        while (timer <= 20)
        {
            timer += Time.deltaTime;
            if (Prize < 15) { yield return null; }//15�����ĂȂ��Ȃ�I��
            else
            {
                Debug.Log("15������܂���");
                break;
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
            this.transform.rotation = defaultRotation;
            yield break;
        }

        //���̃��E���h
        StartCoroutine(NextRound());//���̃��E���h��
        yield break;
    }

    private IEnumerator NextRound()
    {
        this.transform.rotation = defaultRotation;

        yield return new WaitForSeconds(1f);



        //�^�C�}�[
        StartCoroutine(NextRoundTimer());
    }

    public void AttakerOpen_V_Bounus()
    {
        Prize = 0;
        IsAttakerEnable = true;
        IsInV_Open = true;
    }

    private void V_Spot_Update()
    {
        if (Prize >= 1)
        {
            IsInV_Spot = true;
            IsInV_Open = false;
            Prize = 0;
            this.transform.rotation = defaultRotation;
        }
    }
}
