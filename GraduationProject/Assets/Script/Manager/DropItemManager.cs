using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    // どこからでも DropItemManager.Instance で呼べるようにする
    public static DropItemManager Instance { get; private set; }

    [Header("お宝を落とす位置"), SerializeField]
    private Transform dropItemPos;

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
    /// お宝を落とす（生成する）処理
    /// </summary>
    public void DropItem(GameObject dropTreasure)
    {
        if (dropTreasure != null && dropItemPos != null)
        {
            // 指定した位置に、お宝をインスタンス化する
            Instantiate(dropTreasure, dropItemPos.position, dropItemPos.rotation);
            Debug.Log(dropTreasure.name + " を鑑定台に置きました！");
        }
        else
        {
            Debug.LogWarning("お宝のプレハブ、またはドロップ位置が設定されていません！");
        }
    }
}
