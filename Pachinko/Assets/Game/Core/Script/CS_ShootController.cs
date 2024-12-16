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
    private Color NoSelectColor;
    private Color selectColor = new Color(1f, 1f, 1f, 1f);
    private Color selectColorTx = new Color(1f, 0f, 0f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        LeftShootButton();
        leftShootButton.onClick.AddListener(LeftShootButton);
        rightShootButton.onClick.AddListener(RightShootButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LeftShootButton()
    {
        ballSpawn.forceY = 370;

        leftShootButton.GetComponent<Image>().color = selectColor;
        Text buttonText = leftShootButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = selectColorTx;
        }

        rightShootButton.GetComponent<Image>().color = NoSelectColor;
        buttonText = rightShootButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = NoSelectColor;
        }
    }

    private void RightShootButton()
    {
        ballSpawn.forceY = 640;
        leftShootButton.GetComponent<Image>().color = NoSelectColor;

        Text buttonText = leftShootButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = NoSelectColor;
        }

        rightShootButton.GetComponent<Image>().color = selectColor;
        buttonText = rightShootButton.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = selectColorTx;
        }
    }
}
