using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_BP_AttackChoice : MonoBehaviour
{
    [SerializeField,Header("�I��Images")]
    private List<GameObject> mImages; // �F���ړ�������Cube�̔z��

    [SerializeField, Header("�I�𒆂̉��o�v���n�u")]
    private GameObject�@mChoiceTimePrefab;

    [SerializeField, Header("�I�𐬌����̉��o�v���n�u")]
    private GameObject mChoiceSuccessPrefab;

    [SerializeField, Header("�I�����s���̉��o�v���n�u")]
    private GameObject mChoiceFailedPrefab;

    [SerializeField, Header("��I�����̐F")]
    private Color mNoSelectColor;

    Button[] buttons;

    private GameObject mAfterChoicePrefab;//�I����̉��o�v���n�u

    private Color mSelectColor = new Color ( 1f, 1f, 1f, 1f);

    private int selectNum = 0;
    private int mSuccessNum;

    private static int successionNum = 0; //������

    //
    public static int SuccessionNum
    {
        get { return successionNum; }
    }

    private void Start()
    {
        // �{�^���������ꂽ�Ƃ��̃C�x���g��ݒ�
        FindButtons();
        Init();
        successionNum = 0;
    }

    public void Init()
    {
        mChoiceTimePrefab.SetActive(true);
        if (GetComponent<CS_SetPositionPerfPos>()) { GetComponent<CS_SetPositionPerfPos>().Start(); }
        
        if (mAfterChoicePrefab != null) { Destroy(mAfterChoicePrefab); }
      
        if (mImages[0] != null)
        {
            var imageComponent = mImages[0].GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = mSelectColor;
            }
            else
            {
                Debug.LogError("Image �R���|�[�l���g��������܂���I");
            }
        }
        else
        {
            Debug.LogError("mImages �z�񂪋�A�܂��� mImages[0] �� null �ł��I");
        }
        mImages[0].GetComponent<Image>().color = mSelectColor;
        //0�ԖڈӊO�̉摜���I�����̐F�ɕύX
        for (int i = 1; i < mImages.Count; i++)
        {
            mImages[i].GetComponent<Image>().color = mNoSelectColor;
        }

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        for (int i = 0; i < mImages.Count; i++) { mImages[i].SetActive(true); }
        //�I�𐬌��ԍ��̒�����
        mSuccessNum = CS_LotteryFunction.LotNormalInt(mImages.Count);
        Debug.Log("����" + mSuccessNum);
    }

    private void FindButtons()
    {
        Canvas canvas = GameObject.Find("ButtonCanvas").GetComponent<Canvas>();
        buttons = canvas.GetComponentsInChildren<Button>();

      
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == "ButtonLeft") { buttons[i].onClick.AddListener(MoveLeft); }
            else if (buttons[i].name == "ButtonRight") { buttons[i].onClick.AddListener(MoveRight); }
            else if (buttons[i].name == "ButtonPush") { buttons[i].onClick.AddListener(AttackDecision); }
        }
    }

    //�E�{�^��
    private void MoveRight()
    {
        // �Ō��Cube�̐F��ۑ�
        Color lastColors = mImages[mImages.Count - 1].GetComponent<Image>().color;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = mImages.Count - 1; i > 0; i--)
        {
            Color currentMat = mImages[i - 1].GetComponent<Image>().color;
            mImages[i].GetComponent<Image>().color = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        mImages[0].GetComponent<Image>().color = lastColors;

        //�I��ԍ����Z
        selectNum++;
        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum >= mImages.Count) { selectNum = 0; }
    }

    //���{�^��
    private void MoveLeft()
    {
        // �Ō��Cube�̐F��ۑ�
        Color lastColors = mImages[0].GetComponent<Image>().color;

        // �F��ׂ̃I�u�W�F�N�g�Ɉړ�������
        for (int i = 0; i < mImages.Count - 1; i++)
        {
            Color currentMat = mImages[i + 1].GetComponent<Image>().color;
            mImages[i].GetComponent<Image>().color = currentMat;
        }

        // �ŏ���Cube�̐F���Ō��Cube�̐F�ɐݒ�
        mImages[mImages.Count - 1].GetComponent<Image>().color = lastColors;

        selectNum--;

        //selectBack�̃��X�g�͈̔͂𒴂��Ȃ��悤�ɂ���
        if (selectNum < 0) { selectNum = mImages.Count - 1; }
    }

    //Push�{�^��
    private void AttackDecision()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        mChoiceTimePrefab.SetActive(false);
        //�I�΂�ĂȂ��~�b�V�����͏���
        for (int i = 0; i < mImages.Count; i++)
        {
            if (i != selectNum)
            {
                mImages[i].SetActive(false);
            }
        }

        //BossPhaseData���擾
        CS_BossPhaseData bossData = GameObject.Find("BigController").GetComponent<CS_BossPhaseData>();
        
        Transform parentObject = transform.root;
        //�I����̉��o�𐶐�
        if (selectNum == mSuccessNum)
        {
            //����
            successionNum++;
            mAfterChoicePrefab = Instantiate(mChoiceSuccessPrefab, Vector3.zero, Quaternion.identity);
            if (successionNum < 3)//�A�����𐔂�3
            {
                bossData.ChoiceSuccess(true);
            }
            else
            {
                bossData.ChoiceSuccess(false);
                Destroy(transform.root.gameObject);
            }
        }
        else
        {
            mAfterChoicePrefab = Instantiate(mChoiceFailedPrefab, Vector3.zero, Quaternion.identity);
            bossData.ChoiceSuccess(false);
            Destroy(transform.root.gameObject);
        }
    }
}
