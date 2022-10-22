using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get { return _instance; } }
    private static MusicManager _instance;

    public AudioSource defBGM;
    public AudioSource sgBGM;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("WARNING: More than 1 MusicManager exist in this scene!");
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
        
    }
}
