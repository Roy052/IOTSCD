using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class White_score : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreText;
    public static White_score instance;
    static int white_score = 2;

    void Start()
    {
        white_score = 0;
        scoreText.text = "2";
    }
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = white_score.ToString();
    }

    public void update_white(int score)
    {
        white_score = score;
    }
}
