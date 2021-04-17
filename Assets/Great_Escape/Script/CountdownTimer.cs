using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime;
    GameSystem gamesystem;
    public bool Active = false;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        gamesystem = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime > 0)
            {
                countdownText.text = currentTime.ToString("0.0");
                countdownText.color = Color.red;
            }
            if (currentTime <= 0)
            {
                gamesystem.TimeOver();
            }
        }
        
    }
}
