using UnityEngine;
using System.Collections;
using System;

public class BallSpawner : MonoBehaviour
{
    public int Force;

    private Vector3 Direction
    {
        get
        {
            return gameObject.transform.GetChild(0).position - gameObject.transform.position;
        }
    }

	// Use this for initialization
	void Start ()
    {
        Invoke("startBallSpawningCoroutine", 2);
    }

    private void startBallSpawningCoroutine()
    {
        StartCoroutine("startBallSpawning");
    }

    private IEnumerator startBallSpawning()
    {
        for (;;)
        {
            Ball newBall = PrefabFactory.GetBall();

            //TODO Random Enum - probably move this out
            Array values = Enum.GetValues(typeof(Ball.BallColor));
            System.Random random = new System.Random();
            Ball.BallColor randomColor = (Ball.BallColor)values.GetValue(random.Next(values.Length));

            newBall.setColor(randomColor);
            newBall.gameObject.transform.parent = gameObject.transform;
            newBall.gameObject.transform.position = gameObject.transform.position;
            newBall.gameObject.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
            yield return new WaitForSeconds(2f);
        }
    }
}
