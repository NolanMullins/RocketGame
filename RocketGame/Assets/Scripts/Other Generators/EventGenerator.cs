using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class will generate events
 * Enemy
 * Gravity Well**
 * Fog / dust
 */

/* Need to take over planet generator generator
 */

public class EventGenerator : MonoBehaviour
{

    public EnemyGenerator enemyGen;

    private float timer;
    private float gameTime;
    //OG @ 45s
    public float spawnTimer;

    // Use this for initialization
    void Start()
    {
        gameTime = 0;
        timer = 0;
    }

    //called on start of new game
    public void startGame()
    {
        gameTime = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            timer = 0;
            enemyGen.spawnEnemy();
        }
    }

    public void reset()
    {
        gameTime = 0;
        timer = 0;
        enemyGen.reset();
    }

    public void disable()
    {
        enemyGen.disable();
    }
}
