using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ShootController : MonoBehaviour
{
    [SerializeField]
    private Button leftShootButton;
    [SerializeField]
    private Button rightShootButton;
    [SerializeField]
    private CS_SphereSpawn ballSpawn;
    [SerializeField]
    private float mLeftForce = 3000f;
    [SerializeField]
    private float mRightForce = 640f;
    [SerializeField]
    private Color NoSelectColor;
    private Color selectColor = new Color(1f, 1f, 1f, 1f);
    private Color selectColorTx = new Color(1f, 0f, 0f, 1f);
   
    private void OnEnable()
    {
        LeftShootButton();
        leftShootButton.onClick.AddListener(LeftShootButton);
        if (rightShootButton != null)
        {
            rightShootButton.onClick.AddListener(RightShootButton);
        }
        else
        {
            Debug.LogError("rightShootButton is null. Check the Button assignment.");
        }
    }

    private void LeftShootButton()
    {
        ballSpawn.forceY = mLeftForce;

        leftShootButton.GetComponent<Image>().color = selectColor;
       
        rightShootButton.GetComponent<Image>().color = NoSelectColor;
    }

    private void RightShootButton()
    {
        ballSpawn.forceY = mRightForce;
        leftShootButton.GetComponent<Image>().color = NoSelectColor;
        rightShootButton.GetComponent<Image>().color = selectColor;   
    }
}
