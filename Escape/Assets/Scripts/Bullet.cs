using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    public Damageable owner;
    public float damage;

    void Start(){
        Invoke("DestroyBullet", 5f);
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<Damageable>() != null && col.GetComponent<Damageable>() != owner){
            col.GetComponent<Damageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }
}
