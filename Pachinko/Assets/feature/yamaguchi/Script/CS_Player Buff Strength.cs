using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerBuffStrength : MonoBehaviour
{
    public float baseAttackPower = 10f;
    private float currentAttackPower;
    private bool isBuffed = false;

    private float buffChance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackPower = baseAttackPower;
    }

    public void TryApplybuff()
    {
        if(Random.value < buffChance) 
        {
           
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
