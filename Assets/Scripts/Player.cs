using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public float speed;
    public float jump;
    private bool isJumping;
    private bool doubleJumping;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(move * speed, rig.velocity.y);

        if (move > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (move < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(move == 0 && !isJumping)
        {
            anim.SetInteger("transicao", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transicao", 2);
                rig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                isJumping = true;
                doubleJumping = true;
            }
            else if (doubleJumping)
            {
                anim.SetInteger("transicao", 2);
                rig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                doubleJumping = false;
            } 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
        }   
    }
}
