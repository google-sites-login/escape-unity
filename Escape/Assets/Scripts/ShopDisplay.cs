using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour{
    public TMP_Text priceText;
    public TMP_Text nameText;
    public Image image;
    ShopItem item;

    public PlayerMovement player;

    public void Init(ShopItem _item){
        player = FindObjectOfType<PlayerMovement>();
        item = _item;
        priceText.text = _item.price.ToString();
        nameText.text = _item.item.name;
    }

    public void BuyItem(){
        player.PickUpItem(item.item, player.itemIndex);
    }
}
