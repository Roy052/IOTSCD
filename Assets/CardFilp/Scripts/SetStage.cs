using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetStage : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private GameManager gm; //debug


    public int Width{private set; get;} = 4;
    public int Height{private set; get;} = 4;

    private void Awake()
    {
        Width = gm.col;
        Height = gm.row;
    }

    public void GenerateStage()
    {
        for(int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                Vector3 position = new Vector3((-Width*0.5f+0.5f)+x, (Height*0.5f-0.5f)-y, 0);

                SpawnCard(CardType.Back, position);
            }
        }
    }

    private void SpawnCard(CardType cardType, Vector3 position)
    {
        GameObject clone = Instantiate(cardPrefab, position, Quaternion.identity);

        clone.name = "Card";
        clone.transform.SetParent(transform);

        Card card = clone.GetComponent<Card>();
        card.Setup(cardType);
    }
}
