using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour {

    public List<PowerUpInterface> powerUpPools;
    public List<GameObject> activeObjects;
    public float timeBetweenPowerUps;
    public float timeVar;

    public Transform holderPosition;

    private PowerUpInterface powerUpHolder;
    private float timer;
    private float nextPowerUp;
    private bool isActive;

    // Use this for initialization
    void Start() {
        activeObjects = new List<GameObject>();
        resetGame();
    }

    // Update is called once per frame
    void Update() {

        timer += Time.deltaTime;
        if (timer > nextPowerUp)
        {
            timer = 0;
            nextPowerUp = generateRandomTime();
            spawn();
        }

    }

    public void spawn()
    {

    }


    public void resetGame()
    {
        nextPowerUp = generateRandomTime();
        for (int a = 0; a < activeObjects.Count; a++)
            activeObjects[a].SetActive(false);
        activeObjects = new List<GameObject>();
        timer = 0;
    }
    public void pauseGame()
    {
        isActive = false;
    }
    public void resumeGame()
    {
        isActive = true;
    }
    private float generateRandomTime()
    {
        return Random.Range(timeBetweenPowerUps - timeVar, timeBetweenPowerUps + timeVar);
    }

    public void setPowerUp(PowerUpInterface newPower)
    {
        powerUpHolder = newPower;
    }
}