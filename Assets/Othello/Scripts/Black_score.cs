using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_score : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreText;
    public static Black_score instance;
    static int black_score = 2;

    void Start()
    {
        black_score = 0;
        scoreText.text = "2";
    }
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = black_score.ToString();
    }

    public void update_black(int score)
    {
        black_score = score;
    }
}
