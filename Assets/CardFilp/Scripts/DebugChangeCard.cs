using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour
{
    CardFlipper flipper;
    CardModel cardModel;
    int cardIndex = 0;
    public GameObject card;

    void Awake()
    {
        cardModel = card.GetComponent<CardModel> ();
        flipper = card.GetComponent<CardFlipper> ();
    }

    void OnGUI()
    {
        if(GUI.Button (new Rect (10, 10, 100, 28),"클릭하세요"))
        {
            if(cardIndex >= cardModel.faces.Length)
            {
                cardIndex = 0;
                flipper.FlipCard(cardModel.faces[cardModel.faces.Length-1],cardModel.cardBack, -1);
            }
            else
            {
                if(cardIndex > 0)
                {
                    flipper.FlipCard(cardModel.faces[cardIndex-1],cardModel.faces[cardIndex],cardIndex);
                }
                else
                {
                    flipper.FlipCard(cardModel.cardBack,cardModel.faces[cardIndex],cardIndex);
                }
                cardIndex++;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
