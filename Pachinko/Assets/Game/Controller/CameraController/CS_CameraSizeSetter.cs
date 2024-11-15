using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(Camera))]
public class CS_CameraSizeSetter : MonoBehaviour
{
    [SerializeField] Vector2 targetResolution;
    [SerializeField] float targetCameraSize;

    [SerializeField] bool isUpdate;

    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();

        SetCameraSize();
    }

    void Update()
    {
        if (isUpdate)
        {
            SetCameraSize();
        }
    }

    void SetCameraSize()
    {
        var targetRatio = targetResolution.y / targetResolution.x;
        var currentRatio = (float)Screen.height / (float)Screen.width;

        var targetSize = (targetCameraSize * currentRatio) / targetRatio;
        _camera.orthographicSize = targetSize;
    }
}



[ExecuteAlways]
public class CameraResizer : MonoBehaviour
{
    [SerializeField] Vector2 targetResolution;
    [SerializeField] float targetOrthographicSize = 1;

    Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ResizeCameraOrthographicSize();
    }

    void ResizeCameraOrthographicSize()
    {
        var currentResolution = new Vector2(Screen.width, Screen.height);
        var targetAspect = targetResolution.x / targetResolution.y;
        var currentAspect = currentResolution.x / currentResolution.y;

        if (currentAspect >= targetAspect)
        {
            camera.orthographicSize = targetOrthographicSize;
            return;
        }

        var orthoGraphicSize = targetOrthographicSize * (targetAspect / currentAspect);
        camera.orthographicSize = orthoGraphicSize;
    }
}
