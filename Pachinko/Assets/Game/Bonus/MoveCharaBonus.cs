using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharaBonus : MonoBehaviour
{
    // �ړ���̍��W
    public Transform targetPos;
    // �ړ��ɂ����鎞�ԁi�b�j
    public float moveDuration = 2f;

    // �J�n����
    void Start()
    {
        // �R���[�`�����J�n
        StartCoroutine(MoveToPosition(targetPos.position, moveDuration));
    }

    // �w��ʒu��n�b�����Ĉړ�����R���[�`��
    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        // ���݂̈ʒu
        Vector3 start = transform.position;
        // �o�ߎ���
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // �o�ߎ��Ԃ̊���
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // ���`��Ԃňʒu���X�V
            transform.position = Vector3.Lerp(start, target, t);

            // ���̃t���[���܂őҋ@
            yield return null;
        }

        // �ŏI�I�Ɉʒu���m��
        transform.position = target;
    }
}
