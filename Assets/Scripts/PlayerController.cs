using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameStarted = false;

    Animator anim;
    Rigidbody2D rb;


    public int jumpForce;

    public Transform groundPoint;

    public LayerMask groundLayer;

    bool grounded;

    public float yVelocity;

    public GameObject deathEffect;
    public Transform effectPosition;

    public GameObject gameover;
    public GameObject startbutton;

    //body parts
    public GameObject head;
    public GameObject body;
    public GameObject arm1;
    public GameObject arm2;
    public GameObject feet1;
    public GameObject feet2;
    public GameObject feather;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapPoint(groundPoint.position,groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameStarted == false) //game start if (checks for space press)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
                return;
            } else
            {
                return;
            }
            
        }//end of game start if

        if (Input.GetKeyDown(KeyCode.Space)) //start jump if (checks for space press)
        {
            Jump();
        } //end jump if

        if (Input.GetKeyDown(KeyCode.F))
        {
            fire();
        }



        yVelocity = rb.velocity.y;
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("grounded", grounded);

    }

    public void StartGame()
    {
        gameStarted = true;
        anim.SetTrigger("start");
        startbutton.SetActive(false);
    }

    public void Jump()
    {
        if (gameStarted && grounded)
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
        
    }

    public void fire()
    {
        if (gameStarted)
        {
            GameManager.instance.fire.CreateFireball(); 
        }
        
    }

    public void GameOver()
    {
        gameStarted = false;
        Instantiate(deathEffect, effectPosition.position, Quaternion.identity);
        gameover.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        head.SetActive(false);
        body.SetActive(false);
        arm1.SetActive(false);
        arm2.SetActive(false);
        feet1.SetActive(false);
        feet2.SetActive(false);
        feather.SetActive(false);
    }
}
