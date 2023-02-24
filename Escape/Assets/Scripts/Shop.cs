using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem {
    public GameObject item;
    public int price;
}

public class Shop : MonoBehaviour{
    public ShopItem[] items;
    void Start(){
        if(items.Length == 0){
            GenerateItems();
        }
    }

    void GenerateItems(){

    }
}
