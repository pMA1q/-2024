using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SetPsitionPerfPos : MonoBehaviour
{
    [SerializeField, Header("�T�u�J�����̈ʒu����+���̒l")]
    private Vector3 mOffsetPos;

    void Start()
    {
        // "SubCamera"�Ƃ������O�̃J����������
        Camera subCamera = GameObject.Find("SubCamera")?.GetComponent<Camera>();

        // SubCamera�����������ꍇ�ACanvas��Render Camera�ɐݒ�
        if (subCamera != null)
        {
            Vector3 newPos = this.transform.position;
            newPos = subCamera.transform.position + mOffsetPos;
            this.transform.position = newPos;
        }
        else
        {
            Debug.LogWarning("SubCamera��������܂���ł����B");
        }

       // Destroy(this);
    }
}

