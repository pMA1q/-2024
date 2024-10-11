using System.Collections;
using UnityEngine;

public class CS_SphereSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; // 生成するオブジェクトのプレハブ
    public Transform spawnPoint;     // オブジェクトの生成位置
    public float spawnInterval = 0.6f; // 1秒あたり1.67回生成するため、0.6秒ごとに生成
    [SerializeField] float forceY;


    Vector3 force = new Vector3(0.0f, forceY, 0.0f);

    private void Start()
    {

        // コルーチンを開始して、指定の間隔でオブジェクトを生成する
        StartCoroutine(SpawnObjects());

    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // オブジェクトを指定の位置に生成
            var Ball = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            var rb = Ball.GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Impulse);

            // 指定した時間待機
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
