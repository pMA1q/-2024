using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_BoundBall : MonoBehaviour
{

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("��������");
		if (collision.gameObject.CompareTag("Pachinko Ball"))
        {
			//Y�������ɏ�ɓ����͂�^����
			collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
			collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 3f, ForceMode.Impulse);
			Debug.Log("�{�[�����o�E���h");
		}
			
	}
}
