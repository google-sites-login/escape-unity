using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item{

    PlayerMovement player;
    public override void Use(){
        if(player == null){
            player = FindObjectOfType<PlayerMovement>();
        }
        player.currentHealth = Mathf.Clamp(player.currentHealth += ((PotionInfo)itemInfo).healingAmount, 0, player.maxHealth);
        FindObjectOfType<AudioManager>().Play("Drink");
        player.healthBar.SetHealth(player.currentHealth);
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
