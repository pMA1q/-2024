using UnityEngine;

public class Revival : MonoBehaviour
{
    public GameObject leftCapsule;  // �����̃J�v�Z��
    public GameObject rightCapsule1; // �E���̃J�v�Z��
    public GameObject rightCapsule2; // �E���̃J�v�Z��
    public GameObject rightCapsule3; // �E���̃J�v�Z��

    private bool battleStarted = false;
    private int battleCount = 0; // �������
    private Vector3 leftStartPosition;  // ���J�v�Z���̏����ʒu
    private Vector3 rightStartPosition1; // �E�J�v�Z���̏����ʒu
    private Vector3 rightStartPosition2; 
    private Vector3 rightStartPosition3; 
    private Quaternion leftStartRotation; // ���J�v�Z���̏�����]
    private Quaternion rightStartRotation1; // �E�J�v�Z���̏�����]
    private Quaternion rightStartRotation2; 
    private Quaternion rightStartRotation3; 
    private bool battleEnded = false; // �o�g�����I���������ǂ���

    void Start()
    {
        // �����ʒu�Ɖ�]��ۑ�
        leftStartPosition = leftCapsule.transform.position;
        leftStartRotation = leftCapsule.transform.rotation;
        if(rightCapsule1)
        {
            rightStartPosition1 = rightCapsule1.transform.position;
            rightStartRotation1 = rightCapsule1.transform.rotation;
        }
        if (rightCapsule2)
        {
            rightStartPosition2 = rightCapsule2.transform.position;
            rightStartRotation2 = rightCapsule2.transform.rotation;
        }
        if (rightCapsule3)
        {
            rightStartPosition3 = rightCapsule3.transform.position;
            rightStartRotation3 = rightCapsule3.transform.rotation;
        }
    }

    void Update()
    {
        if (!battleStarted && !battleEnded)
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

        // ���҂�����
        bool leftWins = battleCount % 2 == 1; // ���͍��������A������͉E������

        if (leftWins)
        {
            // �E���̃J�v�Z�����|���
            rightCapsule1.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule1.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
            rightCapsule1.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule1.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������
            if (rightCapsule2)
            {
                rightCapsule2.GetComponent<Rigidbody>().isKinematic = false;
                rightCapsule2.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������
            }
            if (rightCapsule3)
            {
                rightCapsule3.GetComponent<Rigidbody>().isKinematic = false;
                rightCapsule3.GetComponent<Rigidbody>().AddForce(Vector3.right * 500); // ���ɓ|���͂�������
            }
            yield return new WaitForSeconds(2); // �|�ꂽ��̑ҋ@����

            // �o�g���I������
            //Debug.Log("�E���̃J�v�Z�����|��܂����B�o�g���I���I");
            battleEnded = true; // �o�g���I���t���O��ݒ�
        }
        else
        {
            // �����̃J�v�Z�����|���
            leftCapsule.GetComponent<Rigidbody>().isKinematic = false;
            leftCapsule.GetComponent<Rigidbody>().AddForce(Vector3.left * 500);
            yield return new WaitForSeconds(2); // �|�ꂽ��̑ҋ@����
            RespawnCapsule(leftCapsule, leftStartPosition, leftStartRotation);
            GameObject rootObject = transform.root.gameObject;
            if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
            {
                rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 5f); ;//�v���n�u�������܂ł̎���(�b)
            }
        }

        battleCount++; // ������񐔂��J�E���g
    }

    void RespawnCapsule(GameObject capsule, Vector3 startPosition, Quaternion startRotation)
    {
        // �J�v�Z�������̈ʒu�Ɖ�]�ɕ���������
        capsule.transform.position = startPosition;
        capsule.transform.rotation = startRotation; // ��]�����ɖ߂�
        capsule.GetComponent<Rigidbody>().isKinematic = true; // �ēx�Î~��Ԃɖ߂�
        battleStarted = false; // �o�g�����ăX�^�[�g�\�ɂ���
    }
}
