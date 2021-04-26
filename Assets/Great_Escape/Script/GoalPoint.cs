using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    GameSystem gamesystem;

    private void Start()
    {
        gamesystem = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gamesystem.stageClear();
        }
    }


}
