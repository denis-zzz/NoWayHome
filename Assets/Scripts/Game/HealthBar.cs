using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;

    void Awake()
    {
        slider.gameObject.SetActive(true);
        if (transform.parent.CompareTag("Player"))
        {
            transform.Find("HealthSlider").Find("HealthFill").
            GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            transform.Find("HealthSlider").Find("HealthFill").
            GetComponent<Image>().color = Color.red;
        }
    }
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(
            transform.parent.position + offset
        );
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }
}
