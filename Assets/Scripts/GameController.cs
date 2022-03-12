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
    public GameObject gameoverPannel;

    private void Awake()
    {

        Time.timeScale = 1f;
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

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameoverPannel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
