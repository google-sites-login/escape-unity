using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : Item{

    PlayerMovement player;
    public override void Use(){
        if(player == null){
            player = FindObjectOfType<PlayerMovement>();
        }
        player.StartDrunk(((PotionInfo)itemInfo).beerNauseaTime, ((PotionInfo)itemInfo).beerUpgradeTime, ((PotionInfo)itemInfo).beerDamageMultiplier);
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
