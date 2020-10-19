using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollide : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player.GameOver();
            GameManager.instance.green.SetActive(false);
        }
    }
}