using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SpawnObject {
    public GameObject spawnObject;
    public float spawnChance;
    public int maxPerChunk;
    public int objectSize;
}

public class ChunkLoader : MonoBehaviour{
    public int ChunkDistance;
    public int ChunkSize;
    public GameObject chunkPrefab;
    public GameObject player;
    public SpawnObject[] spawnObjects;
    Dictionary<Vector2, GameObject> chunks = new Dictionary<Vector2, GameObject>();//CHunks vector 2 is chunk coordinate(world coordinate * chunk size), not world coordinate

    void Start(){
        LoadChunks();
    }

    
    void Update(){
        LoadChunks();
    }

    float nearestChunk(float playerPos){
        return Mathf.Round(playerPos/ChunkSize) * ChunkSize;
    }

    void LoadChunks(){
        foreach(KeyValuePair<Vector2, GameObject> chunk in chunks){
            chunk.Value.SetActive(false);
        }
        for(int x = -ChunkDistance; x < ChunkDistance; x++){
            for(int y = -ChunkDistance; y < ChunkDistance; y++){
                Vector2 chunkPos = new Vector2(nearestChunk(player.transform.position.x) + x*ChunkSize, nearestChunk(player.transform.position.y) + y*ChunkSize);
                Vector2 chunkLocalPos = chunkPos/ChunkSize;

                if(chunks.ContainsKey(chunkLocalPos)){//Enable the chunk
                    chunks[chunkLocalPos].SetActive(true);
                }else{//Create new chunk and save on dictionary
                    GameObject chunk = Instantiate(chunkPrefab, chunkPos, Quaternion.identity, transform);
                    chunk.transform.localScale = new Vector3(ChunkSize/5, ChunkSize/5, 0);
                    chunk.name = "Chunk(" + chunkLocalPos.x + ", " + chunkLocalPos.y + ")";
                    chunks.Add(chunkLocalPos, chunk);
                    for(int i = 0; i < spawnObjects.Length; i++){
                        SpawnObject(chunk, 0, spawnObjects[i]);
                    }
                }
            }
        }
    }

    void SpawnObject(GameObject parent, int loopCount, SpawnObject obj){
        if(Random.Range(0f, 1f) < obj.spawnChance){//Spawn
            float posLimit = ChunkSize/2 - obj.objectSize/2;
            Vector3 spawnPos = parent.transform.position + new Vector3(Random.Range(-posLimit, posLimit), Random.Range(-posLimit, posLimit), 0);
            if(Vector3.Distance(spawnPos, player.transform.position) < 10){
                return;
            }
            Object.Instantiate(obj.spawnObject, spawnPos, Quaternion.identity, parent.transform);
            if(loopCount+1 < obj.maxPerChunk){
                SpawnObject(parent, loopCount+1, obj);
            }
        }
    }
}
