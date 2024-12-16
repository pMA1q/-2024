using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_CommonData : MonoBehaviour
{
    public CS_LeftAttakerOpenClose mLeftAttaker;

    readonly public static string Obj3D_RenderCamera = "FCamera";
    readonly public static string BigControllerName = "BigController";
    //�����W�t���O
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }

    public void ButtonsInteractable()
    {
        Canvas canvas = GameObject.Find("ButtonCanvas").GetComponent<Canvas>();
        Button[] buttons = canvas.GetComponentsInChildren<Button>();

        //�{�^���𖳌��ɂ���
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

      
    }
}
