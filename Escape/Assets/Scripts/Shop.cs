using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem {
    public GameObject item;
    public int price;
    public float chance;
}

public class Shop : MonoBehaviour{
    public ShopItem[] allItems;
    public List<ShopItem> items;
    void Start(){
        if(items.Count == 0){
            GenerateItems();
        }
    }

    void GenerateItems(){
        for(int i = 0; i < allItems.Length; i++){
            if(Random.Range(0f, 1f) < allItems[i].chance){
                items.Add(allItems[i]);
            }
        }
    }
}
