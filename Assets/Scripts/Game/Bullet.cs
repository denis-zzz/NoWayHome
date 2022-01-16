using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Float_Value gun_damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Bandit") ||
        other.gameObject.CompareTag("Player")) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("Bandit")
                && other.GetComponent<Bandit>().state != BanditState.stagger
                && other.isTrigger)
                {
                    float damage =
                    GameObject.FindGameObjectWithTag("Player").
                    GetComponent<Player>().damage.runtime_value;

                    other.GetComponent<Bandit>().TakeDamage(damage);
                }

                if (other.gameObject.CompareTag("Player")
                && other.GetComponent<Player>().state != PlayerState.stagger
                && other.isTrigger)
                {
                    other.GetComponent<Player>().
                    TakeDamage(gun_damage.runtime_value);
                }
            }
        }
        if (!other.gameObject.CompareTag("Zone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
