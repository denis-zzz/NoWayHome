using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour
{
    public float speed;
    public PlayerState state;
    private float og_speed;
    private Rigidbody2D rigid;
    private Vector3 change;
    private Animator anim;
    public PV_Item equiped_weapon;
    public float damage;
    public float pv;
    private PlayerShoot shoot;
    public int ammo;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        state = PlayerState.walk;
        anim = GetComponent<Animator>();
        og_speed = speed;

        damage = equiped_weapon.puissance;
        shoot = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        speed = og_speed;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (state == PlayerState.walk && change != Vector3.zero)
        {
            Walk();
        }

        if (Input.GetButtonDown("Fire1") && state != PlayerState.attack
        && state != PlayerState.stagger)
        {
            state = PlayerState.attack;
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        if (equiped_weapon.pv_item_type == PV_ItemType.couteau)
        {
            anim.SetTrigger("attack_melee");
            if (Mathf.Abs(Mathf.RoundToInt(shoot.attackDir.x)) ==
            Mathf.Abs(Mathf.RoundToInt(shoot.attackDir.y)))
            {
                shoot.attackDir.y = 0;
            }
            anim.SetFloat("aimX", Mathf.RoundToInt(shoot.attackDir.x));
            anim.SetFloat("aimY", Mathf.RoundToInt((shoot.attackDir.y)));
        }
        else
        {
            if (ammo > 0)
            {
                shoot.Shoot();
                ammo -= 1;
            }
        }
        yield return null;
        state = PlayerState.walk;
    }

    private void Walk()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = og_speed + 2;
        }
        rigid.MovePosition(transform.position + change.normalized * speed *
        Time.fixedDeltaTime);

        anim.SetFloat("moveX", change.x);
        anim.SetFloat("moveY", change.y);
    }

    public void ChangeState(PlayerState new_state)
    {
        if (state != new_state)
        {
            state = new_state;
        }
    }

    public void WaitKnock()
    {
        StartCoroutine(KnockCo());
    }

    private IEnumerator KnockCo()
    {
        yield return new WaitForSeconds(.2f);
        rigid.velocity = Vector2.zero;
        ChangeState(PlayerState.walk);
    }
}
