using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (WManager.instance.isPlaying != true)
        {
            return;
        }
        else
        {
            // If collided with a game object that have HoopPole tagged, set the hit bool in WManager to true,
            // the spawn counter will be set to 0 to allows next hoop to spawn, also it will destroy this game object and deactivate other.
            // A sound effect of hitting the pole will also be played.
            if (other.CompareTag("HoopPole"))
            {
                WManager.instance.hit = true;
                WManager.instance.spawnCounter = 0;
                WManager.instance.audioSource.pitch = Random.Range(0.9f, 1.2f);
                WManager.instance.audioSource.Play();
                Destroy(gameObject);
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                //Debug.Log("Remain Poles: " + WManager.instance.remainingPoles);
                //Debug.Log("Hoop Hit!");
                
            }

            // If collided with a game object that have HoopDestroyArea tagged, set the miss bool in WManager to true,
            // Set spawn counter to 0 to allow next spawn and destory this game object.
            if (other.CompareTag("HoopDestroyArea"))
            {
                WManager.instance.miss = true;
                WManager.instance.spawnCounter = 0;
                Destroy(gameObject);
                //Debug.Log("Remain Hoops: " + WManager.instance.remainingHoops);
                //Debug.Log("Hoop Missed!");
            }

            // If collided with a game object that have HoopDespawnArea tagged,
            // Only set the spawn counter to 0 without reducing the remaining hoops in case player have accdentially throw the hoop outside of the play area.
            // Destroy this game object.
            if (other.CompareTag("HoopDespawnArea"))
            {
                WManager.instance.spawnCounter = 0;
                Destroy(gameObject);
            }
        }
    }
}
