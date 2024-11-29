using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CS_CameraZoom : MonoBehaviour
{
    [SerializeField, Header("�t���I�u�W�F�N�g")]
    private Transform mDisplay;

    [SerializeField, Header("�Y�[���A�E�g�ő勗��")]
    private float mZoomOutDistance;

    [SerializeField, Header("�Y�[���C���ŏ�����")]
    private float mZoomInDistance;

    [SerializeField, Header("�Y�[���X�s�[�h")]
    private float mZoomSpeed = 100f;

    public float nowDistance;

    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    private bool hasScaled = false; // �X�P�[���ύX�ς݃t���O

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

        // �^�b�v���o
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
        // �^�b�v�܂��̓N���b�N�����o
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // "DisplayLayer"������Ώۂɂ��郌�C���[�}�X�N���쐬
        int layerMask = LayerMask.GetMask("DisplayLayer");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("�^�b�v���o");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                float movepow = 4.0f;
                float addscalepow = 1.5f;
                Debug.Log($"���C�����������I�u�W�F�N�g: {hit.transform.name}");
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