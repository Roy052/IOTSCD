using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update

    private void Awake()
    {
    }
    //스테이지를 선택하는 버튼
    public void onClickButton()
    {
        //Debug.Log(this.gameObject.name);
        if (this.gameObject.name == "Button45")
        {
            SceneManager.LoadScene("stage45");
        }
        else if (this.gameObject.name == "Button66")
        {
            SceneManager.LoadScene("stage66");
        }
        else if (this.gameObject.name == "Button88")
        {
            SceneManager.LoadScene("stage88");
        }
        else if (this.gameObject.name == "Button1010")
        {
            SceneManager.LoadScene("stage1010");
        }
    }


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClickButton);

    }
}
