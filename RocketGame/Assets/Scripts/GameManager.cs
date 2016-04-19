using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject astroids;
    public GameObject player;
    public GameObject pauseBtn;
    private PlayerController playerController;
    private AstroidGenerator astroidGenerator;

    // Use this for initialization
    void Start() {
        playerController = player.GetComponent<PlayerController>();
        astroidGenerator = astroids.GetComponent<AstroidGenerator>();
        mainMenu.SetActive(true);
        pauseBtn.SetActive(false);
        pauseMenu.SetActive(false);
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

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        Time.timeScale = 1;
    }

    public void restartGame()
    {
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);

        //todo
        //despawn astroids
        //reset Player position
        //reset score

        Time.timeScale = 1;
    }

    public void exitToMain()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        astroids.SetActive(false);
        pauseBtn.SetActive(false);
        playerController.enabled = false;
        astroidGenerator.enabled = false;

        //todo
        //despawn astroids
        //reset Player position
        //reset score
        
        Time.timeScale = 1;
    }

    public void quitGame()
    {

    }

    public void startGame()
    {
        mainMenu.SetActive(false);
        astroids.SetActive(true);
        pauseBtn.SetActive(true);
        playerController.enabled = true;
        astroidGenerator.enabled = true;
    }





    
}
