using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LastUI_Controller : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> mUIs; // �ړ�������UI�̃��X�g

    [SerializeField]
    private List<RectTransform> mGorlTtans; // �S�[���ʒu�̃��X�g

    [SerializeField]
    private float mTimer = 1f; // �ړ��ɂ����鎞��

    [SerializeField]
    private CS_LastAttackPlayer mLastAttack;// �Ƃǂ�

    private List<Vector2> mStartPositions = new List<Vector2>(); // �J�n�ʒu���X�g
    private List<Vector2> mGoalPositions = new List<Vector2>();  // �S�[���ʒu���X�g
    private float elapsedTime = 0.0f; // �o�ߎ���
    private int index = 0; // ���ݏ�������UI�C���f�b�N�X

    private CS_CommonData mData;

    // Start is called before the first frame update
    void Start()
    {
        if (mUIs.Count != mGorlTtans.Count)
        {
            Debug.LogError("mUIs��mGorlTtans�̐�����v���Ă��܂���B");
            return;
        }

        // �J�n�ʒu�ƃS�[���ʒu��������
        for (int i = 0; i < mUIs.Count; i++)
        {
            mStartPositions.Add(mUIs[i].anchoredPosition);
            mGoalPositions.Add(mGorlTtans[i].anchoredPosition);
        }

        // ���ʃf�[�^��������
        mData = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ������������Ȃ�V���ܑҋ@����
        if (index >= mUIs.Count)
        {
            InV_Spot();
            return;
        }
        elapsedTime += Time.deltaTime;

        // �o�ߎ��Ԃ̊������v�Z
        float t = Mathf.Clamp01(elapsedTime / mTimer);

        // ���݂̈ʒu����`��ԂŌv�Z
        Vector2 newPosition = Vector2.Lerp(mStartPositions[index], mGoalPositions[index], t);
        mUIs[index].anchoredPosition = newPosition;

        // �ړ������������玟��UI�ɐi��
        if (t >= 1.0f)
        {
            elapsedTime = 0.0f; // �o�ߎ��Ԃ����Z�b�g
            index++; // ����UI��
        }

        if (index >= mUIs.Count) { mData.V_SpotOpen(); }
    }

    void InV_Spot()
    {
        bool In_V = mData.RightAttaker.IsInV_Spot;
        if(In_V)
        {
            mLastAttack.StartLastAttack();
            Destroy(this.gameObject);
        }
    }
}
