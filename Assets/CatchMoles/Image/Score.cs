using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public static Score instance;
    static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "0 점";
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " 점";
    }

    public void addScore(int point)
    {
        score += point;
    }

    public void setScore(int point)
    {
        score = point;
    }

    public int getScore()
    {
        return score;
    }
}
