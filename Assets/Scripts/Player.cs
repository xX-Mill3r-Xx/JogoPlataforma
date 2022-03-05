using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;
    public float radius;
    public float speed;
    public float jump;
    private bool isJumping;
    private bool doubleJumping;
    private bool isAtacking;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        Atack();
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
            if (!isJumping && !isAtacking)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (move < 0)
        {
            if (!isJumping && isAtacking)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(move == 0 && !isJumping && !isAtacking)
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

    void Atack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAtacking = true;
            anim.SetInteger("transicao", 3);

            Collider2D hit = Physics2D.OverlapCircle(point.position, radius);
            if (hit != null)
            {

            }
            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.13f);
        isAtacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
        }   
    }
}
