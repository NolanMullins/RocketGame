using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {

    public List<PowerUpInterface> powerUps;
    private List<GameObject> activeObjects;
    public float timeBetweenPowerUps;
    public float timeVar;

    public AstroidGenerator astroids;

    public float leftBound;
    public float rightBound;

    public GameObject powerDisplay;

    //public Transform holderPosition;

    private PowerUpInterface powerUpHolder;
    private float timer;
    private float nextPowerUp;
    private bool isActive;
    private bool used;

    // Use this for initialization
    void Start() {
        activeObjects = new List<GameObject>();
        resetGame();
        powerDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if (isActive)
            timer += Time.deltaTime;
        if (timer > nextPowerUp)
        {
            timer = 0;
            nextPowerUp = generateRandomTime();
            spawn();
        }

    }
    //TODO
    //make sure power ups dont spawn on astroids
    public void spawn()
    {
        int type = Random.Range(0, powerUps.Count/2)*2;
        if (!powerUps[type].gameObject.activeInHierarchy)
        {
            powerUps[type].spawn(leftBound,rightBound, astroids.getSpeed());
        }
        else
        {
            powerUps[type+1].spawn(leftBound, rightBound, astroids.getSpeed());
        }
    }


    public void resetGame()
    {
        nextPowerUp = generateRandomTime();
        for (int a = 0; a < activeObjects.Count; a++)
            activeObjects[a].SetActive(false);
        activeObjects = new List<GameObject>();
        timer = 0;
        used = true;
    }
    public void pauseGame()
    {
        isActive = false;
    }
    public void resumeGame()
    {
        isActive = true;
    }

    public void stopGame()
    {
        powerDisplay.SetActive(false);
        isActive = false;
    }

    public void start()
    {
        powerDisplay.SetActive(true);
        isActive = true;
    }
    private float generateRandomTime()
    {
        return Random.Range(timeBetweenPowerUps - timeVar, timeBetweenPowerUps + timeVar);
    }

    public void setPowerUp(PowerUpInterface newPower)
    {
        powerUpHolder = newPower;
        used = false;
    }

    public bool isOpen()
    {
        return (used);
    }

    public void activatePower()
    {
        if (!used)
        {
            powerUpHolder.usePower();
            powerUpHolder = null;
            used = true;
        }
    }
}