using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    public static DropItemManager Instance { get; private set; }

    [Header("ドロップ位置"), SerializeField]
    private Transform dropItemPos;

    // お客さんがカウンターに出したお宝インスタンスの保持
    private GameObject currentDroppedItem;

    public GameObject CurrentDroppedItem => currentDroppedItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// お宝アイテムを生成・ドロップする
    /// </summary>
    public void DropItem(GameObject dropTreasure)
    {
        if (dropTreasure != null && dropItemPos != null)
        {
            // 既にカウンターにお宝が置かれている場合は削除
            if (currentDroppedItem != null)
            {
                Destroy(currentDroppedItem);
            }

            // 新しくお宝をインスタンス化
            currentDroppedItem = Instantiate(dropTreasure, dropItemPos.position, dropItemPos.rotation);

            // Raycast判定用にColliderが無い場合は自動付与
            if (currentDroppedItem.GetComponentInChildren<Collider>() == null)
            {
                currentDroppedItem.AddComponent<BoxCollider>();
            }

            // Raycast判定用にItemDataが無い場合は自動付与
            if (currentDroppedItem.GetComponent<ItemData>() == null)
            {
                ItemData itemData = currentDroppedItem.AddComponent<ItemData>();
                itemData.itemTypeName = "TreasureType";
            }

            Debug.Log("[DropItemManager] " + dropTreasure.name + " をカウンターに置きました！");
        }
        else
        {
            Debug.LogWarning("[DropItemManager] ドロップするお宝Prefab、または dropItemPos が設定されていません！");
        }
    }

    /// <summary>
    /// 買い取り確定時にお客さんが出したお宝を消去・回収する
    /// </summary>
    public void ClearDroppedItem()
    {
        if (currentDroppedItem != null)
        {
            Destroy(currentDroppedItem);
            currentDroppedItem = null;
            Debug.Log("[DropItemManager] カウンターのお宝を回収・削除しました。");
        }

        // 万が一置かれたお宝が残っている場合もシーン内のお宝（ItemData）を探索して削除
        ItemData[] items = FindObjectsOfType<ItemData>();
        foreach (var item in items)
        {
            if (item != null && item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
