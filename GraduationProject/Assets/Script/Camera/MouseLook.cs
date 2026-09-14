using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///カメラにつける用のscript
/// </summary>
public class MouseLook : MonoBehaviour
{
    [Header("感度設定")]
    [SerializeField] private float mouseSensitivityXSpeed = 5.0f;
    [SerializeField] private float mouseSensitivityYSpeed = 5.0f;

    [Header("カメラの上下制限")]
    [SerializeField] private float upperLimit = 75.0f;
    [SerializeField] private float lowerLimit = -75.0f;

    [Header("カメラの手振れ感")]
    [Header("手振れ感ON/OFF"), SerializeField]
    private bool enableHeadBob = true;
    [Header("揺れの速さ（歩行時）"), SerializeField]
    private float bobFrequency = 5.0f;
    [Header("揺れの大きさ（歩行時）"), SerializeField]
    private float bobAmount = 0.05f;

    [Header("静止時の手振れ設定")]
    [SerializeField] private float idleBobFrequency = 1.0f;
    [SerializeField] private float idleBobAmount = 0.01f;

    [Header("走行時の手振れ設定")]
    [SerializeField] private float runBobFrequency = 8.0f;
    [SerializeField] private float runBobAmount = 0.1f;

    [Header("手振れ遷移の滑らかさ")]
    [SerializeField] private float transitionSpeed = 5.0f;

    [Header("酔い防止設定")]
    [Tooltip("左右の揺れの倍率")]
    [SerializeField] private float horizontalBobPercentage = 0.5f;

    private float timer = 0f;
    private float currentBobFrequency;
    private float currentBobAmount;
    // プレイヤーの体
    private Transform playerBody;
    // 上下の回転を記憶する変数
    private float verticalRotation = 0f;

    void Start()
    {
        // ゲーム開始時にマウスポインターを画面中央に固定して消す
        Cursor.lockState = CursorLockMode.Locked;

        // このスクリプトがアタッチされているオブジェクトを探す
        if (transform.parent != null)
        {
            playerBody = transform.parent;
        }

        //シーン上にいるSettingsManagerを見つけRegisterMouseLook関数にMouseLookを渡す
        OptionsSettingsManager manager = FindObjectOfType<OptionsSettingsManager>();
        if (manager != null)
        {
            manager.RegisterMouseLook(this);
        }

        // 手振れの初期値を設定
        currentBobFrequency = idleBobFrequency;
        currentBobAmount = idleBobAmount;
    }

    void Update()
    {
        //ゲームが止まってる時はなにもしない
        if (Time.timeScale == 0f) return;

        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityXSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityYSpeed;

        // カメラを上下に向ける
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, lowerLimit, upperLimit); // 限界角度内に制限

        float currentVerticalRotation=verticalRotation;
        float currentHoriOffset = 0f;

        if (enableHeadBob)
        {
            // プレイヤーの移動入力を取得
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");
            bool isMoving = (Mathf.Abs(inputX) > 0.05f || Mathf.Abs(inputZ) > 0.05f);
            bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

            // 目標とする手振れの速さと大きさを決定
            float targetBobFrequency = idleBobFrequency;
            float targetBobAmount = idleBobAmount;

            if (isMoving)
            {
                if (isRunning)
                {
                    targetBobFrequency = runBobFrequency;
                    targetBobAmount = runBobAmount;
                }
                else
                {
                    targetBobFrequency = bobFrequency;
                    targetBobAmount = bobAmount;
                }
            }

            // 現在の値を目標値にスムーズに近づける
            currentBobFrequency = Mathf.Lerp(currentBobFrequency, targetBobFrequency, Time.deltaTime * transitionSpeed);
            currentBobAmount = Mathf.Lerp(currentBobAmount, targetBobAmount, Time.deltaTime * transitionSpeed);

            timer += Time.deltaTime * currentBobFrequency;

            float noiseX = Mathf.Sin(timer) * (currentBobAmount * horizontalBobPercentage);
            float noiseY = Mathf.Cos(timer * 2.0f) * currentBobAmount;

            currentVerticalRotation += noiseY; // 上下に揺らす
            currentHoriOffset = noiseX;        // 左右に揺らす
        }

        // カメラのローカル回転を更新
        transform.localRotation = Quaternion.Euler(currentVerticalRotation, 0f, currentHoriOffset);

        // プレイヤーの体を左右に回転させる
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    /// <summary>
    /// 感度X軸を取得する
    /// </summary>
    /// <returns></returns>
    public float GetSensitivityX()
    {
        return mouseSensitivityXSpeed;
    }

    /// <summary>
    /// 感度Y軸を取得する
    /// </summary>
    /// <returns></returns>
    public float GetSensitivityY()
    {
        return mouseSensitivityYSpeed;
    }

    /// <summary>
    /// 感度X軸を新しく設定する
    /// </summary>
    public void SetSensitivityX(float newSensitivityX)
    {
        mouseSensitivityXSpeed = newSensitivityX;
    }

    /// <summary>
    /// 感度Y軸を新しく設定する
    /// </summary>
    public void SetSensitivityY(float newSensitivitY)
    {
        mouseSensitivityYSpeed = newSensitivitY;
    }

    /// <summary>
    /// 手振れのオン、オフ
    /// </summary>
    /// <param name="isActive"></param>
    public void SetHeadBobActive(bool isActive)
    {
        enableHeadBob = isActive;
    }
}
