using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D player;
    public Vector2 movement;
    public Animator animator;
    public AudioSource audioSrc;
    bool isMoving = false;

    void start() {
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

    }
    
    //Used fixed update as  Update is unreliable when it comes to physics 
    // Handle movments
    private void FixedUpdate()
    {
        player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);
    }
}
