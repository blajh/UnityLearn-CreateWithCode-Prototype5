using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Place all GameObjects that you'd like to be spawned as Targets
    /// </summary>
    [Tooltip("Place all GameObjects that you'd like to be spawned as Target")]
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    private float spawnRate = 1.0f;
    private int score;

    public bool isGameActive;

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Coroutine that spawns all objects from targets
    /// </summary>
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    /// <summary>
    /// Adds scoreToAdd to score and updates the scoreText UI element
    /// </summary>

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
