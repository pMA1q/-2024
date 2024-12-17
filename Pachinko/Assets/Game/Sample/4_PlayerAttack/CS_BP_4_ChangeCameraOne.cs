using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CS_BP_4_ChangeCameraOne : MonoBehaviour
{
    [SerializeField, Header("カメラが切り替わるまでの時間")]
    private float mCameraChangeTimer = 2f;
    [SerializeField, Header("切り替わったときのカメラの位置")]
    private Vector3 mPosition;
    [SerializeField, Header("切り替わったときのカメラの回転")]
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


        // ポジションの取得

        Vector3 targetPosition = mPosition;

        // 回転の取得
        Quaternion targetRotation = Quaternion.Euler(mRotation);

        // ポジション補間
        subCamera.transform.position = targetPosition;

        // 回転補間
        subCamera.transform.rotation = targetRotation;
    }


}