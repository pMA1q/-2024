using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CutIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject rootObject = transform.root.gameObject;
        if (rootObject.GetComponent<CS_PerformanceFinish>() == null)
        {
            rootObject.AddComponent<CS_PerformanceFinish>().DestroyConfig(true, 3f); ;//ˆø”2:ƒvƒŒƒnƒu‚ğÁ‚·‚Ü‚Å‚ÌŠÔ(•b)
        }
    }
}
