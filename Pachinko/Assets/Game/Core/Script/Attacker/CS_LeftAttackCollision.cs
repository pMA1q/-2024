using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_LeftAttackCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが "PachinkoBall" のタグを持つか確認
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // 衝突したオブジェクトのリジッドボディを取得
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 物理マテリアルのバウンドを0にする
                Collider collider = collision.gameObject.GetComponent<Collider>();
                if (collider != null && collider.material != null)
                {
                    collider.material.bounciness = 0f; // 反発係数を0に
                    collider.material.bounceCombine = PhysicMaterialCombine.Minimum; // 反発の計算を最小値に設定
                }
            }
        }
    }
}
