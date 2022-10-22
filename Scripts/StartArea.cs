using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArea : MonoBehaviour
{
    public GameObject startUI;

    // Start is called before the first frame update
    void Start()
    {
        startUI.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.instance.startWalkable.SetActive(false);
        GameManager.instance.defaultWalkable.SetActive(true);
        startUI.SetActive(false);
    }
}
