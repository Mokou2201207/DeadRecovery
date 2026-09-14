using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// プレイヤー視点判定
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Header("カメラ設定")]
    [SerializeField] private Transform playerCamera;

    [Header("Playerが届くRayの距離")]
    [SerializeField] private float rayDistance = 100.0f;

    [Header("クロスヘアの下のText")]
    [SerializeField] private Text crosshairText;

    void Start()
    {
        //オブジェクト名で自動アタッチ
        if (crosshairText == null)
        {
            GameObject textObj = GameObject.Find("CrosshairText");

            if (textObj != null)
            {
                crosshairText = textObj.GetComponent<Text>();
            }

            if (crosshairText == null)
            {
                Debug.LogWarning("クロスヘア用のテキストが見つかりませんでした！");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //カメラが設定されてなかったら何も処理しない
        if(playerCamera==null) return;

        if (crosshairText != null)
        {
            crosshairText.text = "";
        }

        //カメラの向きを基準でRayをはる
        Ray ray=playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));

        //デバッグ描画
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        //もし何かにヒットしたら実行
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, rayDistance))
        {
            //Tag付きがヒットしたら
            if (hit.collider.CompareTag("GarageDoor"))
            {
                crosshairText.text = "E ステージへ移動";
                //Eキーを押したらステージへ移行
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Teleport teleport=hit.collider.GetComponent<Teleport>();
                    if (teleport!=null)
                    {
                        teleport.StageTeleport();
                    }
                }
            }
        }

    }
}
