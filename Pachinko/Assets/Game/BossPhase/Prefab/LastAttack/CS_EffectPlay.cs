using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EffectPlay : MonoBehaviour
{
    [SerializeField]
    float playTimer = 1.0f;
    [SerializeField]
    private GameObject mSE;
    [SerializeField]
    private GameObject mRainbowFrame;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(PlayeEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayeEffect()
    {
        yield return new WaitForSeconds(playTimer);

        GetComponent<ParticleSystem>().Play();

        //Œø‰Ê‰¹‚ð–Â‚ç‚·
        if(mSE != null)
        {
            Instantiate(mSE, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        GetComponent<ParticleSystem>().Stop();
    }

}
