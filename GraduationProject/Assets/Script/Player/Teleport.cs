using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// テレポート制御
/// </summary>
public class Teleport : MonoBehaviour
{
    /// <summary>
    /// ロビーからステージへテレポート
    /// </summary>
    public void StageTeleport()
    {
        Debug.Log("ロビーからステージへ移動");
        SceneManager.LoadScene("Stage1");
    }
}
