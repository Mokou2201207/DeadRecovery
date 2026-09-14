using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スポーン用の処理
/// </summary>
public class GameSpawnManager : MonoBehaviour
{
    [Header("スポーン設定")]
    [Header("プレイヤーのprefab"), SerializeField]
    private GameObject playerPrefab;
    [Header("スポーン位置"), SerializeField]
    private Transform spawnPoint;

    /// <summary>
    /// 関数でスポーンの処理を最初に実行
    /// </summary>
    private void Start()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// プレイヤーのスポーン処理
    /// </summary>
    private void SpawnPlayer()
    {
        //nullチェック
        if (playerPrefab!=null && spawnPoint!=null)
        {
            //プレイヤーのスポーン処理
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("プレイヤーのスポーンまたはprefabが入ってません。");
        }
    }
}
