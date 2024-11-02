using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Windmill : MonoBehaviour
{
    private Rigidbody rb;
   
    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Pachinko Ballとの衝突をチェック
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // 衝突点に応じてトルク（回転力）を加える
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 direction = transform.position - impactPoint;
            rb.AddTorque(direction * 100f);
        }
    }
}
