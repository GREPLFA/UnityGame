using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster : MonoBehaviour
{
    float speed = 3.0f;
    float move;
    bool attackChcek = false;
    public bool gameStart = false;

    Animator animator;

    public GameObject target;
    Transform Play;

    void Awake()
    {
        target.gameObject.tag = "Player";
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Start()
    {
        Play = target.transform;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            gameStart = true;
        }
        if (gameStart)
        {
            Move();
            Reversal();
            Attack();
            Animation();
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (this.transform.position.x < Play.position.x)
        {
            move = 1.0f * Time.deltaTime;
        }
        else if (this.transform.position.x > Play.position.x)
        {
            move = -1.0f * Time.deltaTime;
        }
        else
        {
            move = 0;
        }

        if (this.transform.position.x - Play.position.x > 1.5f || this.transform.position.x - Play.position.x < -1.5f)
        {
            transform.position = new Vector2(transform.position.x + move * speed, transform.position.y);
        }
    }

    void Reversal()
    {
        if (!attackChcek)
        {
            if (move < 0)
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
            else if (move > 0)
            {
                transform.localScale = new Vector3(3, 3, 1);
            }
        }
    }

    void Attack()
    {
        if (this.transform.position.x - Play.position.x < 1.6f && this.transform.position.x - Play.position.x > -1.6f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackChcek = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
        else
        {
            attackChcek = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void Animation()
    {
        // move
        if (move != 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }

        // attack
        if (attackChcek)
        {
            animator.SetBool("isAttack", false);
        }
        else
        {
            animator.SetBool("isAttack", true);
        }
    }
}
