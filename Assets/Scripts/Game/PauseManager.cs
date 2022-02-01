using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public SignalSender inventaire_signal;

    private bool isPaused;
    public GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangePause();
        }

    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
            inventaire_signal.raise();
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}