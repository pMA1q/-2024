using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyLastAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject mNeraeCanvas;

    [SerializeField]
    private GameObject mEffect;

  
    
    [SerializeField]
    private GameObject mBack;

    [SerializeField]
    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        mNeraeCanvas.transform.SetParent(null);
        mBack.SetActive(false);
        Enemy.SetActive(false);
        StartCoroutine(LastAttack());
    }

    private IEnumerator LastAttack()
    {
       
        mEffect.SetActive(true);

        yield return new WaitForSeconds(2f);
        Enemy.SetActive(true);
        mBack.SetActive(true);
        Destroy(this.gameObject);
    }

    public void StartLastAttack()
    {
        StartCoroutine(LastAttack());
    }
}
