using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CS_BP_4_ChangeCameraOne : MonoBehaviour
{
    [SerializeField, Header("�J�������؂�ւ��܂ł̎���")]
    private float mCameraChangeTimer = 2f;
    [SerializeField, Header("�؂�ւ�����Ƃ��̃J�����̈ʒu")]
    private Vector3 mPosition;
    [SerializeField, Header("�؂�ւ�����Ƃ��̃J�����̉�]")]
    private Vector3 mRotation;

    private Camera subCamera;
    // Start is called before the first frame update
    void Start()
    {
        subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera)?.GetComponent<Camera>();
        StartCoroutine(ChangeCameraTrans());
    }


    private IEnumerator ChangeCameraTrans()
    {
        yield return new WaitForSeconds(mCameraChangeTimer);


        // �|�W�V�����̎擾

        Vector3 targetPosition = mPosition;

        // ��]�̎擾
        Quaternion targetRotation = Quaternion.Euler(mRotation);

        // �|�W�V�������
        subCamera.transform.position = targetPosition;

        // ��]���
        subCamera.transform.rotation = targetRotation;
    }


}