using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { START, ORDERING, MOVEMENT, SUCCESS, FAIL}
public class GameSystem : MonoBehaviour
{
    GameObject[] buttons;
    DirectionButton temp;
    public Text orderMessage;
    CountdownTimer countdown;

    public GameState state;
    // Start is called before the first frame update
    void Start()
    {
        state = GameState.START;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        StartCoroutine(SetUpGame());
        countdown = GameObject.FindGameObjectWithTag("Countdown").GetComponent<CountdownTimer>();
    }

    IEnumerator SetUpGame()
    {
        Debug.Log("Entered");
        yield return new WaitForSeconds(1);
        Text.Destroy(orderMessage);
        countdown.Active = true;
    }

    public void TimeOver()
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(buttons[i].name);
            buttons[i].GetComponent<DirectionButton>().TimeOver();

        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>().Movement();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
