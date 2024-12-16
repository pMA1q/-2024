using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_CommonData : MonoBehaviour
{
    readonly public static string Obj3D_RenderCamera = "FCamera";
    readonly public static string BigControllerName = "BigController";
    readonly public static string MainCanvasName = "ButtonCanvas";

    //�A�^�b�J�[
    public CS_AttakerOpenClose mLeftAttaker;
    public CS_AttakerOpenClose mRightAttaker;
    public CS_AttakerOpenClose RightAttaker
    {
        get { return mRightAttaker; }
    }

    public int Dedama = 0;

    //�����W�t���O
    private bool IsNoDevelopment = false;
    public bool NoDevelpment
    {
        set { IsNoDevelopment = value; }
        get { return IsNoDevelopment; }
    }

    //�{�^���擾
    public void ButtonsInteractable()
    {
        Canvas canvas = GameObject.Find(MainCanvasName).GetComponent<Canvas>();
        Button[] buttons = canvas.GetComponentsInChildren<Button>();

        //�{�^���𖳌��ɂ���
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    // ���̃A�^�b�J�[���J��
    public void LeftAttakerStart(int _round)
    {
        mLeftAttaker.AttakerOpen(_round);
    }

    // �E�̃A�^�b�J�[���J��
    public void RightAttakerStart(int _round)
    {
        mRightAttaker.AttakerOpen(_round);
    }

    // V����
    public void V_SpotOpen()
    {
        mRightAttaker.AttakerOpen_V_Bounus();
    }
}
