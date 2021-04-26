using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionButton : MonoBehaviour
{
    Button button;
    private Playermove playerMove;
    public Text orderMessage;
    // Start is called before the first frame update

    private void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>();
        orderMessage.text = "";
    }
    public void onClickButton()
    { 
        //Debug.Log(this.gameObject.name);
        if(this.gameObject.name == "Up")
        {
            playerMove.UpButtonClicked();
            orderMessage.text += "↑ ";
        }
        else if(this.gameObject.name == "Left")
        {
            playerMove.LeftButtonClicked();
            orderMessage.text += "← ";
        }
        else if(this.gameObject.name == "Right")
        {
            playerMove.RightButtonClicked();
            orderMessage.text += "→ ";
        }
        else if(this.gameObject.name == "Reset")
        {
            playerMove.ResetButtonClicked();
            orderMessage.text = "";
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
