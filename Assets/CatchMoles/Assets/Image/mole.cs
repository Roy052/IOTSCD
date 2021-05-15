using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{
	public int row;
	public int col;

	void Start()
	{

	}

	void Update()
	{

	}

    private void OnMouseDown()
    {
		Debug.Log("꺅");
		hole.moleMap[row, col] = 0;
		Destroy(this);
    }
}
