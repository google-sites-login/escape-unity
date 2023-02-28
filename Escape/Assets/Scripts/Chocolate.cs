using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : Item{
    PlayerMovement player;
    public override void Use(){
        if(player == null){
            player = FindObjectOfType<PlayerMovement>();
        }
        player.StartSpeed(((PotionInfo)itemInfo).speedMultiplier, ((PotionInfo)itemInfo).speedTime);
        player.itemImages[player.itemIndex].sprite = null;
        Destroy(gameObject);
    }
    public override void Reload(){

    }
    public override string GetAmmo(){
        return "";
    }

    public override void StopUse(){

    }
    public override bool IsHold(){
        return false;
    }
}
