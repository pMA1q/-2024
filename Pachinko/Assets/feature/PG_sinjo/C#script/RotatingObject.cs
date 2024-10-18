using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject windmill; // Windmillオブジェクト
    public GameObject cylinder; // Cylinderオブジェクト
    public GameObject cylinder2; // Cylinderオブジェクト

    void Start()
    {
        // WindmillとCylinderのColliderを取得
        Collider windmillCollider = windmill.GetComponent<Collider>();
        Collider cylinderCollider = cylinder.GetComponent<Collider>();
        Collider cylinderCollider2 = cylinder.GetComponent<Collider>();

        // 2つのCollider間の衝突を無視する
        Physics.IgnoreCollision(windmillCollider, cylinderCollider);
        Physics.IgnoreCollision(windmillCollider, cylinderCollider2);
        rb = GetComponent<Rigidbody>();
    }

    // 衝突が発生した時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
            // 衝突点に応じてトルク（回転力）を加える
            Vector3 impactPoint = collision.contacts[0].point;
            Vector3 direction = transform.position - impactPoint; // 衝突点からオブジェクト中心へのベクトル
            rb.AddTorque(direction * 100f); // 回転力を加える
            //Debug.Log("風車衝突");
        }
    }
}
