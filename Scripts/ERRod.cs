using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERRod : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (ERManager.instance.isPlaying)
        {
            if (collision.gameObject.CompareTag("ERObstacle"))
            {
                ERManager.instance.lose = true;
                Debug.Log("Game Over!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ERManager.instance.isStartButtonPressed)
        {
            if (other.gameObject.CompareTag("ERStartPoint"))
            {
                ERManager.instance.startGame = true;
                Debug.Log("Game Start!");
            }
        }

        if (ERManager.instance.isPlaying)
        {
            if (other.gameObject.CompareTag("EREndPoint"))
            {
                ERManager.instance.win = true;
                Debug.Log("You Win!");
            }
        }
    }
}
