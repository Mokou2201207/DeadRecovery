using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// お客さんが持ってるお宝
/// </summary>
public class ItemData : MonoBehaviour
{
    //どこからでもアクセス可能
    public static ItemData Instance { get; private set; }

    [Header("お宝一覧"), SerializeField]
    private GameObject[] itemList;

    [Header("今持ってるお宝※自動でランダムなお宝になります。"), SerializeField]
    private GameObject currentItem;

    private void Start()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄する
        }

        // ランダムなお宝を選ぶ
        int randItem = Random.Range(0, itemList.Length);
        currentItem = itemList[randItem];   
    }

}
