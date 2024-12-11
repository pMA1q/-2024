using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CameraWander : MonoBehaviour
{
    [SerializeField]
    private float wanderSpeed = 2f; // �J�����̍��E�ړ����x
    [SerializeField]
    private float wanderRange = 3f; // �J�����̍��E�ړ��͈�
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 5f, -5f); // �Ώە�����̏����I�t�Z�b�g�ʒu
    [SerializeField]
    private Transform mPlayer;
    private Transform subCameraTransform;
    private Vector3 initialCameraPosition;
    private float timer = 0f;

   
    public void Init()
    {
        // Scene����"SubCamera"�Ƃ������O�̃J�������擾
        GameObject subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera);
        if (subCamera != null)
        {
            subCameraTransform = subCamera.transform;

            // �Ώە��̈ʒu����ɃJ�����̏����ʒu��ݒ�
            initialCameraPosition = mPlayer.position + offset;
            subCameraTransform.position = initialCameraPosition;
            Debug.Log("�J�����̈ʒu" + subCameraTransform.position);
            subCameraTransform.LookAt(transform.position); // �J������Ώە��Ɍ�����
        }
        else
        {
            Debug.LogError("SubCamera �� Scene ���Ɍ�����܂���ł����B�J���������m�F���Ă��������B");
        }

    }
    void Update()
    {
        if (subCameraTransform != null)
        {
            // ���Ԃɂ���ăJ�����̍��E�ړ����v�Z
            timer += Time.deltaTime * wanderSpeed;
            float offsetX = Mathf.Sin(timer) * wanderRange;

            // �J�����̐V�����ʒu���v�Z�i�����ʒu��X�������ɃX���C�h�j
            Vector3 newPosition = initialCameraPosition + mPlayer.right * offsetX;

            // �J�����ʒu���X�V
            subCameraTransform.position = newPosition;

            // �J��������ɑΏۃI�u�W�F�N�g�Ɍ�����
            subCameraTransform.LookAt(mPlayer.position);
        }
    }
}
