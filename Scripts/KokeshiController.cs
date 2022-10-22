using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokeshiController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        anim = GetComponentInParent<Animator>();
        anim.SetTrigger("Start");
    }

    private void OnCollisionEnter(Collision col)
    {
        // Check if isPlaying bool is true in KManager, run below if loop if it is true.
        if (KManager.instance.isPlaying == true)
        {
            if (col.gameObject.CompareTag("Hammer"))
            {
                // Check if Rise bool in Kokeshi's animator is true, do nothing if it is false to prevent scoring even if the Kokeshi is lowered.
                if (anim.GetBool("Rise"))
                {
                    anim.SetBool("Lower", true);
                    anim.SetBool("Rise", false);
                    KManager.instance.score++;
                    audioSource.pitch = Random.Range(0.9f, 1.2f);
                    audioSource.Play();
                }
            }
        }
    }
}
