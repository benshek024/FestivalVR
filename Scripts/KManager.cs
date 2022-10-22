using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KManager : MonoBehaviour
{
    public static KManager instance { get { return _instance; } }
    private static KManager _instance;

    [Header("Reset")]
    public Transform hammer;
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

    [Header("Kokeshi Configuation")]
    public GameObject[] kokeshis;
    public List<int> list = new List<int>();
    [Tooltip("Set the maximum targets can be raise everytime the targets was re-picked again")]
    public int maxkokeshis;
    public bool moveKokeshis = false;
    public bool randomPick = false;
    public bool reset = true;

    private void Awake()
    {
        // KManager singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("WARNING: More than 1 KManager script exist in this scene!");
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Lower all kokeshis at start
        foreach(GameObject kokeshi in kokeshis)
        {
            kokeshi.GetComponent<Animator>().SetBool("Lower", true);
            kokeshi.GetComponent<Animator>().SetBool("Rise", false);
        }

        // Set the game time to 2 minutes if the gameTime field is less than or equal to 0
        if (gameTime <= 0)
        {
            timeLimit = 120f;
        }
        else
        {
            timeLimit = gameTime;
        }

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
            RandomPickKokeshis();
            randomPick = false;
        }

        // Move the targets
        if (moveKokeshis)
        {
            MoveKokeshisCoroutine();
            moveKokeshis = false;
        }

        // End game process
        if (isFinished)
        {
            moveKokeshis = false;
            randomPick = false;
            startGame = false;
            StopCoroutine("MoveKokeshis");
            GameManager.instance.defaultWalkable.SetActive(true);
            GameManager.instance.kWalkable.SetActive(false);
            GameManager.instance.kDetection.SetActive(true);
            foreach (GameObject kokeshi in kokeshis)
            {
                kokeshi.GetComponent<Animator>().SetBool("Lower", true);
                kokeshi.GetComponent<Animator>().SetBool("Rise", false);
            }
            isPlaying = false;
            isFinished = false;
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

    void MoveKokeshisCoroutine()
    {
        StartCoroutine("MoveKokeshis");
    }

    // Rise and Lower Targets
    IEnumerator MoveKokeshis()
    {
        yield return new WaitForSeconds(2f);

        // If reset is true, lower all of the targets that was raised
        if (reset)
        {
            foreach (GameObject kokeshi in kokeshis)
            {
                kokeshi.GetComponent<Animator>().SetBool("Lower", true);
                kokeshi.GetComponent<Animator>().SetBool("Rise", false);
            }
        }

        yield return new WaitForSeconds(2f);

        // Stop the reset process and start picking targets randomly
        reset = false;
        randomPick = true;

        yield return new WaitForSeconds(3f);

        // After the targets was raised for 5 seconds, start the reset process and call the StartGame method again to repeat the process.
        reset = true;
        moveKokeshis = true;
    }

    // Generate random numbers without repetition and rise the targets that was chosen
    void RandomPickKokeshis()
    {
        list = new List<int>(new int[kokeshis.Length]);

        // Total targets that will be randomly picked according to the maxTargets variable 
        for (int i = 0; i < maxkokeshis; i++)
        {
            int index = Random.Range(0, kokeshis.Length);
            GameObject chosenTarget = kokeshis[index];

            while (list.Contains(index + 1))
            {
                index = Random.Range(0, kokeshis.Length);
                chosenTarget = kokeshis[index];
            }

            list[i] = index;

            chosenTarget.GetComponent<Animator>().SetBool("Rise", true);
            chosenTarget.GetComponent<Animator>().SetBool("Lower", false);
        }
    }

    public void ResetHammerPos()
    {
        hammer.position = resetPos.position;
    }
}
