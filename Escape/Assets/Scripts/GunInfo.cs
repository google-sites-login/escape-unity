using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo{
    public bool Hold;
    public int damage;
    public float reloadTime;
    public int maxShots;
    public float shotTime;
    public float range;
}