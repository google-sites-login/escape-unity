using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnEnemy {
    public GameObject prefab;
    public float chance;
}

public class EnemySpawner : MonoBehaviour{
    [SerializeField] float rate;
    [SerializeField] SpawnEnemy[] enemies;
    [SerializeField] Transform player;
    [SerializeField] float spawnRange;
    [SerializeField] float minDST;

    void Start(){
        InvokeRepeating("SpawnEnemys", rate, rate);
    }

    void SpawnEnemys(){
        foreach(SpawnEnemy enemy in enemies){
            if(Random.Range(0f, 1f) < enemy.chance){
                SpawnEnemy(enemy);
            }
        }
    }

    void SpawnEnemy(SpawnEnemy enemy){
        Vector3 spawnPos = player.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
        if(Vector3.Distance(player.position, spawnPos) < minDST){
            SpawnEnemy(enemy);
            return;
        }else{
            Enemy enemyObj = Instantiate(enemy.prefab, spawnPos, Quaternion.identity).GetComponent<Enemy>();
            enemyObj.target = player;
        }
    }
}
