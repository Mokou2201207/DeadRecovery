using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("アニメーションのコンポーネント")]
    [SerializeField] private Animator animator;

    [Header("移動設定")]
    // 移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeed = 8.0f;

    private void Start()
    {
        if (animator==null)
        {
            // Animatorコンポーネントの取得
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Animatorコンポーネントがアタッチされていません。");
        }

    }


    void Update()
    {  
        // キーボード入力の取得
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 移動入力がある場合のみアニメーション処理を行う 
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //シフトを押している場合は走るアニメーションを再生する
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
            }
            //走っていない場合は歩くアニメーションを再生する
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("walk", true);
            }
        }
        else
        {
            //移動入力がない場合は、歩く・走るアニメーションを停止する
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }

        //シフトキーを押しているかどうかで、移動速度を変化
        float currentSpeed=Input.GetKey(KeyCode.LeftShift)?runSpeed:moveSpeed;

        // プレイヤーの向いている方向に合わせて移動方向を計算
        Vector3 move = transform.right * x + transform.forward * z;

        //プレイヤーを移動させる
        transform.Translate(move * currentSpeed * Time.deltaTime, Space.World);
    }
}
