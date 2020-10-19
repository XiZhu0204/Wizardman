using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public int moveVel = 5;


    Transform startPosition;

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Wizardman").GetComponent<PlayerController>().gameStarted)
        { //start if (pauses move flatforms when idle)
            return;
        } //end if

        if (transform.position.x != GameManager.instance.endPosition.position.x)
        { // start if (moves the background)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(GameManager.instance.endPosition.position.x, transform.position.y, transform.position.z), Time.deltaTime * moveVel);
        }
        else
        {
            startPosition = GameManager.instance.spawnPositions[Random.Range(0, GameManager.instance.spawnPositions.Length)];
            transform.position = startPosition.position;
        } //end of if/else
    }
}
