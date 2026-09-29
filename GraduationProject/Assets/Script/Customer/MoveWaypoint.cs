using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// NPCの移動プログラム
/// </summary>
public class MoveWaypoint : MonoBehaviour
{
    [Header("WaypointManagerのscriptをアタッチ"), SerializeField]
    private WaypointManager m_Manager;

    [Header("NavMeshAgentをアタッチ"), SerializeField]
    private NavMeshAgent m_agent;

    [Header("どのWaypointに向かっているか"), SerializeField]
    private int m_currentIndex = 0;

    /// <summary>
    /// 経路が確定して移動中かどうか
    /// </summary>
    private bool m_isMoving = false;

    /// <summary>
    /// 開始
    /// </summary>
    [System.Obsolete]
    private void Start()
    {
        //マネージャー設定なければ最も近いものを探す
        if (m_Manager == null)
        {
            m_Manager = FindClosestManager();
            if (m_Manager == null)
            {
                Debug.LogError("WaypointManagerが見つかりません。");
            }
        }

        //コンポーネント取得
        m_agent = GetComponent<NavMeshAgent>();

        //最初のPointへ
        MoveToNextPoint();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        //NavMeshが有効かどうか
        if (m_agent.enabled)
        {
            // まだ経路計算中なら何もしない
            if (m_agent.pathPending)
                return;

            // 経路が確定したらフラグをON
            if (!m_isMoving && m_agent.hasPath)
            {
                m_isMoving = true;
            }

            // 移動中かつ目的地に到着したら次のPointへ
            if (m_isMoving && m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                m_isMoving = false;
                MoveToNextPoint();
            }
        }
    }
    /// <summary>
    /// 現在のPointに着いたら次のPointへ
    /// </summary>
    void MoveToNextPoint()
    {
        if (m_Manager == null || m_Manager.m_Waypoints.Length == 0)
        {
            Debug.Log("WaypointManagerのscriptが設定されていません。");
            return;
        }

        // 最後の waypoint に到達チェック
        if (m_currentIndex >= m_Manager.m_Waypoints.Length)
        {
            // 削除
            Destroy(gameObject);
            return;
        }

        // 次の移動ポイントセット
        m_agent.destination = m_Manager.m_Waypoints[m_currentIndex].position;

        // フラグリセット（経路計算待ち）
        m_isMoving = false;

        // 次の index へ
        m_currentIndex++;
    }
    /// <summary>
    /// シーン内のWaypointManagerの中で最も近いものを探す
    /// </summary>
    /// <returns>最も近いWaypointManager</returns>
    [System.Obsolete]
    WaypointManager FindClosestManager()
    {
        //シーンにある全てのWaypointManagerを取得
        WaypointManager[] managers = FindObjectsOfType<WaypointManager>();

        //最も近いものを保存する変数
        WaypointManager closest = null;

        //最小距離の初期値
        float minDist = Mathf.Infinity;

        //各WaypointManagerを調べる
        foreach (var m in managers)
        {
            //距離計算
            float dist = Vector3.Distance(transform.position, m.transform.position);

            //今までで最も近いものなら更新
            if (dist < minDist)
            {
                minDist = dist;
                closest = m;
            }
        }

        //最終的に最も近いものを返す
        return closest;
    }
}
