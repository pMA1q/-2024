using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Debuff : MonoBehaviour
{
    //元の攻撃力を保持する変数
    public float baseAttackPower;
    //現在の攻撃力(デバフやバフが適用された状態)
    public float currentAttackPower;

    // Start is called before the first frame update
    void Start()
    {
        //初期化 攻撃力は元の値から始まる
        currentAttackPower = baseAttackPower;
    }

    //攻撃力にデバフを適用    
    public void ApplyDebuff(float debuffPercentage)
    {
        //デバフの割合に応じて攻撃力を減少させる
        float debuffAmount = baseAttackPower * (debuffPercentage / 100);
        currentAttackPower = baseAttackPower - debuffAmount;

        Debug.Log("デバフが適用されました。現在の攻撃力:" + currentAttackPower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
