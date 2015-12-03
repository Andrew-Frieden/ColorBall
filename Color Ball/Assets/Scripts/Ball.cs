using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public BallColor Color;

    void OnCollisionEnter2D(Collision2D col)
    {
        BallColorColliderController colorCollider = col.gameObject.GetComponent<BallColorColliderController>();

        if (colorCollider != null && colorCollider.Color == Color)
        {
            Destroy(gameObject);
        }
    }

    public enum BallColor
    {
        Green,
        Red,
        Blue
    }
}
