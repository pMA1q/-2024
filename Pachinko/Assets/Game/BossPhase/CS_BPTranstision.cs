using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class CS_BPTranstision : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private CS_Controller bigController;
    // Start is called before the first frame update
    void Start()
    {
        //bigController.ChangePhase(CS_Controller.PACHINKO_PHESE.BOSS);
        bigController = GameObject.Find("BigController").GetComponent<CS_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//ミッションフェーズをフェードアウトしてからボスフェーズへフェードイン
//BPController呼び出す(プレハブ/生成)
//ミッションフェーズからプレイヤーステータスを参照
