using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Potion")]
public class PotionInfo : ItemInfo{
    public int healingAmount;
    public float beerDamageMultiplier;
    public float beerNauseaTime;
    public float beerUpgradeTime;
    public float speedMultiplier;
    public float speedTime;
}