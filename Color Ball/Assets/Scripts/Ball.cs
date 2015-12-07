using UnityEngine;
using System.Collections;
using System;

public class Ball : MonoBehaviour
{
    public BallColor Color;

    //TODO lets make a sprite helper and not do this in Ball.cs
    public Sprite RedBallSprite;
    public Sprite GreenBallSprite;
    public Sprite BlueBallSprite;

    public int TimeToLive;

    void Awake()
    {
        TimeToLive = 5;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BallColorColliderController colorCollider = col.gameObject.GetComponent<BallColorColliderController>();

        if (colorCollider != null && colorCollider.Color == Color)
        {
             PrefabFactory.CacheBall(gameObject);
        }
        else
        {
            TimeToLive--;

            if (TimeToLive <= 0)
            {
                PrefabFactory.CacheBall(gameObject);
            }
        }
    }

    public void setColor(BallColor color)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = getSpriteForBallColor(color);
        Color = color;
    }

    //TODO lets make a sprite helper and not do this in Ball.cs
    private Sprite getSpriteForBallColor(BallColor color)
    {
        switch (color)
        {
            case BallColor.Green:
                return GreenBallSprite;
            case BallColor.Red:
                return RedBallSprite;
            case BallColor.Blue:
                return BlueBallSprite;
            default:
                return null;
        }
    }

    public enum BallColor
    {
        Green,
        Red,
        Blue
    }
}
