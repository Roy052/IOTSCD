using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybtn : MonoBehaviour
{
    private GameObject mouse;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
            retry();
    }

    public void retry()
    {
        SceneManager.LoadScene("Game_Start");
    }
}
