using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidStorm : EventShell {

    public ObjectPooler miniAstroids;
    public ObjectPooler pads;
    public AstroidGenerator gen;
    public float stormDensity;
    public float minSize,maxSize;

    public float stormLength;
    private float endTime;
    private float next;

    private float bound = 2.5f;

    public GameObject spawnHeight;

	// Use this for initialization
	void Start () {
        endTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log();
        if ((Time.time > endTime))
            return;
        //generate storm
        if (next < Time.time)
            spawn();
	}

    public override void startEvent()
    {
        endTime = Time.time+stormLength;
        next = Time.time + stormDensity;
    }

    public override void resetEvent()
    {
        endTime = 0;
    }

    public void spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-bound, bound), spawnHeight.transform.position.y, 1);
        GameObject obj = miniAstroids.getPooledObject();
        obj.transform.position = pos;
        obj.SetActive(true);
        float scale = Random.Range(minSize, maxSize);
        obj.transform.localScale = new Vector3(scale, scale, scale);
        AstroidBitPad pad = pads.getPooledObject().GetComponent<AstroidBitPad>();
        pad.attemptSpawn(pos, obj);
        AstroidController ac = obj.GetComponent<AstroidController>();
        ac.setVelocity(0, gen.getSpeed());
        ac.shouldMove(true);
        next = Time.time + stormDensity;
    }

    public override bool isRunning()
    {
        return (Time.time < endTime);
    }

    public override void disableEvent()
    {
        endTime = 0;
    }

}
