using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //UI
    public GameObject menu;
    public GameObject icons;
    public GameObject pauseBtn;
    public GameObject resume;
    public ControlUI helpBtns;

    //Game
    public GameObject astroids;
    public GameObject starGenerator;
    public GameObject player;

    public MusicPlayer musicPlayer;
    public Transform playerStartPosition;
    public ScoreManager scoreManager;
    private PlayerController playerController;
    private AstroidGenerator astroidGenerator;
    public FogGenerator fogGenerator;
    public ObjectPooler[] aPools;
    public ObjectPooler sPool;

    public PowerUpManager powerUpManager;

    public GameObject flightControlLeft;
    public GameObject flightControlRight;

    private bool firstGame;
    private bool paused;

    // Use this for initialization
    void Start() {
        playerController = player.GetComponent<PlayerController>();
        astroidGenerator = astroids.GetComponent<AstroidGenerator>();
        menu.SetActive(true);
        pauseBtn.SetActive(false);
        flightControlLeft.SetActive(false);
        flightControlRight.SetActive(false);
        playerController.enabled = false;
        astroidGenerator.enabled = false;
        resume.SetActive(false);

        firstGame = true;
        paused = false;
        powerUpManager.pauseGame();
        powerUpManager.resetGame();
    }

    // Update is called once per frame
    void Update() {
        //android back key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            if (menu.activeInHierarchy)
            {
                Application.Quit();
            }
            else
            {
                pauseGame();
            }
        }
    }

    private void resetGame()
    {
        //reset player
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
        //clear astroid bits
        AstroidBit[] bits = FindObjectsOfType<AstroidBit>();
        for (int a = 0; a < bits.Length; a++)
        {
            bits[a].gameObject.SetActive(false);
        }
        //freeze fog
        FogController[] fc = FindObjectsOfType<FogController>();
        for (int a = 0; a < fc.Length; a++)
        {
            fc[a].shouldMove(false);
        }

        //clear fog
        for (int a = 0; a < fc.Length; a++)
        {
            fc[a].gameObject.SetActive(false);
        }


        player.SetActive(true);
        scoreManager.reset();
        scoreManager.scoreTextEnabled(true);
        powerUpManager.resetGame();
    }

    public void pauseGame()
    {
        showMenu();
        pauseBtn.SetActive(false);
        flightControlLeft.SetActive(false);
        flightControlRight.SetActive(false);
        scoreManager.collectPoint(false);
        helpBtns.gameObject.SetActive(false);

        Time.timeScale = 0f;
        musicPlayer.stopMusic();
        powerUpManager.pauseGame();
        resume.SetActive(true);
        paused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        hideMenu();
        pauseBtn.SetActive(true);
        flightControlLeft.SetActive(true);
        flightControlRight.SetActive(true);
        scoreManager.collectPoint(true);
        musicPlayer.resumeMusic();
        powerUpManager.resumeGame();
        resume.SetActive(false);
        paused = false;
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        pauseBtn.SetActive(true);
        flightControlLeft.SetActive(true);
        flightControlRight.SetActive(true);
        scoreManager.collectPoint(true);
        scoreManager.onExit();
        resetGame();
        musicPlayer.resumeMusic();
        fogGenerator.enabled = true;
        fogGenerator.startGame();
        hideMenu();
        powerUpManager.start();
        paused = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        hideMenu();
        astroids.SetActive(true);
        starGenerator.SetActive(true);
        pauseBtn.SetActive(true);
        flightControlLeft.SetActive(true);
        flightControlRight.SetActive(true);
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
        powerUpManager.start();

        //reset player
        playerController.resetPlayer();
        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
        player.SetActive(true);

        resume.SetActive(false);
        paused = false;

        Time.timeScale = 1;
    }

    //called when player dies
    public void gameOver()
    {
        showMenu();
        scoreManager.collectPoint(false);
        scoreManager.scoreTextEnabled(false);
        astroids.SetActive(false);
        starGenerator.SetActive(false);
        pauseBtn.SetActive(false);
        flightControlLeft.SetActive(false);
        flightControlRight.SetActive(false);
        helpBtns.gameObject.SetActive(false);

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

        powerUpManager.stopGame();

        playerController.enabled = false;

    }

    public void startButtonClick()
    {
        if (paused)
        {
            resumeGame();
        }
        else
        {
            startGame();
        }
    }

    public void openLeaderBoard()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LeaderBoard");
    }

    public void hideMenu()
    {
        icons.SetActive(false);
        menu.SetActive(false);
    }

    public void showMenu()
    {
        icons.SetActive(true);
        menu.SetActive(true);
    }





    
}
