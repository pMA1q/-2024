using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_NumberRail : MonoBehaviour
{
    [SerializeField, Header("��~�̃|�W�V����")]
    private RectTransform mStopPosition;
    [SerializeField, Header("���̃|�W�V����")]
    private RectTransform mBottom;
    [SerializeField, Header("0���珇��1�̐}����������")]
    private GameObject[] mNumberPatterns = new GameObject[9];

    [SerializeField, Header("�����X�s�[�h")]
    private float mSpeed = 300;
    [SerializeField, Header("�����X�s�[�h�i��~�J�n���j")]
    private float mStopStartSpeed = 2000;

    private float mInterval = 420;//�Ԋu

    private float mVariationSecond = 8f;

    private bool isVariation = false;

    private bool isStopStart = false;

    private int mStopNumber = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartVariation();   
    }

    // Update is called once per frame
    void Update()
    {
        if(!isVariation) { return; }

        //�e�X�g
        if (Input.GetKeyDown(KeyCode.Return)) { StopStart(); }

        float moveSpeed = mSpeed;
        if (isStopStart) { moveSpeed = mStopStartSpeed; }

        MovePatterns(moveSpeed);//�}���̈ړ�
    }

    private void MovePatterns(float _speed)
    {
        //���Ɍ������Đ}�����ړ�
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();

            Vector3 newPos = rTrans.localPosition;
            newPos.y -= _speed * Time.deltaTime;

            rTrans.localPosition = newPos;

            CheckOverBottom(i, rTrans);
        }

        //�}���̒�~���J�n����Ă���Ȃ��~�����J�n
        if (isStopStart) { StopPatterns(); }
    }

    private void StopPatterns()
    {
        int stopPattern = mStopNumber - 1;//�~�܂�}��

        //�~�܂�}���̃|�W�V�����擾
        RectTransform stopPatternRec = mNumberPatterns[stopPattern].GetComponent<RectTransform>();
        Vector3 newStopPos = stopPatternRec.localPosition;

        //��~�ʒu��艺�ɍs���Ε␳���Ē�~
        if(newStopPos.y <= mStopPosition.localPosition.y)
        {
            float offset = Mathf.Abs(newStopPos.y - mStopPosition.localPosition.y);
            for (int i = 0; i < 9; i++)
            {
                RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
                Vector3 newPos = rTrans.localPosition;
                newPos.y += offset;
                rTrans.localPosition = newPos;
            }

            //���̃��[���̕ϓ����X�g�b�v������
            isStopStart = false;
            isVariation = false;
        }

    }

    //�}���̒�~���J�n
    public void StopStart()
    {
        ArrangePatterns();
        isStopStart = true;
    }

    //BottomPos��艺�ɂ���Ȃ�ЂƂO�̃C���f�N�X+Interval�̃|�W�V�����ɂ���
    private void CheckOverBottom(int _val, RectTransform _rRtrans)
    {
        Vector3 pos = _rRtrans.position;
        if( pos.y <= mBottom.position.y)
        {
            // num��0�̂Ƃ��͍Ō�̗v�f���Q�Ƃ��邽�߂̃C���f�b�N�X���v�Z
            int previousIndex = (_val == 0) ? mNumberPatterns.Length - 1 : _val - 1;

            // �O�̃I�u�W�F�N�g��Y���W���擾
            Vector3 previousPos = mNumberPatterns[previousIndex].GetComponent<RectTransform>().localPosition;

            // Y���W��420���₵���ʒu�ɐݒ�
            Vector3 newPos = previousPos;
            newPos.y += mInterval;

            _rRtrans.localPosition = newPos;
        }
    }
    //�ϓ��̎��Ԃ��Z�b�g(�b)
    public void SetVariationSecond(float _second)
    {
        mVariationSecond = _second;
    }

    //���[���̉�]�X�^�[�g
    public void StartVariation()
    {
        StartCoroutine(StartVal());
    }

    private IEnumerator StartVal()
    {
        //�}���������グ��
        float up = 100;
        for(int i = 0; i < 9; i++)
        {
            RectTransform rectTransform = mNumberPatterns[i].GetComponent<RectTransform>();
            Vector3 numberPos = rectTransform.localPosition;
            numberPos.y += 50;
            rectTransform.localPosition = numberPos;
        }
        yield return new WaitForSeconds(1f);

        //���ɗ����ϓ��J�n
        isVariation = true;
    }

    private void ArrangePatterns()
    {
        //num - 4 �̃C���f�b�N�X���v�Z���A�͈͊O�̏ꍇ�ɕ␳
        int startIdx = mStopNumber - 5;
        if (startIdx < 0)
        {
            startIdx += mNumberPatterns.Length; // ���̃C���f�b�N�X��z��̌�납��ɕ␳
        }
        else if (startIdx >= mNumberPatterns.Length)
        {
            startIdx %= mNumberPatterns.Length; // �C���f�b�N�X���z��̒����𒴂���ꍇ�ɕ␳
        }

        // mBottom �̈ʒu�ɔz�u
        RectTransform startTransform = mNumberPatterns[startIdx].GetComponent<RectTransform>();
        startTransform.position = mBottom.position;

        // ���̐}���� mInterval �Ԋu�ŏ�ɐ���
        Vector3 newPos = startTransform.localPosition;
        for (int i = 1; i < mNumberPatterns.Length; i++)
        {
            int index = (startIdx + i) % mNumberPatterns.Length;
            newPos.y += mInterval;
            RectTransform rectTransform = mNumberPatterns[index].GetComponent<RectTransform>();
            rectTransform.localPosition = newPos;
        }
    }
}
