using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitant : Interactible
{
    private Vector3 direction;
    private Transform transf;
    private Rigidbody2D rigid;
    public float speed;
    private Animator anim;
    public Collider2D bounds;

    // Movemement stuff
    //-----------------------------------
    private bool isMoving;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;
    //-----------------------------------

    void Start()
    {
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
        transf = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        changeDirection();
    }

    void Update()
    {
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if (moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }
            if (!playerInRange)
            {
                Walk();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if (waitTimeSeconds <= 0)
            {
                chooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
            }
        }
    }

    private void Walk()
    {
        Vector3 tmp = transform.position + direction
        * speed * Time.fixedDeltaTime;
        if (bounds.bounds.Contains(tmp))
        {
            rigid.MovePosition(tmp);
        }
        else
        {
            changeDirection();
        }
    }

    private void updateAnim()
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    void changeDirection()
    {
        int dir = Random.Range(0, 4);

        switch (dir)
        {
            case 0:
                direction = Vector3.right;
                break;

            case 1:
                direction = Vector3.up;
                break;

            case 2:
                direction = Vector3.left;
                break;

            case 3:
                direction = Vector3.down;
                break;
        }
        updateAnim();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        chooseDifferentDirection();
    }

    private void chooseDifferentDirection()
    {
        Vector3 tmp = direction;
        changeDirection();
        int cpt = 0;
        while (tmp == direction && cpt < 100)
        {
            cpt++;
            changeDirection();
        }
    }
}


