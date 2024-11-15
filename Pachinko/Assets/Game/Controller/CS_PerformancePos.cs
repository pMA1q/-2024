using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PerformancePos : MonoBehaviour
{
    [SerializeField, Header("���o�̏����ʒu")]
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
        Debug.Log("���o�ʒu" + mSpawnPos.position);
        Destroy(mSpawnPos.gameObject.GetComponent<MeshRenderer>());
    }
}
