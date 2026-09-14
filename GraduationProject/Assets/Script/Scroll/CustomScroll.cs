using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomScroll : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private Scrollbar scrollbar;     
    [SerializeField] private RectTransform contentPanel; 

    [Header("移動範囲の調整")]
    [SerializeField] private float minY = 0f;          // 一番上に来たときのY座標
    [SerializeField] private float maxY = 500f;        // 一番下に来たときのY座標（項目の量に合わせて調整）

    void Start()
    {
        // スクロールバーの値が変わったときに、動かす処理を登録する
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(OnScrollValueChanged);
        }
    }

    // スクロールバーが動かされたときに呼ばれる関数
    void OnScrollValueChanged(float value)
    {
        if (contentPanel != null)
        {
            // スクロールバーの値（0.0 〜 1.0）に応じて、パネルのY座標を変化させる
            float newY = Mathf.Lerp(minY, maxY, value);
            Vector2 anchoredPos = contentPanel.anchoredPosition;
            anchoredPos.y = newY;
            contentPanel.anchoredPosition = anchoredPos;
        }
    }
}
