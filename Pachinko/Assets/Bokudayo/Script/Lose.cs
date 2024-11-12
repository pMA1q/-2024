using UnityEngine;

public class Lose : MonoBehaviour // �N���X����ύX
{
    public GameObject leftCapsule;  // �����̃J�v�Z��
    public GameObject rightCapsule1; // �E���̃J�v�Z��
    public GameObject rightCapsule2; // �E���̃J�v�Z��
    public GameObject rightCapsule3; // �E���̃J�v�Z��

    private bool battleStarted = false;

    void Update()
    {
        if (!battleStarted)
        {
            // �o�g�����J�n
            StartBattle();
        }
    }

    void StartBattle()
    {
        battleStarted = true;

        // �J�v�Z�����키
        StartCoroutine(BattleCoroutine());
    }

    System.Collections.IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(1); // �키�O�̑ҋ@����

        // �����_���ɏ��҂�����
        //bool leftWins = Random.value > 0.5f;

        //�����̃J�v�Z�����|���
        bool leftWins = false;

        if (leftWins)
        {
            // �E���̃J�v�Z�����|���
            rightCapsule1.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule1.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������
            rightCapsule2.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule2.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������
            rightCapsule3.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule3.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������

           
        }
        else
        {
            // �����̃J�v�Z�����|���
            leftCapsule.GetComponent<Rigidbody>().isKinematic = false;
            leftCapsule.GetComponent<Rigidbody>().AddForce(Vector3.left * 500);
            GameObject rootObject = transform.root.gameObject;
            if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
            {
                rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 5f); ;//�v���n�u�������܂ł̎���(�b)
            }
        }
    }
}
