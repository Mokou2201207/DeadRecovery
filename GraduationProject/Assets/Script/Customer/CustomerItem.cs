using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// お客さんが持ってるお宝
/// </summary>
public class CustomerItem : MonoBehaviour
{
    [Header("お宝一覧"), SerializeField]
    private GameObject[] itemList;

    [Header("今持ってるお宝※自動でランダムなお宝になります。"), SerializeField]
    public GameObject currentItem;

    // お宝が落とされたかどうかのフラグ
    public bool isItemDropped = false; 

    private void Start()
    {
        //ランダムにお宝を選択する
        if (itemList != null && itemList.Length > 0)
        {
            int randItem =Random.Range(0, itemList.Length);
            currentItem = itemList[randItem];
        }
    }

}
