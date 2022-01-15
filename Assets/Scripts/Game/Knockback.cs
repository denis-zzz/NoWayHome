using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bandit") ||
        other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("Bandit")
                && other.GetComponent<Bandit>().state != BanditState.stagger
                && other.isTrigger)
                {
                    Vector2 difference = hit.transform.position -
                    transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);

                    hit.GetComponent<Bandit>().ChangeState(BanditState.stagger);

                    float damage =
                    this.transform.parent.GetComponent<Player>().damage;

                    other.GetComponent<Bandit>().WaitKnock(damage);
                }

                if (other.gameObject.CompareTag("Player")
                && other.GetComponent<Player>().state != PlayerState.stagger
                && other.isTrigger)
                {
                    Vector2 difference = hit.transform.position -
                    transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);

                    hit.GetComponent<Player>().ChangeState(PlayerState.stagger);
                    other.GetComponent<Player>().WaitKnock();
                }
            }
        }
    }
}
