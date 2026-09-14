using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤー視点判定
/// </summary>
public class PlayerLook : MonoBehaviour
{
    [Header("カメラ設定")]
    [SerializeField] private Transform playerCamera;

    [Header("Playerが届くRayの距離")]
    [SerializeField] private float rayDistance = 100.0f;

    // Update is called once per frame
    void Update()
    {
        //カメラが設定されてなかったら何も処理しない
        if(playerCamera==null) return;

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
                Debug.Log(111);
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
