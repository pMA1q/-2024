using UnityEngine;

public class P_RanAway : MonoBehaviour
{
    public float slideSpeed = 5f; // �X���C�h���x
    private bool isSliding = false; // �X���C�h�����ǂ����̃t���O
    private Vector3 targetPosition; // �ړ���̈ʒu

    void Start()
    {
        targetPosition = new Vector3(-20, 0, 0); // �ړ���̈ʒu��ݒ�
        StartCoroutine(StartSlidingAfterDelay(1f)); // 1�b�ҋ@���ăX���C�h���J�n
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 5f); ;//�v���n�u�������܂ł̎���(�b)
        }
    }

    private System.Collections.IEnumerator StartSlidingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        isSliding = true; // �X���C�h���J�n
    }

    void Update()
    {
        // �X���C�h���̏ꍇ�͈ړ�
        if (isSliding)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            // �ړ�������������X���C�h���I��
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isSliding = false;
                // �I�v�V����: �J�v�Z�����\���ɂ���
                // gameObject.SetActive(false);
            }
        }
    }
}
