using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BallSpeedController : MonoBehaviour
{
    public float speedMultiplier = 2f; // 玉の速度倍率
    private Rigidbody rb;

    private Vector3 mVelocity;
    

    bool addSpeed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AddVel());
    }

    private IEnumerator AddVel()
    {
        yield return new WaitForSeconds(2f);
        // 現在の速度を取得
        mVelocity = rb.velocity * speedMultiplier;
        addSpeed = true;
    }

    void FixedUpdate()
    {
        if (!addSpeed) { return; }
        // 玉の速度を増加
        rb.velocity = mVelocity;
    }
}
