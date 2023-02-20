using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Damageable{
    public float moveSpeed = 5f;
    public int maxHealth = 20;
    public int currentHealth = 0;
    public Rigidbody2D rb;

    public HealthBar healthBar;

    Vector2 input;

    void Awake(){
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    void Update(){
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    public override void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
