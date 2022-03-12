using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public static GameController instance;

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
        
        if(PlayerPrefs.GetInt("Moedas") > 0)
        {
            score += PlayerPrefs.GetInt("Moedas");
            scoreText.text = "x " + score.ToString();
        }

    }


    public void GetCoin()
    {
        score++;
        scoreText.text = "x "+ score.ToString();

        PlayerPrefs.SetInt("Moedas", score);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
