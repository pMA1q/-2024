using System.Collections;
using UnityEngine;

public class CS_SphereSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; // 生成するオブジェクトのプレハブ
    public Transform spawnPoint;     // オブジェクトの生成位置
    public float spawnInterval = 0.6f; // 1秒あたり1.67回生成するため、0.6秒ごとに生成
    [Header("玉の発射速度")]
    [SerializeField] public float forceY;


    private Vector3 force;

    private void Start()
    {
        // forceYを使用してVector3の力を初期化
        force = new Vector3(0.0f, forceY, 0.0f);

        // コルーチンを開始して、指定の間隔でオブジェクトを生成する
        StartCoroutine(SpawnObjects());

    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            force = new Vector3(0.0f, forceY, 0.0f);
            // オブジェクトを指定の位置に生成
            var Ball = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            CS_CommonData data = GameObject.Find(CS_CommonData.BigControllerName).GetComponent<CS_CommonData>();
            data.Dedama--;
            var rb = Ball.GetComponent<Rigidbody>();
            rb.AddForce(force, ForceMode.Impulse);

            // 指定した時間待機
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
