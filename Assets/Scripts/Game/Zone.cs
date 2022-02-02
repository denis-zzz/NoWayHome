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
    public bool first_time = true;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(true);

            other.GetComponent<Player>().zone = this;
            if (hostileZone)
            {
                waveSignal.raise();
            }

            if (checkpoint)
            {
                saveSignal.raise();
                if (SceneManager.GetActiveScene().name != "Calibration"
                && first_time)
                {
                    adaptSignal.raise();
                    first_time = false;
                }

            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(false);
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Bandit"))
                    Destroy(child.gameObject);
            }
        }
    }
}
