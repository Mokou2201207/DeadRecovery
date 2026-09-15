using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    [Header("人間複数をアタッチ")]
    [SerializeField] private GameObject[] humans;

    /// <summary>
    /// リストで管理
    /// </summary>
    [System.Serializable]
    public class SpawnData
    {
        [Header("リストの名前")]
        [SerializeField] private string dataName;
        [Header("死体のスポーンの場所")]
        [SerializeField] public Transform[] spawnPoints;
        [Header("死体の数")]
        [SerializeField] public int corpseCount = 1;
    }

    [Header("スポーン設定リスト")]
    [SerializeField]private List<SpawnData> spawnList=new List<SpawnData>();

    private void Start()
    {
        SpawnCorpses();
    }

    /// <summary>
    /// 死体をスポーンさせる処理
    /// </summary>
    void SpawnCorpses()
    {
        //しっかり中身が入っていなければ処理しない
        if (humans == null || humans.Length == 0 || spawnList.Count == 0) return;

        foreach(var data in spawnList)
        {
            //スポーンの中身が入ってなけば飛ばして次の処理に進む
            if(data.spawnPoints==null||data.spawnPoints.Length==0) continue;

            for (int i=0; i<data.corpseCount;i++)
            {
                //キャラクターを０からキャラクターの数までランダムに選択して決める
                int randomIndex = Random.Range(0, humans.Length);
                GameObject selectedHuman = humans[randomIndex];

                //出現ポイントを０から出現ポイントの数までランダムに選択して決める
                int randomPointIndex = Random.Range(0, data.spawnPoints.Length);
                Transform selectedPoint = data.spawnPoints[randomPointIndex];

                //出現ポイントがしっかり決まったらそこに出現
                if (selectedPoint!=null)
                {
                    Instantiate(selectedHuman, selectedPoint.position, selectedPoint.rotation);
                }
            }
        }
    }

}
