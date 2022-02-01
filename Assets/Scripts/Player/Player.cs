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
    private Shoot shoot;
    public Inventory playerInventory;
    private Item ammo;
    public HealthBar healthbar;
    public Zone zone;
    public Float_Value damage;
    public Inventory inventory;
    // SIGNALS STUFF
    //------------------------------------
    public SignalSender tir_signal;
    public SignalSender stab_signal;
    public SignalSender sprint_signal;
    public SignalSender knife_signal;
    public SignalSender gun_signal;
    public SignalSender immobile_signal;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        state = PlayerState.walk;
        anim = GetComponent<Animator>();
        og_speed = speed;

        shoot = GetComponent<Shoot>();

        healthbar.SetMaxHealth(max_pv.initial_value);
        healthbar.SetHealth(max_pv.runtime_value);
    }

    // Update is called once per frame
    void FixedUpdate()
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
        else
        {
            immobile_signal.raise();
        }

        if (Input.GetButtonDown("Fire1") && equiped_weapon != null &&
        state != PlayerState.attack && state != PlayerState.stagger)
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
        else if (equiped_weapon.pv_item_type == PV_ItemType.pistolet)
        {
            ammo = playerInventory.items.Find((x) => x.item_type == ItemType.munition);
            if (ammo != null && ammo.quantite > 0)
            {
                tir_signal.raise();
                shoot.Fire();
                ammo.quantite -= 1;
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
        max_pv.runtime_value -= damage;
        SetHealth();
    }

    public void Heal(float heal)
    {
        max_pv.runtime_value += heal;
        SetHealth();
    }

    public void SetHealth()
    {
        healthbar.SetHealth(max_pv.runtime_value);
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
            save_pv = max_pv.runtime_value,
            save_ammo = ammo,
            save_state = state,
            save_wep_type = equiped_weapon.pv_item_type
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (PlayerSaveData)state;
        var pos = saveData.position;
        transform.position = new Vector3(pos[0], pos[1]);
        max_pv.runtime_value = saveData.save_pv;
        healthbar.SetHealth(max_pv.runtime_value);
        ammo = saveData.save_ammo;
        state = saveData.save_state;
        PV_ItemType type = saveData.save_wep_type;
        switch (type)
        {
            case PV_ItemType.couteau:
                knife_signal.raise();
                break;
            case PV_ItemType.pistolet:
                gun_signal.raise();
                break;
        }
    }
}

[Serializable]
public class PlayerSaveData
{
    public float[] position;
    public float save_pv;
    public Item save_ammo;
    public PlayerState save_state;
    public PV_ItemType save_wep_type;
}
