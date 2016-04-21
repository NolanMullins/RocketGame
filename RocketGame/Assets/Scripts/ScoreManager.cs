﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float scorePerSecond;
    public Text scoreText;
    public Text pauseScore;
    public Text highScoreText;
    private float score;
    private float highScore;
    private bool isAlive;

    //planets
    public ObjectPooler[] planets;
    public int[] spawnAtPoints;
    private bool[] used;
    public PlanetGenerator planetGenerator;

    private string distUnit = "km";

    // Use this for initialization
    void Start() {
        //load high score
        highScore = 0;
        if (PlayerPrefs.HasKey("HS"))
            highScore = PlayerPrefs.GetFloat("HS");
        isAlive = true;

        used = new bool[planets.Length];

        reset();
    }

    // Update is called once per frame
    void Update() {
        if (isAlive)
        {
            score += scorePerSecond * Time.deltaTime;
            for (int a = 0; a < spawnAtPoints.Length; a++)
            {
                if (spawnAtPoints[a] <= score && !used[a])
                {
                    planetGenerator.spawnPlanet(a, planets[a].getPooledObject());
                    used[a] = true;
                }
            }
        }
        scoreText.text = Mathf.Round(score) + " " + distUnit;
        pauseScore.text = Mathf.Round(score) + " " + distUnit;
        highScoreText.text = "High Score\n" + Mathf.Round(highScore) + " " + distUnit;
    }

    

    public void onExit()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HS", highScore);
        }
    }

    public void collectPoint(bool should)
    {
        isAlive = should;
    }

    public void addScore(float add)
    {
        score += add;
    }

    public void reset()
    {
        score = 0;
        for (int a = 0; a < used.Length; a++)
        {
            used[a] = false;
        }
        planetGenerator.clearPlanets();
    }

    public void scoreTextEnabled(bool enabled)
    {
        scoreText.gameObject.SetActive(enabled);
    }
}
