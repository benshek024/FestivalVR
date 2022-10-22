using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SGManager : MonoBehaviour
{
    public static SGManager instance { get { return _instance; } }
    private static SGManager _instance;

    [Header("Reset")]
    public Transform gun;
    public Transform resetPos;

    [Header("Game Timer")]
    [Tooltip("Set how long the game will last")]
    public float gameTime;
    [HideInInspector] public float timeLimit;
    public TMP_Text timerText;
    public bool startTimer;

    [Header("Score")]
    public int score;
    private int totalScore;
    public TMP_Text scoreText;

    [Header("Booleans")]
    public bool startGame = false;
    public bool isEntered = false;
    public bool isPlaying = false;
    public bool isFinished = false;

    [Header("Targers Configuation")]
    public GameObject[] targets;
    public List<int> list = new List<int>();
    [Tooltip("Set the maximum targets can be raise everytime the targets was re-picked again")]
    public int maxTargets;
    [HideInInspector] public bool moveTargets = false;
    [HideInInspector] public bool randomPick = false;
    [HideInInspector] public bool reset = true;

    private void Awake()
    {
        // SGManager singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("WARNING: More than 1 SGManager script exist in this scene!");
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Lower all targets at start
        foreach (GameObject target in targets)
        {
            target.GetComponent<Animator>().SetBool("Lower", true);
            target.GetComponent<Animator>().SetBool("Rise", false);
        }

        // Set the game time to 3 minutes if the gameTime field is less than or equal to 0
        if (gameTime <= 0)
        {
            timeLimit = 180f;
        }
        else
        {
            timeLimit = gameTime;
        }

        //Debug.Log("Game Time: " + timeLimit);
        totalScore = score;
        scoreText.text = "0000";
        timerText.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        // Start and update the timer when startGame the condition is met. stops if false.
        if (startGame && isEntered && isPlaying)
        {
            startTimer = true;
            UpdateScoreText();
        }

        // Start the timer if startTimer boolean is true
        if (startTimer)
        {
            if (timeLimit >= 0)
            {
                timeLimit -= Time.deltaTime;
                StartTimer(timeLimit, timerText);

                // End the game if time has ran out
                if (timeLimit <= 0)
                {
                    //Debug.Log("Game Over!");
                    timeLimit = 0;
                    timerText.text = "00:00";
                    startTimer = false;
                    isFinished = true;
                }
            }
        }

        // Randomly picking some targets and rise it
        if (randomPick)
        {
            RandomPickTargets();
            randomPick = false;
        }

        // Move the targets
        if (moveTargets)
        {
            MoveTargetsCoroutine();
            moveTargets = false;
        }

        // End game process
        if (isFinished)
        {
            moveTargets = false;
            randomPick = false;
            startGame = false;
            StopCoroutine("MoveTargets");
            GameManager.instance.defaultWalkable.SetActive(true);
            GameManager.instance.sgWalkable.SetActive(false);
            GameManager.instance.sgDetection.SetActive(true);
            foreach (GameObject target in targets)
            {
                target.GetComponent<Animator>().SetBool("Lower", true);
                target.GetComponent<Animator>().SetBool("Rise", false);
            }
            isPlaying = false;
            isFinished = false;
            MusicManager.instance.defBGM.Play();
            MusicManager.instance.sgBGM.Stop();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = string.Format("{0:0000}", score);
    }

    void StartTimer(float time, TMP_Text timerText)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void MoveTargetsCoroutine()
    {
        StartCoroutine("MoveTargets");
    }

    // Rise and Lower Targets
    IEnumerator MoveTargets()
    {
        yield return new WaitForSeconds(2f);

        // If reset is true, lower all of the targets that was raised
        if (reset)
        {
            foreach (GameObject target in targets)
            {
                target.GetComponent<Animator>().SetBool("Lower", true);
                target.GetComponent<Animator>().SetBool("Rise", false);
            }
        }

        yield return new WaitForSeconds(1f);

        // Stop the reset process and start picking targets randomly
        reset = false;
        randomPick = true;

        yield return new WaitForSeconds(3f);

        // After the targets was raised for 5 seconds, start the reset process and call the StartGame method again to repeat the process.
        reset = true;
        moveTargets = true;
    }

    // Generate random numbers without repetition and rise the targets that was chosen
    void RandomPickTargets()
    {
        list = new List<int>(new int[targets.Length]);

        // Total targets that will be randomly picked according to the maxTargets variable 
        for (int i = 0; i < maxTargets; i++)
        {
            int index = Random.Range(0, targets.Length);
            GameObject chosenTarget = targets[index];

            while (list.Contains(index + 1))
            {
                index = Random.Range(0, targets.Length);
                chosenTarget = targets[index];
            }

            list[i] = index;

            chosenTarget.GetComponent<Animator>().SetBool("Rise", true);
            chosenTarget.GetComponent<Animator>().SetBool("Lower", false);
        }
    }

    public void ResetGunPos()
    {
        gun.position = resetPos.position;
    }
}
