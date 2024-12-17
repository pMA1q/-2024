//---------------------------------
//OnCollisionでパチンコボールが衝突時の処理
//担当者：中島
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftAttackTrigger : MonoBehaviour
{
    // 力の大きさを設定する
    [SerializeField]
    private float forceAmount = 10f;

    // 力を加える方向を列挙
    public enum ForceDirection
    {
        Forward,   // 奥行方向 (Z+)
        Backward,  // 手前方向 (Z-)
        Left,      // 左方向 (X-)
        Right,     // 右方向 (X+)
        Up         // 上方向 (Y+)
    }

    // 力を加える方向
    [SerializeField]
    private ForceDirection forceDirection = ForceDirection.Forward;

    // トリガーに何かが触れたときに呼ばれるメソッド
    private void OnTriggerStay (Collider other)
    {
        // 触れたオブジェクトのタグが"A"の場合
        if (other.CompareTag("Pachinko Ball"))
        {
            // Rigidbodyコンポーネントを取得する
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // Rigidbodyが存在する場合のみ力を加える
            if (rb != null)
            {
                // 力の方向を決定する
                Vector3 force = Vector3.zero;

                switch (forceDirection)
                {
                    case ForceDirection.Forward:
                       
                        force = Vector3.forward * forceAmount;
                        
                        break;
                    case ForceDirection.Backward:
                        force = Vector3.back * forceAmount;
                        break;
                    case ForceDirection.Left:
                        force = Vector3.left * forceAmount;
                        break;
                    case ForceDirection.Right:
                        force = Vector3.right * forceAmount;
                        break;
                    case ForceDirection.Up:
                        force = Vector3.up * forceAmount;
                        break;
                }
                
                // 指定した方向に力を加える
                //rb.AddForce(force, ForceMode.Impulse);
                rb.velocity = force;
            }
        }
    }
}

