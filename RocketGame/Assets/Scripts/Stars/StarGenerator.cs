using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StarGenerator : MonoBehaviour {

    public float timeBetweenStars;
    public ObjectPooler starPool;
    public GameObject generationPoint;
    public GameObject destroyPoint;
    //scale
    public float smallest;
    public float biggest;
    //OG bounds = 2.75 somehow works lol
    public float xBound;

    private float timer;

    public float yScale;
    public float xScale;

    private float xBoundScale = 1.25f;
    private float xShiftPool;
    public float sideSpawnScale=0.2f;

    public Camera main;

	// Use this for initialization
	void Start () 
    {
        xBound = main.ViewportToWorldPoint(new Vector3(1, 0)).x;
        timer = 0;
        xShiftPool = 0;
        StartCoroutine(LateStart(0.1f));
	}

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //fnc is a go
        float y = destroyPoint.transform.position.y;
        //increase interval
        float yInc = timeBetweenStars * 0.2f;
        while (y < generationPoint.transform.position.y)
        {
            spawnStar(y);
            y += yInc;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenStars)
        {
            spawnStar(generationPoint.transform.position.y);
            timer = 0;
        }
	}

    private void spawnStar(float yVal) {
       float xVal = generationPoint.transform.position.x + Random.Range(-xBound*xBoundScale, xBound*xBoundScale);
       spawnStar(xVal, yVal);
    }

    private void spawnStar(float xPos, float yPos) {
        GameObject obj = starPool.getPooledObject();
        obj.transform.position = new Vector3(xPos, yPos, 0);
        obj.transform.rotation = transform.rotation;
        float scale = Random.Range(smallest, biggest);
        float bonusScale = scale/biggest;
        StarController sc = obj.GetComponent<StarController>();
        sc.setScale(xScale*bonusScale, yScale*bonusScale);
        sc.setXBound(xBound*xBoundScale);
        obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.SetActive(true);
    }

    public void shiftStars(float shift) 
    {
        trackXShift(shift);
        List<GameObject> pool = starPool.getPool();
        foreach (GameObject o in pool) 
        {
            if (o.activeInHierarchy)
                o.GetComponent<StarController>().shiftX(shift);
        }
    }

    private void trackXShift(float xShift) {
        xShiftPool += xShift;
        if (Mathf.Abs(xShiftPool) > sideSpawnScale) {
            if (xShiftPool > 0)
                spawnSide(-xBound*xBoundScale);
            else 
                spawnSide(xBound*xBoundScale);
            xShiftPool = 0;
        }
    }

    private void spawnSide(float xPos) {
        float y = destroyPoint.transform.position.y;
        //increase interval
        float yInc = timeBetweenStars * 4;
        while (y < generationPoint.transform.position.y)
        {
            spawnStar(xPos, y);
            y += yInc*Random.Range(0.25f, 1.75f);
        }
    }

}
