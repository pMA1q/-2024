using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Dedama : MonoBehaviour
{
    [SerializeField]
    private Text mTxDeedama;
    [SerializeField]
    private Color mWinColor;
    [SerializeField]
    private Color mLoseColor;
    private CS_CommonData data;//ã§í ÉfÅ[É^
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();  
    }

    // Update is called once per frame
    void Update()
    {
        mTxDeedama.text  = data.Dedama.ToString();

        if(data.Dedama < 0)
        {
            mTxDeedama.color = mLoseColor;
        }
        else
        {
            mTxDeedama.color = mWinColor;
        }
    }
}
