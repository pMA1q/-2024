using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [Header("���W(10000,0,0)�����_�Ƃ����Ƃ��̃|�W�V����")]
    [SerializeField, Header("�T�u�J�����̈ʒu����+���̒l")]
    private Vector3 mObjectPos= new Vector3(0f, 0f, 10f);

    [SerializeField, Header("�J�����̃|�W�V����")]
    private Vector3 mCameraPos;

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
        }
        else
        {
            Debug.LogError("SubCamera��������܂���ł����B");
        }

       // Destroy(this);
    }

    private void Update()
    {
        this.transform.position = mSubCamera.transform.position + mOffsetPos;
    }
}

