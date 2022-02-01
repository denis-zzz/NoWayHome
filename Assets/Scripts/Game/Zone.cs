using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone : MonoBehaviour
{
    public GameObject virtualcam;
    public bool hostileZone;
    public bool checkpoint;
    public List<Transform> spawnPoints;
    public SignalSender waveSignal;
    public SignalSender adaptSignal;
    public SignalSender saveSignal;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<Player>().zone = this;
            if (hostileZone)
            {
                waveSignal.raise();
            }

            if (checkpoint)
            {
                saveSignal.raise();
                if (SceneManager.GetActiveScene().name != "Calibration")
                {
                    adaptSignal.raise();
                }

            }
            virtualcam.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(false);
        }
    }
}
