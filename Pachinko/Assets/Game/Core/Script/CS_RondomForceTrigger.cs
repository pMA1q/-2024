using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RondomForceTrigger : MonoBehaviour
{
    public float forceStrength = 100f;
    public float forceAngleRange = 30f; // ±30度の範囲でランダム

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pachinko Ball")) // ボールにTagを設定
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ランダムな角度で力を加える
                float angle = Random.Range(-forceAngleRange, forceAngleRange);
                Vector3 forceDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                rb.AddForce(forceDirection * forceStrength, ForceMode.Impulse);
            }
        }
    }
}
