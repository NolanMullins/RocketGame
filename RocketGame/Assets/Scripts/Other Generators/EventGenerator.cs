using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class will generate events
 * Enemy
 * Gravity Well**
 * Fog / dust
 */

public class EventGenerator : MonoBehaviour
{

    public EnemyGenerator enemyGen;
    public PlanetGenerator planetGenerator;

    private float timer;
    private float gameTime;
    //OG @ 45s
    public int spawnEnemy;
    public int spawnPlanet;

    private bool isGameActive;
    private bool planetFlip;
    private bool enemyFlip;

    // Use this for initialization
    void Start()
    {
        gameTime = 0;
        timer = 0;
        isGameActive = false;
        enemyFlip = false;
    }

    //called on start of new game
    public void startGame()
    {
        gameTime = 1;
        timer = 0;
        isGameActive = true;
        planetFlip = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive)
            return;
        gameTime += Time.deltaTime;
        timer += Time.deltaTime;
        //if (gameTime >= spawnEnemy)
        if ((int)gameTime % spawnEnemy == 0)
        {
            if (enemyFlip)
            {
                timer = 0;
                enemyGen.spawnEnemy();
                enemyFlip = false;
            }
        }
        else if (!enemyFlip)
            enemyFlip = true;
        if ((int)gameTime % spawnPlanet == 0)
        {
            if (planetFlip)
            {
                planetGenerator.spawnNextPlanet();
                planetFlip = false;
            }
        }
        else if (!planetFlip)
                planetFlip = true;     
                
               
        //if (planetTimer >= planetTimer)
            //planetGenerator.spawnNextPlanet();
    }

    public void reset()
    {
        gameTime = 0;
        timer = 0;
        enemyGen.reset();
        planetGenerator.reset();
    }

    public void disable()
    {
        enemyGen.disable();
        isGameActive = false;
    }
}
