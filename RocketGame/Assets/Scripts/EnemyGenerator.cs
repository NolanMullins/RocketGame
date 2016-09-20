﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour {

    public List<GameObject> spawns;
    public List<GameObject> enemyPool;
    public float spawnTimer;

    //notification lights
    public AlertLight leftLight;
    public AlertLight rightLight;

    private float timer;
    private float gameTime;
    private float running;
    private IEnumerator spawn;
	// Use this for initialization
    void Start () {
        running = false;    
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
        Transform pos = spawns[Random.Range(0, spawns.Count)].transform;
        spawn = spawnSeq(pos);
        if (pos.position.x < 0)
            leftLight.startAnimation();
        else
            rightLight.startAnimation();

            StartCoroutine(spawn);        
    }

    IEnumerator spawnSeq(Transform pos)
    {
        running = true;
        for (int a = 0; a < enemyPool.Count; a++)
        {
            if (!enemyPool[a].activeInHierarchy)
            {
                //Debug.Log("Spawning: "+a);
                yield return new WaitForSeconds(3);
                GameObject enemy = enemyPool[a];
                enemy.SetActive(true);
                enemy.GetComponent<EnemyController>().spawn(new Vector3(pos.position.x, pos.position.y, 0));
                running = false;
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
        if (isGenerating())
        {
            StopCoroutine(spawn);
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

    public bool isGenerating()
    {
        return running;
    }

    public void startGame()
    {

    }
}
