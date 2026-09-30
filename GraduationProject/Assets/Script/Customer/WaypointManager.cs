using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPCの移動ポイントを管理するクラス
/// </summary>
public class WaypointManager : MonoBehaviour
{
    [Header("敵の移動ポイントを導入")]
    public Transform[] m_Waypoints;
    /// <summary>
    ///  子オブジェクトからWaypointを自動登録します
    /// </summary>
    void Awake()
    {
        // 子オブジェクト自動登録
        if (m_Waypoints == null || m_Waypoints.Length == 0)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t != transform)
                {
                    list.Add(t);
                }
            }
            m_Waypoints = list.ToArray();
            Debug.Log("子オブジェクトから自動でWaypointsを登録しました。");
        }
        else
        {
            Debug.Log("インスペクターでWaypointsが手動設定されています。");
        }
    }
}
