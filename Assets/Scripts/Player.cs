using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private PlayerAudios playerAudios;

    private Rigidbody2D rig;

    private bool isJumping;
    private bool doubleJumping;
    private bool isAtacking;
    private bool recovery;

    public Animator anim;
    public Transform point;
    public float radius;
    public float speed;
    public float jump;
    public int health;

    public LayerMask enemy_layer;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerAudios = GetComponent<PlayerAudios>();
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
                playerAudios.PlaySFX(playerAudios.jump);
            }
            else if (doubleJumping)
            {
                anim.SetInteger("transicao", 2);
                rig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                doubleJumping = false;
                playerAudios.PlaySFX(playerAudios.jump);
            } 
        }
    }

    void Atack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAtacking = true;
            anim.SetInteger("transicao", 3);

            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemy_layer);
            playerAudios.PlaySFX(playerAudios.hit);
            if (hit != null)
            {
                if (hit.GetComponent<Slime>())
                {
                    hit.GetComponent<Slime>().OnHit();
                }

                if (hit.GetComponent<Goblin>())
                {
                    hit.GetComponent<Goblin>().OnHit();
                }
            }
            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.13f);
        isAtacking = false;
    }

    float recovery_Count;
    public void onHit()
    {
        recovery_Count += Time.deltaTime;

        if (recovery_Count >= 1.8f)
        {
            anim.SetTrigger("hit");
            health--;
            recovery_Count = 0f;
        }
        
        if(health <= 0 && !recovery)
        {
            recovery = true;
            anim.SetTrigger("death");
            //Game Over Aqui;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            onHit();
        }

        if (collision.CompareTag("Coin"))
        {
            playerAudios.PlaySFX(playerAudios.coin);
            collision.GetComponent<Animator>().SetTrigger("hit");
            GameController.instance.GetCoin();
            Destroy(collision.gameObject, 0.3f);
        }

        if(collision.gameObject.layer == 7)
        {
            GameController.instance.NextLevel();
        }

        if (collision.gameObject.layer == 12)
        {
            PlayerPos.instance.CheckPoint();
        }
    }
}
