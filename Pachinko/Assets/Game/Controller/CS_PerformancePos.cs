using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformancePos : MonoBehaviour
{
    [SerializeField, Header("演出の初期位置")]
    private Transform mSpawnPos;

    public Vector3 InitialPos
    {
        get
        {
            return mSpawnPos.position;
        }
    }

    private void Start()
    {
        Debug.Log("演出位置" + mSpawnPos.position);
        Destroy(mSpawnPos.gameObject.GetComponent<MeshRenderer>());
    }
}
