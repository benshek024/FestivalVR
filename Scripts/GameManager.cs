using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using Valve.VR;

/// <summary>
/// Abbreviations of the name of Minigames
/// 
/// sg = Shooting Gallery
/// w = Wanage (Hoop Throw)
/// er = Electric Rush
/// fw = Fireworks
/// k = Whack A Mole
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get { return _instance; } }
    private static GameManager _instance;

    [Header("Player Types")]
    public GameObject steamVRRig;
    public GameObject fallbackRig;

    [Header("Post Processing")]
    public PostProcessVolume ppVolume;
    private Bloom _bloom;
    private DepthOfField _dof;

    [Header("Walkable Planes")]
    public GameObject defaultWalkable;
    public GameObject sgWalkable;
    public GameObject wWalkable;
    public GameObject erWalkable;
    public GameObject startWalkable;
    public GameObject kWalkable;

    [Header("Detection Areas")]
    public GameObject sgDetection;
    public GameObject wDetection;
    public GameObject erDetection;
    public GameObject fwDetection;
    public GameObject kDetection;

    private void Awake()
    {
        // GameManager singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("WARNING: More than 1 GameManager script exist in this scene!");
        }
        else
        {
            _instance = this;
        }

        /*
        if (SteamVR.instance != null)
        {
            steamVRRig.SetActive(true);
            fallbackRig.SetActive(false);
            _bloom.intensity.value = 20f;
            _dof.focalLength.value = 90f;
            _dof.focusDistance.value = 1.7f;
            _dof.aperture.value = 7f;
            _dof.focalLength.value = 25;
        }
        else
        {
            fallbackRig.SetActive(true);
            steamVRRig.SetActive(false);
            _bloom.intensity.value = 1.5f;
            _dof.focusDistance.value = 1.9f;
            _dof.aperture.value = 7f;
            _dof.focalLength.value = 25;
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        // Deactivates all but the startWalkable game object.
        // Activates all detection areas.
        startWalkable.SetActive(true);
        defaultWalkable.SetActive(false);
        sgWalkable.SetActive(false);
        wWalkable.SetActive(false);
        erWalkable.SetActive(false);
        kWalkable.SetActive(false);
        sgDetection.SetActive(true);
        wDetection.SetActive(true);
        erDetection.SetActive(true);
        fwDetection.SetActive(true);
        kDetection.SetActive(true);
    }
}
