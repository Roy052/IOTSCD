using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SetStage stage;
    public int[,] arr;
    public int[,] check;
    public int row, col;
    private void Start()
    {
        arr = new int[12,12];
        check = new int[12,12];

        setBoard(row, col,row*col*3);
        //debug
        for(int i=0;i<row;i++)
        {
            for(int j=0;j<col;j++)
            {
                Debug.Log(arr[i,j]);
            }
        }
        stage.GenerateStage();
    }
    void setBoard(int row, int col, int count)
    {
        int i, j, temp;
        int r1,r2,c1,c2;
        for(i=0;i<row*col;i=i+2)
        {
            arr[i/col,i%col] = i/2;
            arr[(i+1)/col,(i+1)%col] = i/2;
        }

        for(i=0;i<row;i++)
        {
            for(j=0;j<col;j++)
            {
                check[i,j] = 0;
            }
        }

        for(i=0;i<count;i++)
        {
            c1 = Random.Range(0,col);
            c2 = Random.Range(0,col);
            r1 = Random.Range(0,row);
            r2 = Random.Range(0,row);

            temp = arr[r1,c1];
            arr[r1,c1] = arr[r2,c2];
            arr[r2,c2] = temp;
        }
    }
}
