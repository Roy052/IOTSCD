using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime;
    GameObject[] buttons;
    DirectionButton temp;
    bool printed = false;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime > 0)
        {
            
            countdownText.text = currentTime.ToString("0.0");
            countdownText.color = Color.red;
        }

        if(currentTime <= 0)
        {
            if (printed != true)
            {
                for (int i = 0; i < 3; i++)
                {
                    Debug.Log(buttons[i].name);
                    buttons[i].GetComponent<DirectionButton>().TimeOver();
                    
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>().Movement();
                printed = true;
            }
            //SceneManager.LoadScene("geFail");
        }
    }
}
