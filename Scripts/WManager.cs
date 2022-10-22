using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WManager : MonoBehaviour
{
    public static WManager instance { get { return _instance; } }
    private static WManager _instance;

    [Header("Integers")]
    public int totalHoops;
    [HideInInspector] public int remainingHoops;
    public int totalPoles;
    [HideInInspector] public int remainingPoles;
    [HideInInspector] public int spawnCounter = 0;

    [Header("Booleans")]
    public bool startGame = false;
    public bool isEntered = false;
    public bool isPlaying = false;
    public bool isFinished = false;
    public bool isSpawnable = false;
    public bool hit = false;
    public bool miss = false;

    [Header("Despawn Areas")]
    public List<GameObject> despawnAreas = new List<GameObject>();

    [Header("Hoop Pole Detectors")]
    public List<GameObject> detectors = new List<GameObject>();

    [Header("Score and Remaining Hoops")]
    public TMP_Text poleText;
    public TMP_Text hoopText;

    [Header("Hoop Spawning")]
    public GameObject spawnPos;
    public GameObject hoopPrefab;
    public List<GameObject> hoops = new List<GameObject>();

    [Header("Sound Effect")]
    public AudioSource audioSource;
    public AudioClip hitClip;

    private void Awake()
    {
        // WManager singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("WARNING: More than 1 WManager script exist in this scene!");
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Clamp the spawnCounter
        spawnCounter = Mathf.Clamp(spawnCounter, 0, 1);

        // Set totalPoles related to the count of detectors
        totalPoles = detectors.Count;

        remainingPoles = totalPoles;
        remainingHoops = totalHoops;
        poleText.text = remainingPoles + " / " + totalPoles;
        hoopText.text = remainingHoops + " / " + totalHoops;

        // Deactivates all of the detectors and despawn areas, and destroy all hoops and clears the hoops List at start.
        foreach (GameObject dArea in despawnAreas)
        {
            dArea.SetActive(false);
        }

        foreach (GameObject detector in detectors)
        {
            detector.GetComponent<CapsuleCollider>().enabled = false;
        }

        foreach (GameObject hoop in hoops)
        {
            Destroy(hoop);
        }

        hoops.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        poleText.text = remainingPoles + " / " + totalPoles;
        hoopText.text = remainingHoops + " / " + totalHoops;

        // Game start initialization process
        if (startGame && isEntered && isPlaying)
        {
            startGame = false;
            spawnCounter = 0;

            GameManager.instance.defaultWalkable.SetActive(false);
            GameManager.instance.wWalkable.SetActive(true);

            // Activates all of the despawn areas and detectors, 
            // destory any hoops that are spawned and clear the hoops List.
            foreach (GameObject dArea in despawnAreas)
            {
                dArea.SetActive(true);
            }

            foreach (GameObject detector in detectors)
            {
                detector.GetComponent<CapsuleCollider>().enabled = true;
            }

            foreach (GameObject hoop in hoops)
            {
                Destroy(hoop);
            }
            hoops.Clear();
        }

        if (isPlaying)
        {
            // If the hoop hits one of the poles that have detectors game object on it,
            // reduce both remaining hoops and poles by 1.
            if (hit)
            {
                hit = false;
                remainingHoops--;
                remainingPoles--;
            }

            // Reduce the remaining hoop by 1 only if the hoop did NOT hit the detector on the pole.
            if (miss)
            {
                miss = false;
                remainingHoops--;
            }
        }

        // Finish the game if BOTH remaining hoops and spawn counter is 0,
        // so that the player can finish throw the last hoop before the game ends.
        if (remainingHoops <= 0 && spawnCounter <= 0)
        {
            remainingHoops = totalHoops;
            isFinished = true;
        }

        if (isFinished)
        {
            isFinished = false;
            isPlaying = false;
            startGame = false;
            GameManager.instance.defaultWalkable.SetActive(true);
            GameManager.instance.wWalkable.SetActive(false);
            GameManager.instance.wDetection.SetActive(true);

            // Deactivates all of the despawn areas and detectors,
            //destory any hoops that are spawned and clear the hoops List when the game is over.
            foreach (GameObject dArea in despawnAreas)
            {
                dArea.SetActive(false);
            }

            foreach (GameObject detector in detectors)
            {
                detector.GetComponent<CapsuleCollider>().enabled = false;
            }

            foreach (GameObject hoop in hoops)
            {
                Destroy(hoop);
            }

            hoops.Clear();
        }

        // After player has pressed spawn button, it will set isSpawnable to false to stop in spawning process.
        // A hoop will be instantiated and the hoops List will add the instantiated hoop to the List so it can be destoryed along with other instantiated hoops
        // afther the game is over or started.
        if (isSpawnable && spawnCounter >= 1)
        {
            isSpawnable = false;
            GameObject hoop = (GameObject)Instantiate(hoopPrefab, spawnPos.transform.position, Quaternion.identity);
            hoops.Add(hoop);
        }
        else
        {
            return;
        }
    }
}
