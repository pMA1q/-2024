using System.Collections;
using UnityEngine;

public class CS_SphereSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; // ��������I�u�W�F�N�g�̃v���n�u
    public Transform spawnPoint;     // �I�u�W�F�N�g�̐����ʒu
    public float spawnInterval = 0.6f; // 1�b������1.67�񐶐����邽�߁A0.6�b���Ƃɐ���
    [SerializeField] float forceY;


    Vector3 force = new Vector3(0.0f, forceY, 0.0f);

    private void Start()
    {

        // �R���[�`�����J�n���āA�w��̊Ԋu�ŃI�u�W�F�N�g�𐶐�����
        StartCoroutine(SpawnObjects());

    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // �I�u�W�F�N�g���w��̈ʒu�ɐ���
            var Ball = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            var rb = Ball.GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Impulse);

            // �w�肵�����ԑҋ@
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
