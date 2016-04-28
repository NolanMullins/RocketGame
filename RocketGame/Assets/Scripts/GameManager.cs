using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject astroids;
    public GameObject starGenerator;
    public GameObject player;
    public GameObject pauseBtn;
    public ControlUI helpBtns;
    public MusicPlayer musicPlayer;
    public Transform playerStartPosition;
    public ScoreManager scoreManager;
    private PlayerController playerController;
    private AstroidGenerator astroidGenerator;
    public FogGenerator fogGenerator;
    public ObjectPooler[] aPools;
    public ObjectPooler sPool;

    private bool firstGame;

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

        firstGame = true;
    }

    // Update is called once per frame
    void Update() {
        //android back key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            if (mainMenu.activeInHierarchy)
            {
                Application.Quit();
            }
            else if (pauseMenu.activeInHierarchy)
            {
                exitToMain();
            }
            else if (gameOverMenu.activeInHierarchy)
            {
                exitToMain();
            }
            else
            {
                pauseGame();
            }
        }
    }

    private void resetGame()
    {
        playerController.resetPlayer();
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
        astroids.SetActive(true);
        starGenerator.SetActive(true);
        astroidGenerator.reset();
        fogGenerator.reset();
        AstroidController[] ac = FindObjectsOfType<AstroidController>();
        for (int a = 0; a < ac.Length; a++)
        {
            ac[a].gameObject.SetActive(true);
            ac[a].GetComponent<AstroidController>().shouldMove(true);
            ac[a].gameObject.SetActive(false);
        }

        //Unfreeze stars
        StarController[] stars = FindObjectsOfType<StarController>();
        for (int a = 0; a < stars.Length; a++)
        {
            stars[a].shouldMove(true);
        }

        //clear fog
        FogController[] fc = FindObjectsOfType<FogController>();
        for (int a = 0; a < fc.Length; a++)
        {
            fc[a].gameObject.SetActive(false);
        }

        player.SetActive(true);
        scoreManager.reset();
        scoreManager.scoreTextEnabled(true);
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        scoreManager.collectPoint(false);
        Time.timeScale = 0f;
        musicPlayer.stopMusic();
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        scoreManager.collectPoint(true);
        musicPlayer.resumeMusic();
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
        musicPlayer.resumeMusic();
        fogGenerator.enabled = true;
        fogGenerator.startGame();
    }

    public void exitToMain()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        astroids.SetActive(false);
        starGenerator.SetActive(false);
        pauseBtn.SetActive(false);
        gameOverMenu.SetActive(false);
        playerController.enabled = false;
        astroidGenerator.enabled = false;
        fogGenerator.enabled = false;

        scoreManager.onExit();
        resetGame();
        musicPlayer.stopMusic();
        scoreManager.scoreTextEnabled(false);
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        astroids.SetActive(true);
        starGenerator.SetActive(true);
        pauseBtn.SetActive(true);
        playerController.enabled = true;
        astroidGenerator.enabled = true;
        scoreManager.scoreTextEnabled(true);
        scoreManager.collectPoint(true);
        fogGenerator.enabled = true;

        resetGame();
        musicPlayer.startMusic();
        if (firstGame)
        {
            helpBtns.startAnimation();
            firstGame = false;
        }

        fogGenerator.startGame();
    }

    //called when player dies
    public void gameOver()
    {
        gameOverMenu.SetActive(true);
        scoreManager.collectPoint(false);
        scoreManager.scoreTextEnabled(false);
        astroids.SetActive(false);
        starGenerator.SetActive(false);
        pauseBtn.SetActive(false);

        fogGenerator.enabled = false;
        //TODO
        //freeze fog
        FogController[] fc = FindObjectsOfType<FogController>();
        for (int a = 0; a < fc.Length; a++)
        {
            fc[a].shouldMove(false);
        }
        //freeze planets
        PlanetController[] pc = FindObjectsOfType<PlanetController>();
        for (int a = 0; a < pc.Length; a++)
        {
            pc[a].shouldMove(false);
        }

        //freeze stars
        StarController[] stars = FindObjectsOfType<StarController>();
        for (int a = 0; a < stars.Length; a++)
        {
            stars[a].shouldMove(false);
        }

        for (int a = 0; a < aPools.Length; a++)
        {
            for (int b = 0; b < aPools[a].getPool().Count; b++)
            {
                aPools[a].getPool()[b].GetComponent<AstroidController>().shouldMove(false);
            }
        }
  
        scoreManager.onExit();
        musicPlayer.stopMusic();

    }





    
}
