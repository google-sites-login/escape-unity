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

    void Start(){
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    void SpawnEnemy(){
        foreach(SpawnEnemy enemy in enemies){
            if(Random.Range(0f, 1f) < enemy.chance){
                Enemy enemyObj = Instantiate(enemy.prefab, player.position + new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity).GetComponent<Enemy>();
                enemyObj.target = player;
            }
        }
    }
}
