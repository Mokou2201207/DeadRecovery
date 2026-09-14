using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// テレポートする処理
/// </summary>
public class Teleport : MonoBehaviour
{
    /// <summary>
    /// ロビーからステージにテレポートする処理
    /// </summary>
    public void StageTeleport()
    {
        Debug.Log("ロビーからステージに移動");
        SceneManager.LoadScene("Stage1");
    }
}
