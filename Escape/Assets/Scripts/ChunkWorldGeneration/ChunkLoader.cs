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

    void LoadChunks(){
        foreach(KeyValuePair<Vector2, GameObject> chunk in chunks){
            chunk.Value.SetActive(false);
        }
        for(int x = -ChunkDistance; x < ChunkDistance; x++){
            for(int y = -ChunkDistance; y < ChunkDistance; y++){
                if(Vector2.Distance(new Vector2(x*ChunkSize, y*ChunkSize), new Vector2(player.transform.position.x, player.transform.position.y)) < ChunkDistance){
                    if(chunks.ContainsKey(new Vector2(x, y))){//Enable the chunk
                        chunks[new Vector2(x, y)].SetActive(true);
                    }else{//Create new chunk and save on dictionary
                        GameObject chunk = Instantiate(chunkPrefab, new Vector3(x*ChunkSize, y*ChunkSize, 0), Quaternion.identity, transform);
                        chunk.transform.localScale = new Vector3(ChunkSize/5, ChunkSize/5, 0);
                        chunk.name = "Chunk(" + y + ", " + x + ")";
                        chunks.Add(new Vector2(x, y), chunk);
                        for(int i = 0; i < spawnObjects.Length; i++){
                            SpawnObject(chunk, 0, spawnObjects[i]);
                        }
                    }
                }
            }
        }
    }

    void SpawnObject(GameObject parent, int loopCount, SpawnObject obj){
        if(Random.Range(0f, 1f) < obj.spawnChance){//Spawn
            float posLimit = ChunkSize/2 - obj.objectSize/2;
            Object.Instantiate(obj.spawnObject, parent.transform.position + new Vector3(Random.Range(-posLimit, posLimit), Random.Range(-posLimit, posLimit), 0), Quaternion.identity, parent.transform);
            if(loopCount+1 < obj.maxPerChunk){
                SpawnObject(parent, loopCount+1, obj);
            }
        }
    }
}
