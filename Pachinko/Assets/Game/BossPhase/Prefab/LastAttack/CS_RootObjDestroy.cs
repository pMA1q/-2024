using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_RootObjDestroy : MonoBehaviour
{
    [SerializeField]
    float mTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObj());
    }

    private IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(mTimer);

        Destroy(transform.root.gameObject);
    }
}
