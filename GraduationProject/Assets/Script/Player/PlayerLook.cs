using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー視点・インタラクション処理
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Header("カメラ設定")]
    [SerializeField] private Transform playerCamera;

    [Header("Playerから出すRayの距離")]
    [SerializeField] private float rayDistance = 100.0f;

    [Header("クロスヘアの横Text")]
    [SerializeField] private Text crosshairText;

    void Start()
    {
        // オブジェクトで自動アタッチ
        if (crosshairText == null)
        {
            GameObject textObj = GameObject.Find("CrosshairText");

            if (textObj != null)
            {
                crosshairText = textObj.GetComponent<Text>();
            }

            if (crosshairText == null)
            {
                Debug.LogWarning("クロスヘア用のテキストが見つかりません！");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // カメラ設定されていなければ何もしない
        if (playerCamera == null) return;

        if (crosshairText != null)
        {
            crosshairText.text = "";
        }

        // カメラの視線基準でRayを飛ばす
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // デバッグ描画
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        // Ray判定
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // レイアー判定
            int CustomerLayer = LayerMask.NameToLayer("Customer");
            if (hit.collider.gameObject.layer == CustomerLayer)
            {
                // ヒットオブジェクトにMoveWaypointスクリプトがあるか確認
                MoveWaypoint hitCustomer = hit.collider.GetComponent<MoveWaypoint>();
                if (hitCustomer != null)
                {
                    // 1番目のお客さんでレジ停止中
                    if (hitCustomer.isStopped && hitCustomer.queuePositionNumber == 0)
                    {
                        crosshairText.text = "お宝を見せてもらう(左クリック)";
                        if (Input.GetMouseButtonDown(0))
                        {

                            // 会計完了処理を実行
                           // hitCustomer.FinishCheckout();
                        }
                    }
                }
            }
        }
    }
}
