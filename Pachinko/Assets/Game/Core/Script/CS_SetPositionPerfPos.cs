using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPositionPerfPos : MonoBehaviour
{
    [SerializeField, Header("�T�u�J�����̈ʒu����+���̒l")]
    private Vector3 mOffsetPos= new Vector3(0f, 0f, 10f);

    [SerializeField, Header("���v�l+���̒l")]
    private Vector3 mSumPos;

    Camera mSubCamera;

    void Start()
    {
        // "SubCamera"�Ƃ������O�̃J����������
        mSubCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCamera�����������ꍇ�ACanvas��Render Camera�ɐݒ�
        if (mSubCamera != null)
        {
            Vector3 newPos = this.transform.position;
            newPos = mSubCamera.transform.position + mOffsetPos;
            this.transform.position = newPos;
        }
        else
        {
            Debug.LogWarning("SubCamera��������܂���ł����B");
        }

       // Destroy(this);
    }

    private void Update()
    {
        this.transform.position = mSubCamera.transform.position + mOffsetPos;
    }
}

