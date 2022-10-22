using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip riseClip;
    [SerializeField] private AudioClip lowerClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Start");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if isPlaying bool is true in SGManager, run below if loop if it is true.
        if (SGManager.instance.isPlaying == true)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // Check if Rise bool in Target's animator is true, do nothing if it is false to prevent scoring even if the Target is lowered.
                if (anim.GetBool("Rise"))
                {
                    anim.SetBool("Lower", true);
                    anim.SetBool("Rise", false);
                    SGManager.instance.score++;
                    audioSource.pitch = Random.Range(0.9f, 1.1f);
                    audioSource.Play();
                }
            }
        }
    }

    public void Rise()
    {
        audioSource.PlayOneShot(riseClip, 0.6f);
    }

    public void Lower()
    {
        audioSource.PlayOneShot(lowerClip, 0.6f);
    }
}
