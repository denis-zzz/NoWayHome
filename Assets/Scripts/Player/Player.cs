using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour, ISavable
{
    public float speed;
    public PlayerState state;
    private float og_speed;
    private Rigidbody2D rigid;
    private Vector3 change;
    private Animator anim;
    public PV_Item equiped_weapon;
    public Float_Value max_pv;
    public float pv;
    private Shoot shoot;
    public int ammo;
    public HealthBar healthbar;
    public Zone zone;
    public Float_Value damage;
    public Inventory inventory;
    // SIGNALS STUFF
    //------------------------------------
    public SignalSender tir_signal;
    public SignalSender stab_signal;
    public SignalSender sprint_signal;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        state = PlayerState.walk;
        anim = GetComponent<Animator>();
        og_speed = speed;

        damage.runtime_value = equiped_weapon.puissance;
        shoot = GetComponent<Shoot>();

        pv = max_pv.runtime_value;
        healthbar.SetMaxHealth(max_pv.runtime_value);
        healthbar.SetHealth(pv);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == PlayerState.interact)
            return;

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
            ChangeState(PlayerState.attack);
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        if (equiped_weapon.pv_item_type == PV_ItemType.couteau)
        {
            stab_signal.raise();
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
                tir_signal.raise();
                shoot.Fire();
                ammo -= 1;
            }
        }
        yield return null;
        if (state != PlayerState.interact)
            ChangeState(PlayerState.walk);
    }

    private void Walk()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint_signal.raise();
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

    public void WaitKnock(float damage)
    {
        StartCoroutine(KnockCo());
        TakeDamage(damage);
    }

    private IEnumerator KnockCo()
    {
        yield return new WaitForSeconds(.2f);
        rigid.velocity = Vector2.zero;
        ChangeState(PlayerState.walk);
    }

    public void TakeDamage(float damage)
    {
        pv -= damage;
        healthbar.SetHealth(pv);
    }

    public void receive_item()
    {
        anim.SetBool("receive_item", true);
        ChangeState(PlayerState.interact);
    }

    public void item_received()
    {
        anim.SetBool("receive_item", false);
        ChangeState(PlayerState.walk);
    }

    public void dialog_started()
    {
        ChangeState(PlayerState.interact);
    }

    public void dialog_ended()
    {
        ChangeState(PlayerState.walk);
    }

    public object CaptureState()
    {
        var saveData = new PlayerSaveData()
        {
            position = new float[]{transform.position.x,
            transform.position.y},
            save_pv = pv,
            save_ammo = ammo,
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (PlayerSaveData)state;
        var pos = saveData.position;
        transform.position = new Vector3(pos[0], pos[1]);
        pv = saveData.save_pv;
        healthbar.SetHealth(pv);
        ammo = saveData.save_ammo;
    }
}

[Serializable]
public class PlayerSaveData
{
    public float[] position;
    public float save_pv;
    public int save_ammo;
}
