using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BanditState
{
    random,
    stagger,
    chase
}
public class Bandit : MonoBehaviour
{
    public float pv;
    public Float_Value max_pv;
    public float speed;
    public Transform target;
    public float chase_radius;
    public float attack_radius;
    private Rigidbody2D rigid;
    public BanditState state;
    public float damage;
    public PV_Item equiped_weapon;
    public HealthBar healthbar;

    // random movement stuff
    //----------------------------
    public float randomRadius;
    public float randomTimer = 2f;
    private float randomTimeLeft;
    private Vector3 randomMove;
    //----------------------------
    // bullet firing stuff
    //----------------------------
    public GameObject bulletPrefab;
    private Shoot shoot;
    public float fireTimer = 2f;
    private float fireTimeLeft;
    //----------------------------

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        state = BanditState.random;

        pv = max_pv.runtime_value;
        damage = equiped_weapon.puissance;

        if (equiped_weapon.pv_item_type == PV_ItemType.couteau)
            chase_radius = 4;
        else
            chase_radius = 8;

        healthbar.SetMaxHealth(max_pv.runtime_value);
        healthbar.SetHealth(pv);

        shoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == BanditState.random)
            MoveRandom();

        CheckDistance();
    }

    private void CheckDistance()
    {
        float targetDistance = Vector3.Distance(target.position,
        transform.position);

        if (targetDistance <= chase_radius && state != BanditState.stagger)
        {
            switch (equiped_weapon.pv_item_type)
            {
                case (PV_ItemType.couteau):
                    if (targetDistance > attack_radius)
                    {
                        Vector3 tmp = Vector3.MoveTowards(transform.position,
                        target.position, speed * Time.deltaTime);

                        rigid.MovePosition(tmp);
                    }
                    break;
                case (PV_ItemType.pistolet):
                    fireTimeLeft -= Time.deltaTime;
                    if (fireTimeLeft <= 0)
                    {
                        shoot.BanditFire(target);
                        fireTimeLeft += fireTimer;
                    }
                    break;
            }

            ChangeState(BanditState.chase);
        }

        if (targetDistance > chase_radius)
            ChangeState(BanditState.random);
    }

    private void MoveRandom()
    {
        randomTimeLeft -= Time.deltaTime;
        if (randomTimeLeft <= 0)
        {
            randomMove = transform.position +
            (Vector3)Random.insideUnitCircle * randomRadius;

            randomTimeLeft += randomTimer;
        }

        Vector3 tmp = Vector3.MoveTowards(transform.position,
        randomMove, speed * Time.deltaTime);

        rigid.MovePosition(tmp);
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
        ChangeState(BanditState.chase);
    }
}
