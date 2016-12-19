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
    public AstroidStorm storm;

    private float timer;
    private float gameTime;
    //OG @ 45s
    public int spawnEvent;
    public int spawnPlanet;

    private bool isGameActive;
    private bool planetFlip;
    private bool flip;

    // Use this for initialization
    void Start()
    {
        gameTime = 0;
        timer = 0;
        isGameActive = false;
        flip = false;
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

        //gen a storm
        //if (!storm.isActive())
            //storm.startStorm();

        //generate events
        if ((int)gameTime % spawnEvent == 0)
        {
            if (flip)
            {
                timer = 0;
                genEvent();
                flip = false;
            }
        }
        else if (!flip)
            flip = true;

        //spwn planets in the background
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

    public void genEvent()
    {
        int type = Random.Range(0,2);
        if (type==0)
        {
            enemyGen.spawnEnemy();
        }
        else if (type == 1)
        {
            storm.startStorm();
        }
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
        storm.stop();
    }
}
