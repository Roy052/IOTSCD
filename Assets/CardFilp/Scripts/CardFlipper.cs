using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardModel model;
    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponent<CardModel>();
    }

    public void FlipCard(Sprite startImage, Sprite EndImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, EndImage, cardIndex));
        StartCoroutine(Flip(startImage, EndImage, cardIndex));
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex)
    {
        spriteRenderer.sprite = startImage;
        float time = 0f;
        while(time <= 1)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;
            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
            
            if(time>=0.5f)
            {
                spriteRenderer.sprite = endImage;
            }
            yield return new WaitForFixedUpdate();
        }
        if(cardIndex==-1)
        {
            model.cardIndex = cardIndex;
            model.ToggleFace(true);
        }
    }
}
