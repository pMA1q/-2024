using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [Header("���W(10000,0,0)�����_�Ƃ����Ƃ��̃|�W�V����")]
    [SerializeField, Header("�I�u�W�F�N�g�̏����ʒu")]
    private Vector3 mObjectPos= new Vector3(10000f, 0f, 10f);

    [SerializeField, Header("�J�����̃|�W�V����")]
    private Vector3 mCameraPos = new Vector3(10000f, 0f, 0f);
    [SerializeField, Header("�J�����̉�]")]
    private Vector3 mCameraRot;

    Camera mSubCamera;

    void Start()
    {
        // "SubCamera"�Ƃ������O�̃J����������
        mSubCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCamera�����������ꍇ�ACanvas��Render Camera�ɐݒ�
        if (mSubCamera != null)
        {
            Vector3 newPos = mObjectPos;
            this.transform.position = newPos;
            mSubCamera.transform.position = mCameraPos;
            mSubCamera.transform.eulerAngles = mCameraRot;
        }
        else
        {
            Debug.LogError("SubCamera��������܂���ł����B");
        }

       // Destroy(this);
    }
}

