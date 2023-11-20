using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public AudioClip audioClipSuccess;
    public AudioClip audioClipFailure;
    public AudioSource audioSource;

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            audioSource.PlayOneShot(audioClipSuccess);
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if (!gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(true);
            audioSource.PlayOneShot(audioClipFailure);
        }
    }
}
