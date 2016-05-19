using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {

    public List<PowerUpInterface> powerUps;
    public float timeBetweenPowerUps;
    public float timeVar;

    public AstroidGenerator astroids;

    public float leftBound;
    public float rightBound;

    public GameObject powerDisplay;
    public Image powerDisplayImage;
    public List<Sprite> powerUpImages;

    public DistancePad pad;


    //public Transform holderPosition;

    private PowerUpInterface powerUpHolder;
    private float timer;
    private float nextPowerUp;
    private bool isActive;
    private bool used;

    // Use this for initialization
    void Start() {
        resetGame();
        powerDisplay.SetActive(false);
        powerDisplayImage.gameObject.SetActive(false);
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
        int type = Random.Range(0, (powerUps.Count-1)/2)*3;
        for (int a = 0; a < 3; a++)
        {
            if (!powerUps[type + a].gameObject.activeInHierarchy)
            {
                powerUps[type + a].spawn(leftBound, rightBound, astroids.getSpeed());
                break;
            }
        }
    }

    public void resetGame()
    {
        nextPowerUp = generateRandomTime();
        for (int a = 0; a < powerUps.Count; a++)
            powerUps[a].gameObject.SetActive(false);
        timer = 0;
        used = true;
        if (powerUpHolder != null)
            powerUpHolder.gameObject.SetActive(false);
        powerDisplayImage.gameObject.SetActive(false);
    }
    public void pauseGame()
    {
        isActive = false;
        for (int a = 0; a < powerUps.Count; a++)
        {
            powerUps[a].stop();
        }
    }
    public void resumeGame()
    {
        isActive = true;
        for (int a = 0; a < powerUps.Count; a++)
        {
            powerUps[a].start();
        }
    }

    public void stopGame()
    {
        powerDisplay.SetActive(false);
        for (int a = 0; a < powerUps.Count; a++)
        {
            powerUps[a].stop();
        }
        isActive = false;
    }

    public void start()
    {
        powerDisplay.SetActive(true);
        for (int a = 0; a < powerUps.Count; a++)
        {
            powerUps[a].start();
        }
        isActive = true;
    }
    private float generateRandomTime()
    {
        return Random.Range(timeBetweenPowerUps - timeVar, timeBetweenPowerUps + timeVar);
    }

    public void setPowerUp(PowerUpInterface newPower, int type)
    {
        if (powerUpHolder != null)
            powerUpHolder.gameObject.SetActive(false);
        powerUpHolder = newPower;
        powerDisplayImage.gameObject.SetActive(true);
        powerDisplayImage.sprite = powerUpImages[type];
        used = false;
    }

    public bool isOpen()
    {
        return true;
        //return (used);
    }

    public void activatePower()
    {
        if (!used)
        {
            if (powerUpHolder.usePower())
            {
                powerDisplayImage.gameObject.SetActive(false);
                powerUpHolder = null;
                used = true;
            }
        }
    }

    public AstroidGenerator getGenerator()
    {
        return astroids;
    }

    public DistancePad getDistPad()
    {
        return pad;
    }
}