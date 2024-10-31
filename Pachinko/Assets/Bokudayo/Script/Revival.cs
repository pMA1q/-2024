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
        rightStartPosition1 = rightCapsule1.transform.position;
        rightStartPosition2 = rightCapsule2.transform.position;
        rightStartPosition3 = rightCapsule3.transform.position;
        leftStartRotation = leftCapsule.transform.rotation;
        rightStartRotation1 = rightCapsule1.transform.rotation;
        rightStartRotation2 = rightCapsule2.transform.rotation;
        rightStartRotation3 = rightCapsule3.transform.rotation;
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
            rightCapsule2.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule2.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
            rightCapsule3.GetComponent<Rigidbody>().isKinematic = false;
            rightCapsule3.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);
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
