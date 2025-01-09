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
    [SerializeField, Header("0���珇��1�̐}����������(�E��9����)")]
    private GameObject[] mNumberPatterns = new GameObject[9];

    [SerializeField, Header("0���珇��1�̐}����������(�E��9����)")]
    private bool isInversion = false;

    [SerializeField, Header("�p�l���I�u�W�F�N�g")]
    private RectTransform mPanelRect;

    private float mSpeed = 10000;//�����X�s�[�h

    private float mStopStartSpeed = 2000;//��~�J�n���̃X�s�[�h

    private float mInterval = 420;//�Ԋu

    private bool isVariation = false;

    private bool isStopStart = false;

    private int mStopNumber = 1;

    private float mNowAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
            CheckOutsidePanel(i, rTrans);//�p�l���O�Ȃ瓧���ɂ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(!isVariation) { return; }

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
            CheckOutsidePanel(i, rTrans);//�p�l���O�Ȃ瓧���ɂ���
        }

        //�}���̒�~���J�n����Ă���Ȃ��~�����J�n
        if (isStopStart) { StopPatterns(); }
    }

    //�p�l���O�ɏo����Alpha�l��0�ɂ���
    private void CheckOutsidePanel(int _index, RectTransform _rTrans)
    {
        
        // Panel��RectTransform�����擾
        Rect panelRect = mPanelRect.rect;
        Vector3[] panelCorners = new Vector3[4];
        mPanelRect.GetWorldCorners(panelCorners);

        // Panel��bottom�ʒu�i���[���h���W�j���擾
        float panelBottom = panelCorners[0].y; // ������y���W��bottom

        // �I�u�W�F�N�g�̌��݈ʒu�i���[���h���W�j���擾
        Vector3 objectPosition = _rTrans.position;

        // �I�u�W�F�N�g��bottom��Panel��bottom��艺�����`�F�b�N
        bool isBelow = objectPosition.y < panelBottom;

        // Alpha���X�V
        Image img = mNumberPatterns[_index].GetComponent<Image>();
        if (img != null)
        {
            img.color = new Color(1, 1, 1, isBelow ? 0.0f : mNowAlpha);
        }
    }

   

    private void StopPatterns()
    {
        int stopPattern = mStopNumber - 1;//�~�܂�}��

        //���]�t���O������Ȃ��~�ԍ��Ƌt�̔ԍ��ɃA�N�Z�X�ł���悤�ɐݒ�
        if(isInversion){ stopPattern = 9 - stopPattern - 1; }

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
                CheckOutsidePanel(i, rTrans);//�p�l���O�Ȃ瓧���ɂ���
            }

            //���̃��[���̕ϓ����X�g�b�v������
            isStopStart = false;
            isVariation = false;
        }

    }

    //�}���̒�~���J�n
    public void StopStart(int _stopNum)
    {
        mStopNumber = _stopNum;
        ArrangePatterns(5);
        isStopStart = true;
    }
    public void StopStartRush(int _stopNum)
    {
        mStopNumber = _stopNum;
        Debug.Log("stopnum" + mStopNumber);
        ArrangePatterns(1);
        StopPatterns();
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


    //���[���̉�]�X�^�[�g
    public void StartVariation()
    {
        StartCoroutine(StartVal());
    }
   

    //�A���t�@�l��ς���
    public void ChangeAlpha(float _alpha)
    {
        mNowAlpha = _alpha;
        for (int i = 0; i < 9; i++)
        {
            RectTransform rTrans = mNumberPatterns[i].GetComponent<RectTransform>();
            mNumberPatterns[i].GetComponent<Image>().color = new Color(1, 1, 1, _alpha);
            CheckOutsidePanel(i, rTrans);//�p�l���O�Ȃ瓧���ɂ���
        }
    }

    private IEnumerator StartVal()
    {
        
        //�}���������グ��
        float up = 100;
        float afterpos = mNumberPatterns[0].GetComponent<RectTransform>().localPosition.y + up;
       

        while(mNumberPatterns[0].GetComponent<RectTransform>().localPosition.y <= afterpos)
        {
            for (int i = 0; i < 9; i++)
            {
                RectTransform rectTransform = mNumberPatterns[i].GetComponent<RectTransform>();
                Vector3 numberPos = rectTransform.localPosition;
                numberPos.y += 150 * Time.deltaTime;
                rectTransform.localPosition = numberPos;
                CheckOutsidePanel(i, rectTransform);//�p�l���O�Ȃ瓧���ɂ���
            }

            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f);

        //���ɗ����ϓ��J�n
        isVariation = true;
    }

    //�}���̐���
    private void ArrangePatterns(int _bottomNum)
    {
        //num - 4 �̃C���f�b�N�X���v�Z���A�͈͊O�̏ꍇ�ɕ␳
        int startIdx = mStopNumber - _bottomNum;
        if (isInversion)
        {
            startIdx = 9 - mStopNumber - (_bottomNum-1);
        }
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
            CheckOutsidePanel(i, rectTransform);//�p�l���O�Ȃ瓧���ɂ���
        }
    }
}
