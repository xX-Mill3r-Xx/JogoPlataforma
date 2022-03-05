using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public bool isFront;
    private Vector2 direction;

    public bool isRight;
    public float stopDistance;

    public Transform point;
    public Transform behind;
    
    public float speed;
    public float maxVision;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        GetPlayer();
        OnMove();
    }

    void OnMove()
    {
        if (isFront)
        {
            anim.SetInteger("transicao",1);
            if (isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }

        }
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if(distance <= stopDistance)
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;
                    anim.SetInteger("transicao", 2);
                    hit.transform.GetComponent<Player>().onHit();
                }
            }
        }

        RaycastHit2D hitBehind = Physics2D.Raycast(behind.position, -direction, maxVision);
        if(hitBehind.collider != null)
        {
            if (hitBehind.transform.CompareTag("Player"))
            {
                isRight = !isRight;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(behind.position, -direction * maxVision);
    }


}
