using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 鑑定中にお宝をマウスで回転させる
/// </summary>
public class ItemInspectController : MonoBehaviour
{
    [Header("回転の感度"),SerializeField]
    private float rotationSpeed = 5.0f;

    //鑑定中かどうか
    [SerializeField]private bool isInspecting = false;

    private void Update()
    {
        //鑑定中じゃなければ処理をしない
        if (!isInspecting) return;

        if (Input.GetMouseButton(0))
        {
            //マウスの移動量を取得
            float xDelta = Input.GetAxis("Mouse X") * rotationSpeed;
            float yDelta = Input.GetAxis("Mouse Y") * rotationSpeed;

            //オブジェクトを回転させる
            transform.Rotate(Vector3.up, -xDelta, Space.World);
            transform.Rotate(Vector3.right, yDelta, Space.World);
        }

    }

    /// <summary>
    /// 鑑定モードを開始するメソッド
    /// </summary>
    public void StartInspection()
    {
        isInspecting = true;
    }

    /// <summary>
    /// 鑑定モードを終了するメソッド
    /// </summary>
    public void EndInspection()
    {
        isInspecting = false;
    }
}

