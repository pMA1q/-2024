//-----------------------------
//プレイヤー攻撃(ボスフェーズ項目番号:7)
//-----------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BP_4_Player : MonoBehaviour
{
    [SerializeField, Header("EnemyのTransform")]
    private Transform mTarget;

    private bool IsMoving = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
