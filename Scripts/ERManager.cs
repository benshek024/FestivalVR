using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERManager : MonoBehaviour
{
    public static ERManager instance { get { return _instance; } }
    private static ERManager _instance;

    [Header("Reset Position")]
    public Transform rodPos;
    public Transform resetPos;

    [Header("Booleans")]
    public bool startGame = false;
    public bool isPlaying = false;
    public bool isEntered = false;
    public bool isStartButtonPressed = false;
    public bool win = false;
    public bool lose = false;

    [Header("Obstacle Animators")]
    public Animator roFan1Anim;
    public Animator roFan2Anim;
    public Animator springAnim;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("WARNING: More than 1 ERManager exist in this scene!");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartButtonPressed)
        {
            isStartButtonPressed = false;
            startGame = true;
        }

        if (startGame && isEntered)
        {
            isPlaying = true;
            startGame = false;
            roFan1Anim.SetBool("Rotate", true);
            roFan2Anim.SetBool("Rotate", true);
            springAnim.SetBool("Rotate", true);
        }

        if (isPlaying)
        {
            if (win)
            {
                win = false;
                startGame = false;
                isPlaying = false;
                Debug.Log("WIN!");
                roFan1Anim.SetBool("Rotate", false);
                roFan2Anim.SetBool("Rotate", false);
                springAnim.SetBool("Rotate", false);
                GameManager.instance.erDetection.SetActive(true);
                GameManager.instance.defaultWalkable.SetActive(true);
                GameManager.instance.erWalkable.SetActive(false);
                audioSource.PlayOneShot(winClip, 0.7f);
            }

            if (lose)
            {
                lose = false;
                startGame = false;
                isPlaying = false;
                Debug.Log("LOSE!");
                roFan1Anim.SetBool("Rotate", false);
                roFan2Anim.SetBool("Rotate", false);
                springAnim.SetBool("Rotate", false);
                GameManager.instance.erDetection.SetActive(true);
                GameManager.instance.defaultWalkable.SetActive(true);
                GameManager.instance.erWalkable.SetActive(false);
                audioSource.PlayOneShot(loseClip, 0.7f);
            }
        }
    }

    public void ResetRod()
    {
        rodPos.position = resetPos.position;
    }
}
