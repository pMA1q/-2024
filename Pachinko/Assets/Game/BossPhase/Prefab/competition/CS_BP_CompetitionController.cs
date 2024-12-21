using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_CompetitionController : MonoBehaviour
{
    private CS_BossPhaseData mBossData;
    private CSO_PlayerStatus mPlayerStatus;
    [SerializeField]
    List<GameObject> mTikets;

    [SerializeField, Header("��I�����̐F")]
    private Color mNoSelectColor;

    List<int> mTiketNum = new List<int> { 0,0,0};
    List<int> mTiketNumbers = new List<int> { 1,2,3};

    private Color mSelectColor = new Color(1f, 1f, 1f, 1f);

    private int selectNum = 0;

    bool IsOnePush = false;
    Button[] buttons;

    public bool NoHaveTikets() { return mTikets.Count == 0; }
    // Start is called before the first frame update
    private void OnEnable()
    {
        mBossData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_BossPhaseData>();
        if (mBossData == null) { Debug.LogWarning("CS_BossPhaseData������"); }
        mPlayerStatus = mBossData.PlayerStatus;
        mTiketNum[0] = mPlayerStatus.ticket.special;
        mTiketNum[1] = mPlayerStatus.ticket.partner;
        mTiketNum[2] = mPlayerStatus.ticket.preemptiveAttack;

        for (int i = 0; i < mTikets.Count; i++)
        {
            if (mTiketNum[i] <= 0)
            {
                //mTikets[i]��mTiketNumbers[i]�����������l�߂�
                Destroy(mTikets[i]);
                mTikets.RemoveAt(i);
                mTiketNum.RemoveAt(i);
                mTiketNumbers.RemoveAt(i);
                i --;
            }
            
        }
        Debug.Log("�`�P�b�g�J�E���g" + mTikets.Count);
        //0�ԖڈӊO�̉摜���I�����̐F�ɕύX
        for (int i = 1; i < mTikets.Count; i++)
        {
            mTikets[i].GetComponent<Image>().color = mNoSelectColor;
        }

        if (mTikets.Count <= 0)
        {
            mBossData.UseTiket = CS_BossPhaseData.USE_TIKET.NONE;
            StartCoroutine(AfterPush());
        }
        else
        {
            FindButtons();
            StartCoroutine(NoPush());
        }
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find(CS_CommonData.MainCanvasName).GetComponent<Canvas>();
        buttons = canvas.GetComponentsInChildren<Button>();


        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(LeftButton); }
            else if (buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(RightButton); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(PushButton); }
        }
    }

    //�E�{�^���������ꂽ�Ƃ��̏���
    private void RightButton()
    {
         // �Ō��Cube�̐F��ۑ�
        Color lastColors = mTikets[mTikets.Count - 1].GetComponent<Image>().color;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = mTikets.Count - 1; i > 0; i--)
        {
            Color currentMat = mTikets[i - 1].GetComponent<Image>().color;
            mTikets[i].GetComponent<Image>().color = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        mTikets[0].GetComponent<Image>().color = lastColors;

        //�I��ԍ����Z
        selectNum++;
        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum >= mTikets.Count) { selectNum = 0; }
    }

    //���{�^���������ꂽ�Ƃ��̏���
    private void LeftButton()
    {
        // �Ō��Cube�̐F��ۑ�
        Color lastColors = mTikets[0].GetComponent<Image>().color;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = 0; i < mTikets.Count - 1; i++)
        {
            Color currentMat = mTikets[i + 1].GetComponent<Image>().color;
            mTikets[i].GetComponent<Image>().color = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        mTikets[mTikets.Count - 1].GetComponent<Image>().color = lastColors;
        selectNum--;
        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum < 0) { selectNum = mTikets.Count - 1; }
    }

    //Push�{�^���������ꂽ�Ƃ��̏���
    private void PushButton()
    {
        if (IsOnePush) { return; }
        IsOnePush = true;

        //�{�^���𖳌��ɂ���
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        //�I�񂾂��̈ӊO������
        for (int i = 0; i < mTikets.Count; i++)
        {
            if (i != selectNum)
            {
                mTikets[i].SetActive(false);
            }
        }

        //�I�񂾃`�P�b�g�̖��������炷
        mTiketNum[selectNum]--;
        //�v���C���[�̃`�P�b�g�X�e�[�^�X�X�V
        switch(mTiketNumbers[selectNum])
        {
            case 1:
                mPlayerStatus.ticket.special = mTiketNum[selectNum];
                break;
            case 2:
                mPlayerStatus.ticket.partner = mTiketNum[selectNum];
                break;
            case 3:
                mPlayerStatus.ticket.preemptiveAttack = mTiketNum[selectNum];
                break;
        }

        //�g�����`�P�b�g�̎�ނ��X�V
        mBossData.UseTiket = (CS_BossPhaseData.USE_TIKET)mTiketNumbers[selectNum];

        StartCoroutine(AfterPush());
    }
    
    private IEnumerator NoPush()
    {
        float t = 0f;

        while(t <= 5f)
        {
            if(IsOnePush)
            {
                yield break;
            }

            t += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Push�{�^����������Ȃ��������ߋ����I��");
        PushButton();

        yield return null;
    }

    private IEnumerator AfterPush()
    {

        yield return  new WaitForSeconds(2f);


        //Destroy(transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
