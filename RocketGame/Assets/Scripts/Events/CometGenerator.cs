using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometGenerator : EventShell {

	public GameObject leftSpawn;
	public GameObject rightSpawn;
    public ObjectPooler cometPool;

    public AstroidGenerator astroidGenerator;

    private bool running;
    private IEnumerator spawn;
	// Use this for initialization
    void Start () {
        running = false;    
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void startEvent()
    {
        spawn = spawnComet();
		StartCoroutine(spawn);        
    }

    IEnumerator spawnComet()
    {
        running = true;
        
        //hold astroid generation
        astroidGenerator.enabled = false;
        yield return new WaitForSeconds(0.5f);
        //create commet
        GameObject comet = cometPool.getPooledObject();
        comet.SetActive(true);
        
        float spawnX = Random.Range(leftSpawn.transform.position.x, rightSpawn.transform.position.x);
        float spawnY = leftSpawn.transform.position.y;

        float rise = spawnY - this.transform.position.y;
        float run = spawnX - (this.transform.position.x+Random.Range(-1.2f,1.2f));

        float velY = astroidGenerator.getSpeed();
        float scale = velY/rise;
        float velX = run*scale;
        
        float theta = Mathf.Atan(rise/run)* Mathf.Rad2Deg;
        float rotation = 90 - (theta < 0 ? -theta : theta);

        if (theta > 0)
            rotation *= -1;

        comet.SetActive(true);
        CometController controller = comet.GetComponent<CometController>();
        controller.enabled = true;
        controller.spawn(new Vector3(spawnX, spawnY, 0), velX, velY);
        comet.transform.eulerAngles = new Vector3(0,0,rotation);
        //wait n seconds then turn astroids back on
        yield return new WaitForSeconds(0.5f);
        astroidGenerator.enabled = true;

        running = false;        
    }

    public override void disableEvent()
    {        
        foreach (GameObject comet in cometPool.getPool())
            comet.GetComponent<CometController>().enabled = false;

        if (isRunning())
        {
            astroidGenerator.enabled = true;
            StopCoroutine(spawn);
        }
    }

    //TODO
    public override void resetEvent()
    {
        foreach (GameObject comet in cometPool.getPool())
            comet.SetActive(false);
    }

    //returns if the event is currently generating a comet
    public override bool isRunning()
    {
        return running;
    }

    public override void togglePause(bool paused)
    {
        foreach (GameObject comet in cometPool.getPool())
            comet.GetComponent<CometController>().shouldMove(!paused);
    }
}
