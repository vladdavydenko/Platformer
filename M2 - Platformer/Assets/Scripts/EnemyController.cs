using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float enemySpeed = 3;
    public float rangeY = 2;
    Vector3 initialPos;
    int direction = 1;

	void Start () {
        initialPos = transform.position;
	}
	
	void Update () {
        float factor = direction == -1 ? 2f : 1;
        float movementY = factor * enemySpeed * Time.deltaTime * direction;
        float newY = transform.position.y + movementY;

        if(Mathf.Abs(newY - initialPos.y) > rangeY)
        {
            direction *= -1;
        }
        else
        {
            transform.position += new Vector3(0, movementY, 0);
        }
    }
}
