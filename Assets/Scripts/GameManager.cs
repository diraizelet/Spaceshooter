using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    public bool isCoopMode = false;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    public void GameOver()
    {
        _isGameOver = true;
    }
    private void start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true && isCoopMode == false)
        {
            _isGameOver = false;
            SceneManager.LoadScene(1);
        }
        else if(Input.GetKeyDown(KeyCode.R) && _isGameOver ==true && isCoopMode == true)
        {
            _isGameOver = false;
            SceneManager.LoadScene(2);
        }
        if(Input.GetKeyDown(KeyCode.Tab) && _isGameOver == true )
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Called resume game");
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}