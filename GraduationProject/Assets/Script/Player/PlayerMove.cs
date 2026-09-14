using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    // 移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeed = 8.0f;

    void Update()
    {  
        // キーボード入力の取得
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //シフトキーを押しているかどうかで、移動速度を変化
        float currentSpeed=Input.GetKey(KeyCode.LeftShift)?runSpeed:moveSpeed;

        // プレイヤーの向いている方向に合わせて移動方向を計算
        Vector3 move = transform.right * x + transform.forward * z;

        //プレイヤーを移動させる
        transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);
    }
}
