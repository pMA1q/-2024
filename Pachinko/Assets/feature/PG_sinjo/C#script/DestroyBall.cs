using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyBall : MonoBehaviour
{
    void Start()
    { }

    void Update()
    { }

    private void OnCollisionEnter(Collision collision) //ぶつかったら消える命令文開始
    {
        if (collision.gameObject.CompareTag("DestroyWall"))//さっきつけたTagutukeruというタグがあるオブジェクト限定で～という条件の下
        {
            Destroy(gameObject);//このゲームオブジェクトを消滅させる
        }
    }
}