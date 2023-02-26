using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dropping {
    public GameObject prefab;
    public float chance;
}

public class Enemy : Damageable{
    [Header("Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 20;
    public int damage = 5;
    public float damageDelay = 2f;
    public int currentHealth = 0;

    public GameObject deathObject;

    public Color baseColour;
    public Color damagedColour;
    public SpriteRenderer sprite;

    public AudioSource die;
    public AudioSource grrr;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform target;

    [Header("Droppings")]
    public Dropping[] droppings;

    bool canAttack = true;

    PlayerMovement player;

    void Start(){
        player = FindObjectOfType<PlayerMovement>();
        currentHealth = maxHealth;
        Invoke("Sound", Random.Range(5f, 10f));
    }

    void Update(){
        CheckCollisions();
    }

    void FixedUpdate(){
        if(!player.shopMenu.activeSelf){
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        //rb.MovePosition(target.transform.position * Time.fixedDeltaTime);

        Vector2 lookDir = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void CheckCollisions(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

        for(int i = 0; i < colliders.Length; i++){
            if(colliders[i].GetComponent<PlayerMovement>() != null && canAttack){
                colliders[i].GetComponent<Damageable>().TakeDamage(damage);
                canAttack = false;
                Invoke("ResetAttack", damageDelay);
            }
        }
    }

    void ResetAttack(){
        canAttack = true;
    }

    void Sound(){
        grrr.Play();
        Invoke("Sound", Random.Range(5f, 10f));
    }

    void SpawnDroppings(){
        Instantiate(deathObject, transform.position, Quaternion.identity);
        for(int i = 0; i < droppings.Length; i++){
            if(Random.Range(0f, 1f) < droppings[i].chance){
                Instantiate(droppings[i].prefab, transform.position, Quaternion.identity);
            }
        }
    }
    public override void TakeDamage(int damage){
        currentHealth -= damage;
        sprite.color = damagedColour;
        Invoke("ResetColour", 0.2f);
        if(currentHealth <= 0){
            FindObjectOfType<AudioManager>().Play("ZombieDie");
            SpawnDroppings();
            Destroy(gameObject);
        }
    }

    void ResetColour(){
        sprite.color = baseColour;
    }
}
