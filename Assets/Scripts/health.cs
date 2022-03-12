using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Image[] hearts;
    public Sprite heart;
    public Sprite Noheart;
    public int life;
    public int heartsCount = 16;

    void Update()
    {
        for(int i=0; i < hearts.Length; i++)
        {
            if (i < life)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = Noheart;
            }


            if (i < heartsCount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
