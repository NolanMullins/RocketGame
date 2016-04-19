using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject astroids;
    public GameObject player;
    public GameObject pauseBtn;
    public Transform playerStartPosition;
    public ScoreManager scoreManager;
    private PlayerController playerController;
    private AstroidGenerator astroidGenerator;

    // Use this for initialization
    void Start() {
        playerController = player.GetComponent<PlayerController>();
        astroidGenerator = astroids.GetComponent<AstroidGenerator>();
        mainMenu.SetActive(true);
        pauseBtn.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        playerController.enabled = false;
        astroidGenerator.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        //android back key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

        }
    }

    private void resetGame()
    {
        playerController.resetPlayer();
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;

        AstroidController[] ac = FindObjectsOfType<AstroidController>();
        for (int a = 0; a < ac.Length; a++)
        {
            ac[a].gameObject.SetActive(false);
        }
        player.SetActive(true);
        scoreManager.reset();
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        scoreManager.collectPoint(false);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        scoreManager.collectPoint(true);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        gameOverMenu.SetActive(false);
        scoreManager.collectPoint(true);
        scoreManager.onExit();
        resetGame();

    }

    public void exitToMain()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        astroids.SetActive(false);
        pauseBtn.SetActive(false);
        gameOverMenu.SetActive(false);
        playerController.enabled = false;
        astroidGenerator.enabled = false;
        scoreManager.scoreTextEnabled(false);
        scoreManager.onExit();
        resetGame();
        
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        
    }

    public void startGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        astroids.SetActive(true);
        pauseBtn.SetActive(true);
        playerController.enabled = true;
        astroidGenerator.enabled = true;
        scoreManager.scoreTextEnabled(true);
        scoreManager.collectPoint(true);
        resetGame();
    }

    //called when player dies
    public void gameOver()
    {
        gameOverMenu.SetActive(true);
        scoreManager.collectPoint(false);
        scoreManager.onExit();

    }





    
}
