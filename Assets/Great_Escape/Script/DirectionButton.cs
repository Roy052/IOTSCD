using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    public void onClickButton()
    { 
        Debug.Log(this.gameObject.name);
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClickButton);
    }
}
