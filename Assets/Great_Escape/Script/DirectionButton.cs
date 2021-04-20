using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionButton : MonoBehaviour
{
    Button button;
    private Playermove playerMove;
    // Start is called before the first frame update

    private void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>();
    }
    public void onClickButton()
    { 
        //Debug.Log(this.gameObject.name);
        if(this.gameObject.name == "Up")
        {
            playerMove.UpButtonClicked();
        }
        else if(this.gameObject.name == "Left")
        {
            playerMove.LeftButtonClicked();
        }
        else if(this.gameObject.name == "Right")
        {
            playerMove.RightButtonClicked();
        }
        else if(this.gameObject.name == "Reset")
        {
            playerMove.ResetButtonClicked();
        }
    }

    public void TimeOver()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClickButton);
        
    }
}
