using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScore : MonoBehaviour
{
    private int point;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += new Vector3(0, 0.01f, 0);
    }

    public void init(int p)
    {
        point = p;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
