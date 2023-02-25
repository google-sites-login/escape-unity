using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun{
    int shotsLeft;
    bool canShoot;
    public Transform shootPoint;
    [SerializeField] GameObject prefab;

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
    public override string GetAmmo(){
        return shotsLeft.ToString() + "/" + ((GunInfo)itemInfo).maxShots.ToString();
    }

    void Shoot(){
        shotsLeft -= 1;
        FindObjectOfType<AudioManager>().Play("Shoot");
        GameObject bullet = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootPoint.up * 20, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().damage = ((GunInfo)itemInfo).damage;
        bullet.GetComponent<Bullet>().owner = transform.parent.GetComponent<Damageable>();
    }
    void ResetShoot(){
        canShoot = true;
    }
    void ReloadGun(){
        shotsLeft = ((GunInfo)itemInfo).maxShots;
        canShoot = true;
    }
}
