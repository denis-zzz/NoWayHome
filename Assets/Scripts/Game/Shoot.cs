using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Shoot : MonoBehaviour
{
    private Transform shootTransform;
    public Vector3 attackDir;
    private Animator anim;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float bulletForce;

    // Start is called before the first frame update
    void Start()
    {
        shootTransform = transform.Find("Shoot");
        anim = transform.Find("Shoot").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            attackDir = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
            shootTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void Fire()
    {
        anim.SetTrigger("Shoot");

        GameObject bullet = Instantiate(bulletPrefab,
        firepoint.position, firepoint.rotation);

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(attackDir * bulletForce, ForceMode2D.Impulse);
    }

    public void BanditFire(Transform target)
    {
        attackDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        shootTransform.eulerAngles = new Vector3(0, 0, angle);

        Fire();
    }
}
