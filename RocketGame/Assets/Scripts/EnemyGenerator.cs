﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

    public List<GameObject> spawns;
    public List<GameObject> enemyPool;
    public float spawnTimer;

    private float timer;
    private float gameTime;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        gameTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            timer = 0;
            spawnEnemy();
        }
	}

    private void spawnEnemy()
    {
        Transform  pos = spawns[Random.Range(0, spawns.Count)].transform;
        for(int a = 0; a < enemyPool.Count; a++)
        {
            if (!enemyPool[a].activeInHierarchy)
            {
                //Debug.Log("Spawning: "+a);
                GameObject enemy = enemyPool[a];
                enemy.SetActive(true);
                enemy.GetComponent<EnemyController>().spawn(new Vector3(pos.position.x, pos.position.y, 0));
            }
            else
            {
                //Debug.Log("Cant Spawn: " + a);
            }
                
        }
        
    }

    public void disable()
    {
        for (int a = 0; a < enemyPool.Count; a ++)
        {
            enemyPool[a].GetComponent<EnemyController>().enabled = false;
        }
    }

    //TODO
    public void reset()
    {
        gameTime = 0;
        timer = 0;
        for (int a = 0; a < enemyPool.Count; a++)
        {
            enemyPool[a].GetComponent<EnemyController>().enabled = true;
        }
    }

    public void startGame()
    {

    }
}
