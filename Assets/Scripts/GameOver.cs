using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text secondsSurvivedUI;
    bool gameOver;

    void Start()
    {
        //Get access to PlayerController
        //Subscribing OnGameOver method to Onplayerdeath event
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //load first scene (i.e. the only scene)
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver()
    {
        //enables object
        gameOverScreen.SetActive(true);
        secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();

        gameOver = true;
    }
}
