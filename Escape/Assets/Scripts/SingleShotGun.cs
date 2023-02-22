using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun{
    [HideInInspector] public Camera cam;
    int shotsLeft;
    bool canShoot;

    void Awake(){
        shotsLeft = ((GunInfo)itemInfo).maxShots;
        canShoot = true;
    }
    public override bool IsHold(){
        return ((GunInfo)itemInfo).Hold;
    }

    public override void Use()
    {
        if(shotsLeft >= 1 && canShoot){
            Shoot();
        }
    }

    public override void StopUse(){
        
    }

    public override void Reload(){
        if(shotsLeft < ((GunInfo)itemInfo).maxShots){
            Invoke("ReloadGun", ((GunInfo)itemInfo).reloadTime);
        }
    }

    void Shoot(){
        Debug.Log("Shoot");
    }
    void ResetShoot(){
        canShoot = true;
    }
    void ReloadGun(){
        shotsLeft = ((GunInfo)itemInfo).maxShots;
        canShoot = true;
    }
}
