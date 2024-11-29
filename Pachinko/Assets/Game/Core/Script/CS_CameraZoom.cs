using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CS_CameraZoom : MonoBehaviour
{
    [SerializeField, Header("液晶オブジェクト")]
    private Transform mDisplay;

    [SerializeField, Header("ズームアウト最大距離")]
    private float mZoomOutDistance;

    [SerializeField, Header("ズームイン最小距離")]
    private float mZoomInDistance;

    [SerializeField, Header("ズームスピード")]
    private float mZoomSpeed = 100f;

    public float nowDistance;

    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    private bool hasScaled = false; // スケール変更済みフラグ

    void Start()
    {
        SetupButton("ButtonUp", () => isZoomingIn = true, () => isZoomingIn = false);
        SetupButton("ButtonDown", () => isZoomingOut = true, () => isZoomingOut = false);
        nowDistance = Vector3.Distance(this.transform.position, mDisplay.position);
    }

    void Update()
    {
        //if (isZoomingIn)
        //{
        //    ZoomIn();
        //}

        //if (isZoomingOut)
        //{
        //    ZoomOut();
        //}

        // タップ検出
        DetectTapOnDisplay();
    }

    private void SetupButton(string buttonName, UnityEngine.Events.UnityAction onPress, UnityEngine.Events.UnityAction onRelease)
    {
        Button button = GameObject.Find(buttonName).GetComponent<Button>();
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pressEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pressEntry.callback.AddListener((_) => onPress());
        trigger.triggers.Add(pressEntry);

        EventTrigger.Entry releaseEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        releaseEntry.callback.AddListener((_) => onRelease());
        trigger.triggers.Add(releaseEntry);
    }

    private void ZoomIn()
    {
        nowDistance = Vector3.Distance(this.transform.position, mDisplay.position);

        if (nowDistance <= mZoomInDistance) { return; }

        Vector3 normalizeVec = (mDisplay.position - this.transform.position).normalized;
        Vector3 nowCameraPos = this.transform.position;
        nowCameraPos += normalizeVec * mZoomSpeed * Time.deltaTime;
        this.transform.position = nowCameraPos;
    }

    private void ZoomOut()
    {
        if (nowDistance >= mZoomOutDistance) { return; }

        Vector3 normalizeVec = (mDisplay.position - this.transform.position).normalized;
        Vector3 nowCameraPos = this.transform.position;
        nowCameraPos -= normalizeVec * mZoomSpeed * Time.deltaTime;
        this.transform.position = nowCameraPos;

        nowDistance = Vector3.Distance(this.transform.position, mDisplay.position);
    }

    private void DetectTapOnDisplay()
    {
        // タップまたはクリックを検出
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // "DisplayLayer"だけを対象にするレイヤーマスクを作成
        int layerMask = LayerMask.GetMask("DisplayLayer");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("タップ検出");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                float movepow = 4.0f;
                float addscalepow = 1.5f;
                Debug.Log($"レイが当たったオブジェクト: {hit.transform.name}");
                if (!hasScaled) 
                { 
                    mDisplay.localScale *= addscalepow;
                    mDisplay.position -= (Camera.main.transform.forward * movepow);
                }
                else 
                { 
                    mDisplay.localScale /= addscalepow;
                    mDisplay.position += (Camera.main.transform.forward * movepow);
                }

                hasScaled = !hasScaled;

            }
        }
            
    }
}