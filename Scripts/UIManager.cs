using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get { return _instance; } }
    private static UIManager _instance;

    public AudioSource audioSource;

    [Header("Start Scene")]
    public GameObject startUI;
    public Animator startAnim;

    [Header("Shooting Gallery")]
    public GameObject sgPanel;
    public GameObject sgHTPPanel;

    [Header("Wanage")]
    public GameObject wPanel;
    public GameObject wHTPPanel;
    public GameObject wSpawnButton;

    [Header("Electric Rush")]
    public GameObject erPanel;
    public GameObject erHTPPanel;

    [Header("Fireworks")]
    public GameObject fwPanel;
    public Button fwStartButton;

    [Header("Whack A Mole")]
    public GameObject kPanel;
    public GameObject kHTPPanel;

    private void Awake()
    {
        // UIManager singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("WARNING: More than 1 UIManager script exist in this scene!");
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fwPanel.SetActive(false);
        sgPanel.SetActive(false);
        sgHTPPanel.SetActive(false);
        wPanel.SetActive(false);
        wHTPPanel.SetActive(false);
        wSpawnButton.SetActive(true);
        erPanel.SetActive(false);
        erHTPPanel.SetActive(false);
        kPanel.SetActive(false);
        kHTPPanel.SetActive(false);

        startAnim = startUI.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Both ShowUIPanel and HideUIPanel are used in TriggerStart.cs,
    // to show or hide UI Panels without playing UI sound effect.
    public void ShowUIPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HideUIPanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ShowMainPanel(GameObject panel)
    {
        panel.SetActive(true);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    public void HideMainPanel(GameObject panel)
    {
        panel.SetActive(false);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    public void ShowPanel(GameObject panelToShow, GameObject panelToHide)
    {
        panelToShow.SetActive(true);
        panelToHide.SetActive(false);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    public void HidePanel(GameObject panelToShow, GameObject panelToHide)
    {
        panelToShow.SetActive(true);
        panelToHide.SetActive(false);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    // Shooting Gallery start button
    public void OnSGStartPressed()
    {
        SGManager.instance.timeLimit = SGManager.instance.gameTime;
        SGManager.instance.score = 0;
        SGManager.instance.startGame = true;
        SGManager.instance.moveTargets = true;
        SGManager.instance.isFinished = false;
        SGManager.instance.isPlaying = true;
        GameManager.instance.defaultWalkable.SetActive(false);
        GameManager.instance.sgWalkable.SetActive(true);
        GameManager.instance.sgDetection.SetActive(false);
        UIManager.instance.HideMainPanel(UIManager.instance.sgPanel);
        UIManager.instance.HideMainPanel(UIManager.instance.sgHTPPanel);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
        MusicManager.instance.defBGM.Pause();
        MusicManager.instance.sgBGM.Play();
    }

    // Wanage start button
    public void OnWStartPressed()
    {
        UIManager.instance.HideMainPanel(UIManager.instance.wPanel);
        UIManager.instance.HideMainPanel(UIManager.instance.wHTPPanel);
        GameManager.instance.defaultWalkable.SetActive(false);
        GameManager.instance.wWalkable.SetActive(true);
        GameManager.instance.wDetection.SetActive(false);
        WManager.instance.startGame = true;
        WManager.instance.isPlaying = true;
        WManager.instance.remainingPoles = WManager.instance.totalPoles;
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    // For spawning hoop in WManager.cs
    public void SpawnHoop()
    {
        // If spawnCounter in WManager is 1, it means that there is a hoop spawned,
        // then the loop will not run
        if (WManager.instance.spawnCounter >= 1)
        {
            return;
        }
        else
        {
            // If it is 0, then spawn a hoop.
            WManager.instance.isSpawnable = true;
            WManager.instance.spawnCounter++;
        }
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    // Electric Rush start button
    public void OnERStartPressed()
    {
        UIManager.instance.HideMainPanel(UIManager.instance.erPanel);
        UIManager.instance.HideMainPanel(UIManager.instance.erHTPPanel);
        GameManager.instance.defaultWalkable.SetActive(false);
        GameManager.instance.erWalkable.SetActive(true);
        GameManager.instance.erDetection.SetActive(false);
        ERManager.instance.isStartButtonPressed = true;
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    // Main Menu start button
    public void OnStartButtonPresse()
    {
        Debug.Log("Start Pressed!");
        startAnim.SetTrigger("Start");
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }

    // Whack A Mole start button
    public void OnKStartButtonPressed()
    {
        KManager.instance.timeLimit = KManager.instance.gameTime;
        KManager.instance.score = 0;
        KManager.instance.startGame = true;
        KManager.instance.moveKokeshis = true;
        KManager.instance.isFinished = false;
        KManager.instance.isPlaying = true;
        GameManager.instance.defaultWalkable.SetActive(false);
        GameManager.instance.kWalkable.SetActive(true);
        GameManager.instance.kDetection.SetActive(false);
        UIManager.instance.HideMainPanel(UIManager.instance.kPanel);
        UIManager.instance.HideMainPanel(UIManager.instance.kHTPPanel);
        audioSource.pitch = Random.Range(0.7f, 1.2f);
        audioSource.Play();
    }
}
