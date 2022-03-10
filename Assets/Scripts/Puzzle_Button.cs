using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Button : MonoBehaviour
{
    private Animator anim;
    public Animator barriraAnim;
    public BoxCollider2D box;
    private bool isTrigger;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnPressed()
    {
        anim.SetBool("press",true);
        barriraAnim.SetBool("down", true);
    }

    void OnExit()
    {
        anim.SetBool("press", false);
        barriraAnim.SetBool("down", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("stone"))
        {
            OnPressed();
            if (!isTrigger)
            {
                box.enabled = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("stone"))
        {
            OnExit();
            if (!isTrigger)
            {
                box.enabled = true;
            }
        }
    }

}
