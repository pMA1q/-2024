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
    private Coroutine motionCoroutine; // �J�������[�V�����̃R���[�`��

    private bool mosionStart = false;

    // �o�b�N�A�b�v�p�ϐ�
    private Vector3 backupPosition;
    private Quaternion backupRotation;

    float backUpT = 0.0f;

    void Awake()
    {
        subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera)?.GetComponent<Camera>();
        subCamera.transform.position = cameraMosion.inofomations[0].positions[0];
        subCamera.transform.eulerAngles = cameraMosion.inofomations[0].lotations[0];

        backupPosition = subCamera.transform.position;
        backupRotation = subCamera.transform.rotation;
    }

    void OnEnable()
    {
        if (subCamera == null)
        {
            subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera)?.GetComponent<Camera>();
        }

        if (cameraMosion != null && cameraMosion.inofomations.Count > 0)
        {
            // �ĊJ���Ƀo�b�N�A�b�v�̈ʒu�Ɖ�]���Z�b�g
            if (backupPosition != Vector3.zero || backupRotation != Quaternion.identity)
            {
                subCamera.transform.position = backupPosition;
                subCamera.transform.rotation = backupRotation;
            }

            // ���[�V�����𑱂�����ĊJ
            if (!isMoving && subCamera != null)
            {
                motionCoroutine = StartCoroutine(MoveAndRotateCamera());
            }
        }

        mosionStart = true;
    }

    private void Update()
    {
        if (mosionStart && motionCoroutine == null)
        {
            mosionStart = false;
            motionCoroutine = StartCoroutine(MoveAndRotateCamera());
        }
    }

    void OnDisable()
    {
        // ���݂̃R���[�`�����~
        if (motionCoroutine != null)
        {
            StopCoroutine(motionCoroutine);
            motionCoroutine = null;
        }

        // ���݂̃J�����̈ʒu�Ɖ�]���o�b�N�A�b�v
        if (subCamera != null)
        {
            backupPosition = subCamera.transform.position;
            backupRotation = subCamera.transform.rotation;
        }

        mosionStart = false;
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

            float t = backUpT;

            // �|�W�V�����Ɖ�]����
            while (t < 1f)
            {
                t += Time.deltaTime * currentMotion.moveSpeed;
                backUpT = t;
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
        backUpT = 0.0f;


        mosionStart = true;
        motionCoroutine = null;
    }
}