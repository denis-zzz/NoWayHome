using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BanditState
{
    walk,
    stagger
}
public class Bandit : MonoBehaviour
{
    public float pv;
    public Float_Value max_pv;
    public int degats;
    public float speed;
    private Transform target;
    public float chase_radius;
    public float attack_radius;
    private Rigidbody2D rigid;
    public BanditState state;
    public float damage;
    public PV_Item equiped_weapon;
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        state = BanditState.walk;
        pv = max_pv.initial_value;
        damage = equiped_weapon.puissance;
        healthbar.SetMaxHealth(max_pv.initial_value);
        healthbar.SetHealth(pv);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        float targetDistance = Vector3.Distance(target.position,
        transform.position);

        if (targetDistance <= chase_radius && targetDistance > attack_radius
        && state != BanditState.stagger)
        {
            Vector3 tmp = Vector3.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);

            rigid.MovePosition(tmp);
            ChangeState(BanditState.walk);
        }
    }

    public void ChangeState(BanditState new_state)
    {
        if (state != new_state)
        {
            state = new_state;
        }
    }

    public void TakeDamage(float damage)
    {
        pv -= damage;
        healthbar.SetHealth(pv);
        if (pv <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void WaitKnock(float damage)
    {
        StartCoroutine(KnockCo());
        TakeDamage(damage);
    }
    private IEnumerator KnockCo()
    {
        yield return new WaitForSeconds(.2f);
        rigid.velocity = Vector2.zero;
        ChangeState(BanditState.walk);
    }
}
