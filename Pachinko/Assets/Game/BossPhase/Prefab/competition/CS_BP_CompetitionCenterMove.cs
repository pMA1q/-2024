using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_CompetitionCenterMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    // �ړ��͈�
    private float moveRange = 200f;

    // �����ϐ�
    private RectTransform rectTransform;
    private float direction = 1f; // 1: �E, -1: ��
    private float initialX;

    void Start()
    {
        // RectTransform�̎Q�Ƃ��擾
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform���A�^�b�`����Ă��܂���I");
            enabled = false;
            return;
        }
        // ������X���W��ۑ�
        initialX = rectTransform.anchoredPosition.x;
    }

    void Update()
    {
        // ���݂̈ʒu���擾
        Vector2 pos = rectTransform.anchoredPosition;

        // X���W���X�V
        pos.x += speed * direction * Time.deltaTime;

        // �͈͂𒴂����ꍇ�A�ړ������𔽓]
        if (pos.x > initialX + moveRange)
        {
            pos.x = initialX + moveRange;
            direction = -1f;
        }
        else if (pos.x < initialX - moveRange)
        {
            pos.x = initialX - moveRange;
            direction = 1f;
        }

        // �X�V���ꂽ�ʒu��K�p
        rectTransform.anchoredPosition = pos;
    }
}
