using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Slider healthBarSlideer;

    public Text pointsText, gameOverPointsText;
    public int points = 0;

    public PlayerController playerController;
    public LevelLoader levelLoader;

    public GameObject gamePanel, gameOverPanel;

    public float levelEnd = 500f;

    private void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlideer.value = playerController.health;
        pointsText.text = points.ToString();
        gameOverPointsText.text = "Points: " + points;

        GameOver();
    }

    void GameOver()
    {
        if (playerController != null)
        {
            if (playerController.health <= 0 || playerController.gameObject.transform.position.z >= levelEnd)
            {
                gamePanel.SetActive(false);
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void OnClickLevel(int level)
    {
        levelLoader.LoadNextLevel(level);
    }

    public void OnClickPouse(float time)
    {
        Time.timeScale = time;
    }
}
