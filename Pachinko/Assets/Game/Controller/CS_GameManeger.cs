//---------------------------------
//ゲームマネージャー
//担当者：野崎
//---------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour
{
    private CSO_PlayerStatus playerStatus;

    void Start()
    {
        // 初期ステータス設定
        playerStatus = new CSO_PlayerStatus(initialHp: 100, initialAttack: 10, initialDefense: 10);

        // ゲーム開始
        StartGame();
    }

    // ゲーム進行
    public void StartGame()
    {
        PreparePhase();
        MissionPhase();
        BossPhase();
        EndGame();
    }

    // 準備フェーズ（アイテム収集、鍛錬、討伐ミッションの選択）
    private void PreparePhase()
    {
        Debug.Log("準備フェーズ開始");
        // ミッション選択などの処理
    }

    // ミッションフェーズ（20+α回転でステータスを保持・更新）
    private void MissionPhase()
    {
        Debug.Log("ミッションフェーズ開始");

        for (int i = 0; i < 20; i++)  // 回転数に応じてステータスを更新
        {
            // ランダムにステータスを更新（例としてランダムな数値を使用）
            int randomHpChange = UnityEngine.Random.Range(-5, 10);
            int randomAttackChange = UnityEngine.Random.Range(0, 5);
            int randomDefenseChange = UnityEngine.Random.Range(0, 3);

            playerStatus.UpdateStatus(randomHpChange, randomAttackChange, randomDefenseChange);
        }
    }

    // ボスフェーズ（確立したステータスを呼び出して抽選）
    private void BossPhase()
    {
        Debug.Log("ボスフェーズ開始");

        for (int i = 0; i < 10; i++)  // ボスフェーズでの10回転
        {
            // ランダムでボスと戦う結果
            int randomBossAttack = UnityEngine.Random.Range(0, 15);
            playerStatus.UpdateStatus(-randomBossAttack, 0, 0);
        }
    }

    // ゲーム終了時の処理（リセット）
    private void EndGame()
    {
        Debug.Log("ゲーム終了、ステータスをリセットする");
        playerStatus.ResetStatus();  // ステータスを初期状態に戻す
    }
}
