using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_NodevCameraMotion : MonoBehaviour
{
    [SerializeField]
    private CSO_CameraInterpolation cameraMosion;

    private Camera subCamera;


    private int motionNum = 0; // ���݂̃��[�V�����ԍ�
    private int currentStep = 1; // ���݂̃X�e�b�v�ԍ�

    private bool isMoving = false; // �ړ������ǂ���

    // Start is called before the first frame update
    void Start()
    {
        subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();
        subCamera.transform.position = cameraMosion.inofomations[0].positions[0];
        subCamera.transform.eulerAngles = cameraMosion.inofomations[0].lotations[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && subCamera != null)
        {
            StartCoroutine(MoveAndRotateCamera());
        }
    }

    private IEnumerator MoveAndRotateCamera()
    {
        isMoving = true;

        var currentMotion = cameraMosion.inofomations[motionNum];

        // �|�W�V�����Ɖ�]�̃X�e�b�v���X�g
        List<Vector3> positions = currentMotion.positions;
        List<Vector3> rotations = currentMotion.lotations;

        // �������̃��X�g�̒�������ɂ���
        int stepCount = Mathf.Max(positions.Count, rotations.Count);

        // ���݂̃X�e�b�v����Ō�̃X�e�b�v�܂ŕ��
        while (currentStep < stepCount)
        {
            
            // �|�W�V�����̎擾�i�Z���ꍇ�͍Ō�̗v�f���ێ��j
            Vector3 startPosition = subCamera.transform.position;
            Vector3 targetPosition = currentStep < positions.Count
                ? positions[currentStep]
                : positions[positions.Count - 1];

            // ��]�̎擾�i�Z���ꍇ�͍Ō�̗v�f���ێ��j
            Quaternion startRotation = subCamera.transform.rotation;
            Quaternion targetRotation = currentStep < rotations.Count
                ? Quaternion.Euler(rotations[currentStep])
                : Quaternion.Euler(rotations[rotations.Count - 1]);

            float t = 0f;

            // �|�W�V�����Ɖ�]����
            while (t < 1f)
            {
                Debug.Log("�ړ���");
                t += Time.deltaTime * currentMotion.moveSpeed;

                // �|�W�V�������
                subCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                // ��]���
                subCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                yield return null;
            }

            // �X�e�b�v����
            currentStep++;
        }

        // ���̃��[�V�����ɐi��
        currentStep = 1;
        yield return StartCoroutine(NextCameraMotion());

        isMoving = false;
    }

    private IEnumerator NextCameraMotion()
    {
        yield return new WaitForSeconds(1f); // ���̃��[�V�����J�n�܂ł̑ҋ@

        motionNum++;

        if (motionNum >= cameraMosion.inofomations.Count)
        {
            motionNum = 0; // �ŏ��̃��[�V�����ɖ߂�
        }

        subCamera.transform.position = cameraMosion.inofomations[motionNum].positions[0];
        subCamera.transform.eulerAngles = cameraMosion.inofomations[motionNum].lotations[0];
    }
}
