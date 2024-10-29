using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BoundBall : MonoBehaviour
{

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
			//Y²•ûŒü‚Éí‚É“¯‚¶—Í‚ğ—^‚¦‚é
			collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 3f, ForceMode.Impulse);
			collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 3f, ForceMode.Impulse);
		}
			
	}
}
