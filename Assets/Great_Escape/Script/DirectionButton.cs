using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionButton : MonoBehaviour
{
    Button button;
    int ordercount;
    private GameObject character;
    // Start is called before the first frame update

    private void Awake()
    {
        ordercount = 0;
        character = GameObject.FindGameObjectWithTag("Character");
    }
    public void onClickButton()
    { 
        Debug.Log(this.gameObject.name);
        if(this.gameObject.name == "Up")
        {
            
        }
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClickButton);
        
    }
}
