//-----------------------------
//プレイヤー攻撃(ボスフェーズ項目番号:4)
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
        if (!IsMoving) { return; }
        float moveSpeed = 15f;
        transform.position = Vector3.MoveTowards(transform.position, mTarget.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            IsMoving = false;
        }
    }
}
