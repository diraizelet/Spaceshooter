using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    public Text highscore;
    private GameManager _gamemanager;
    public GameObject _GameoverSequence;
    [SerializeField]
    private Text _escsequence;
    private int bestscore;
    // Start is called before the first frame update
    void Start()
    {
        _text.text = "Score: " + 0;
        bestscore = PlayerPrefs.GetInt("HighScore",0);
        highscore.text = "High Score: " + bestscore.ToString();
        _gameOver.gameObject.SetActive(false);
        _gamemanager = GameObject.Find("Game_manager").GetComponent<GameManager>();

        if(_gamemanager == null )
        {
            Debug.LogError("Game Manager is NULL!!");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _text.text = "Score: " + playerScore.ToString();
        if(playerScore >bestscore)
        {
            bestscore = playerScore;
            PlayerPrefs.SetInt("HighScore",bestscore);
            highscore.text = "High Score: " + bestscore.ToString();
        }
    }

    
    public void UpdateLives(int currentlives)
    {
        _livesImg.sprite = _livesSprites[currentlives];
        if(currentlives == 0)
        {
            _gameOver.gameObject.SetActive(true);
        }
    }

    public void LastSceneGameOver()
    {
        _gamemanager.GameOver();
        _GameoverSequence.gameObject.SetActive(true);
       // _restartText.gameObject.SetActive(true);
        //_escsequence.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOver.text = "Game Over !!";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void ResumePlay()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}

