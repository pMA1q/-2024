using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Stock : MonoBehaviour
{
    readonly int MAX_STOCK = 5;
    private int mStockCount = 0;
    
    [SerializeField]
    GameObject[] stockObjects = null;

    public int Count
    {
        get
        {
            return mStockCount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    
    // ストックを無効化するメソッド
    public void DisableStock()
    {
        mStockCount--;

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
            AddStock();
            Destroy(other.gameObject);
        }
            
    }

    public void AddStock()
    {
        if (mStockCount < MAX_STOCK) // == から < に変更。MAX_STOCK未満の場合に追加する
        {
            mStockCount++;
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
}
