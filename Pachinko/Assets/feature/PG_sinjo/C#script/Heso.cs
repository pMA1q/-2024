using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heso : MonoBehaviour
{
    private const int TotalValues = 65536;
    //private const int WinningValues = 36553;
    private const int WinningValues = 10000;
    private CS_Controller csController;

    readonly int MAX_STOCK = 5;
    public List<int[]> stock = new List<int[]>();
    [SerializeField]
    GameObject[] stockObjects = null;

    // Start is called before the first frame update
    void Start()
    {
        // CS_Controllerがアタッチされているオブジェクトを取得
        GameObject controllerObject = GameObject.Find("BigController");

        // CS_Controllerコンポーネントを取得
        if (controllerObject != null)
        {
            csController = controllerObject.GetComponent<CS_Controller>();
        }
        else
        {
            Debug.LogError("CS_ControllerObjectが見つかりません！");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 抽選メソッド
    // 抽選メソッド
    void Lottery()
    {
        if (stock.Count < MAX_STOCK) // == から < に変更。MAX_STOCK未満の場合に追加する
        {
            int randomValue = Random.Range(0, TotalValues);
            int[] i;

            // 当たり判定
            if (randomValue < WinningValues)
            {
                Debug.Log("当たり！");
                i = new int[] { 1, 1, 1 };  // 当たりのときは固定値
            }
            else
            {
                Debug.Log("ハズレ");
                // 通常はランダムに生成
                i = new int[] { Random.Range(1, 8), Random.Range(1, 8), Random.Range(1, 8) };

                // リーチ判定 (左右の数字が一致しているかどうか)
                if (i[0] == i[2])
                {
                    Debug.Log("リーチ発生！");

                    // 真ん中の数字をリーチ数字+1に変更
                    i[1] = (i[0] + 1) % 10;  // リーチの数字+1にして、範囲を1-9に制限
                }
            }

            // ストックに追加
            stock.Add(i);
            Debug.Log("Stock added. Current stock: " + csController.GetStock());

            // 非アクティブなstockObjectをアクティブにする
            foreach (var v in stockObjects)
            {
                if (!v.activeInHierarchy)
                {
                    v.SetActive(true); // 非アクティブなオブジェクトをアクティブにする
                    break;
                }
            }
        }
    }


    // ストックを無効化するメソッド
    public void DisableStock()
    {
        //if (stock.Count > 0) // ストックがある場合のみ削除
        //{
        //    stock.RemoveAt(0);
        //}
        //else
        //{
        //    Debug.LogWarning("ストックが空です");
        //}

        // stockObjectsの配列サイズをチェックしてループ
        for (int i = stockObjects.Length; i > 0; i--)
        {
            if (i - 1 >= 0 && stockObjects[i - 1].activeInHierarchy)
            {
                stockObjects[i - 1].SetActive(false); // アクティブなオブジェクトを非アクティブにする
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pachinko Ball"))
        {
            //Debug.Log("BallがPlaneを通過しました");
            // コルーチンが実行されていない、かつheso.stock.Countが0ではない場合、コルーチンを開始する
            Lottery();
        }
    }
}
