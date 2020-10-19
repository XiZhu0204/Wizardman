using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{


    public Transform greenfire;
    public Transform purplefire;

    public bool appeared = false;

    public float countdown = 5;

    Transform fireStart;

    public int vel = 8;
    bool purpleCreated = false;

    public Rigidbody2D purpleRB;
    public int xForce;
    public int yForceMultiplier;

    public float antiWatts = -1;

    private void Start()
    {

        purpleRB = GetComponentInChildren<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameObject.Find("Wizardman").GetComponent<PlayerController>().gameStarted)
        { //start if (pauses movement during idle)
            return;
        } //end if

        if (!appeared) //start if (Start counting down)
        {
            countdown -= Time.deltaTime;
        } //end if

        if (countdown < 0) //start if (If countdown is negative, hazard appears)
        {
            appeared = true;
            GameManager.instance.green.SetActive(true);
        } // end if 

        if (greenfire.position.x != GameManager.instance.fireEnd.position.x) //start if (moves greenfire towards player)
        {
            greenfire.position = Vector3.MoveTowards(greenfire.position, new Vector3(GameManager.instance.fireEnd.position.x, greenfire.position.y, greenfire.position.z), Time.deltaTime * vel);
        }
        else
        {
            fireStart = GameManager.instance.fireSpawns[Random.Range(0, GameManager.instance.fireSpawns.Length)];
            greenfire.position = fireStart.position;
            countdown = -1;
        }// end if (tps fireball back to start, set countdown to negative to ensure proper creation of next fireball)

        if (purpleCreated && purplefire.position.x <= GameManager.instance.purpleEnd.position.x) //start if (ridgidbody force on the purple fire)
        {
            purpleRB.AddForce(Vector2.right * xForce);
            purpleRB.AddForce(Vector3.up * yForceMultiplier * GameManager.instance.player.yVelocity);
        } else
        {
            GameManager.instance.purple.SetActive(false);
        } //end if

        antiWatts -= Time.deltaTime;
    }

    public void CreateFireball()
    {
        if (antiWatts < 0)
        {
            GameManager.instance.purple.SetActive(true);
            purpleCreated = true;
            purplefire.position = GameManager.instance.player.transform.position;
            purplefire.position = new Vector3(purplefire.position.x, purplefire.position.y + 2, purplefire.position.z);
            antiWatts = 3;
        }
        

    }
}
