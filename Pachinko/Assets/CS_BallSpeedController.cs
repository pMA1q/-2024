using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BallSpeedController : MonoBehaviour
{
    public float speedMultiplier = 2f; // �ʂ̑��x�{��
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
        // ���݂̑��x���擾
        mVelocity = rb.velocity * speedMultiplier;
        addSpeed = true;
    }

    void FixedUpdate()
    {
        if (!addSpeed) { return; }
        // �ʂ̑��x�𑝉�
        rb.velocity = mVelocity;
    }
}
