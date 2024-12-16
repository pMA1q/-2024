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


    Quaternion defaultRotation;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = this.transform.localRotation;
        Debug.Log("�����ʒu" + this.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAttakerEnable && IsAttackOpen)
        {
            AddDedama();//�o�ʏ���
        }
    }

    //�o�ʏ���
    private void AddDedama()
    {
        if(Prize < 15) { return; }

        Prize = 0;
        NowRound++;
        IsAttackOpen = false;
        if (NowRound > RoundNum)
        {
            IsAttakerEnable = false;
            NowRound = 0;
            this.transform.rotation = defaultRotation;
            StopCoroutine(NextRoundTimer());
            return;
        }

        StartCoroutine(NextRound());//���̃��E���h��
    }

    public void AttakerOpen(int _round)
    {
        Prize = 0;
        IsAttakerEnable = true;
        StartCoroutine(NextRound());
        StartCoroutine(NextRoundTimer());
        RoundNum = _round;
    }

  

    private IEnumerator NextRoundTimer()
    {
        yield return new WaitForSeconds(20f);
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

        StartCoroutine(NextRound());//���̃��E���h��
    }

    private IEnumerator NextRound()
    {
        this.transform.rotation = defaultRotation;

        yield return new WaitForSeconds(1f);

        IsAttackOpen = true;
        this.transform.eulerAngles = new Vector3(openRot, defaultRotation.y, defaultRotation.z);
    }
}


