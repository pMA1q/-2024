using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CS_CameraSetAcpect : MonoBehaviour
{
	//[SerializeField]
	//Camera targetCamera;
	//[SerializeField]
	//Vector2 targetSize = new(9f, 16f);
	//[SerializeField]
	//bool autoSetOnStart = true;

	//public Camera TargetCamera { get { return targetCamera; } set { targetCamera = value; } }
	//public Vector2 TargetSize { get { return targetSize; } set { targetSize = value; } }

	//void Awake()
	//{
	//	if (autoSetOnStart)
	//	{
	//		SetAspectRatio();
	//	}
	//}

	//public void SetAspectRatio()
	//{
	//	if (targetCamera == null)
	//	{
	//		return;
	//	}

	//	float currentRatio = (float)Screen.width / Screen.height;
	//	float targetRatio = targetSize.x / targetSize.y;
	//	float scaleHeight = currentRatio / targetRatio;
	//	Rect rect = targetCamera.rect;

	//	if (scaleHeight < 1f)
	//	{
	//		rect.width = 1f;
	//		rect.height = scaleHeight;
	//		rect.x = 0;
	//		rect.y = (1f - scaleHeight) / 2f;
	//	}
	//	else
	//	{
	//		float scaleWidth = 1f / scaleHeight;
	//		rect.width = scaleWidth;
	//		rect.height = 1f;
	//		rect.x = (1f - scaleWidth) / 2f;
	//		rect.y = 0;
	//	}

	//	targetCamera.rect = rect;
	//}
	Camera _camera;

	[SerializeField] Vector2 targetResolution;      // 目標の解像度
	[SerializeField] bool isUpdate;                 // 毎フレーム画面をそろえるか

	private void Awake()
	{
		_camera = GetComponent<Camera>();
		SetAspect();
	}

	private void Update()
	{
		if (isUpdate)
		{
			SetAspect();
		}
	}

	void SetAspect()
	{
		var scrnAspect = (float)Screen.width / (float)Screen.height;        // 現在のアスペクト比
		var targAspect = targetResolution.x / targetResolution.y;           // 目標のアスペクト比

		var rate = targAspect / scrnAspect;     // 現在と目標との比率
		var rect = new Rect(0, 0, 1, 1);

		// 倍率が小さい場合、横をそろえる
		if (rate < 1)
		{
			rect.width = rate;
			rect.x = 0.5f - rect.width * 0.5f;
		}

		// 縦をそろえる
		else
		{
			rect.height = 1 / rate;
			rect.y = 0.5f - rect.height * 0.5f;
		}

		// 反映
		_camera.rect = rect;
	}
}
