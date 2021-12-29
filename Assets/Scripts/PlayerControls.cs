using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D player;
    public Vector2 movement;
    public Animator animator;
    public AudioSource audioSrc;
    bool isMoving = false;

    //Player health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Player Mana
    public int maxMana = 100;
    public int currentMana;
    public ManaBar manaBar;

    //Player Stamina
    public int maxStamina = 100;
    public int currentStamina;
    public StaminaBar staminaBar;

    void start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update to handle input
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x !=0 || movement.y !=0){
            isMoving = true;
        }
        else{
            isMoving = false;
        }

        if(isMoving){
            if (!audioSrc.isPlaying){
                audioSrc.Play();
            }
        }
        else
        {
            audioSrc.Stop();
        }

        // check to see if we can get damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

    }
    
    //Used fixed update as  Update is unreliable when it comes to physics 
    // Handle movments
    private void FixedUpdate()
    {
        player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void ManaUsage(int cost)
    {
        currentMana -= cost;
        manaBar.SetMana(currentMana);
    }

    // void StaminaUsage(int energy)
    // {
    //     currentStamina -= energy;
    //     staminaBar.SetStamina(currentStamina);
    // }
}
