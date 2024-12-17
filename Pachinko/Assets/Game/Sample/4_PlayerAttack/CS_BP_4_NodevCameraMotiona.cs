//スクリプトカメラモーション
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_4_NodevCameraMotiona : MonoBehaviour
{

    [SerializeField]
    private Vector3 mStartPosition;
    [SerializeField]
    private Vector3 mEndPosition;

    [SerializeField]
    private Vector3 mStartRotation;
    [SerializeField]
    private Vector3 mEndRotation;

    [SerializeField]
    private float mMoveSpeed = 1f;

    private Camera subCamera;


    private Coroutine motionCoroutine; // カメラモーションのコルーチン


    float backUpT = 0.0f;

    void Awake()
    {
        subCamera = GameObject.Find(CS_CommonData.Obj3D_RenderCamera)?.GetComponent<Camera>();
        subCamera.transform.position = mStartPosition;
        subCamera.transform.eulerAngles = mStartRotation;
    }

    void OnEnable()
    {
        StartCoroutine(MoveAndRotateCamera());
    }

    private IEnumerator MoveAndRotateCamera()
    {

        // ポジションの取得
        Vector3 startPosition = subCamera.transform.position;
        Vector3 targetPosition = mEndPosition;

        // 回転の取得
        Quaternion startRotation = subCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(mEndRotation);

        float t = backUpT;

        // ポジションと回転を補間
        while (t < 1f)
        {
            t += Time.deltaTime * mMoveSpeed;
            backUpT = t;
            // ポジション補間
            subCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // 回転補間
            subCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        Destroy(this);//このコンポーネントを消す
    }


}