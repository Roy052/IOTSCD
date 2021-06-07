using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetStage : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameManager gameManager; //debug
    public float gap = 1.3f;
    public float imagesize = 2;


    public int Width;
    public int Height;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>(); //GameManager에 SetStage를 부착했음.
        gameManager.boardSetup(Width, Height);  
        Debug.Log("Width = " + Width + " Height = " + Height);
        GenerateStage();
    }

    public void GenerateStage()
    {
        float startX = -(imagesize*(Width/2)+gap*(Width/2))+gap;
        float startY = -(imagesize*(Height/2)+gap*(Height/2));

        for(int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                Vector3 position = new Vector3(startX+(imagesize*x)+(gap*x),startY+(imagesize*y)+(gap*y), 0);

                SpawnCard(gameManager.board[x,y], x, y, position);
            }
        }
    }

    //카드에 대해 카드 이미지 번호, 카드 width, 카드 height, 카드 위치 정보 전달.
    private void SpawnCard(int cardType, int positionWidth, int positionHeight, Vector3 position)
    {
        GameObject clone = Instantiate(cardPrefab, position, Quaternion.identity);

        clone.name = "Card" + positionWidth + positionHeight;
        clone.transform.SetParent(transform);

        Card card = clone.GetComponent<Card>();
        card.Setup(cardType, positionWidth, positionHeight, position);
    }
}
