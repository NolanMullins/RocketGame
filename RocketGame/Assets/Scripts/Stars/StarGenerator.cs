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

    public Camera main;

	// Use this for initialization
	void Start () 
    {
        xBound = main.ViewportToWorldPoint(new Vector3(1, 0)).x;
        timer = 0;
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

    void spawnStar(float yVal) {
        GameObject obj = starPool.getPooledObject();
        obj.transform.position = new Vector3(generationPoint.transform.position.x + Random.Range(-xBound, xBound), yVal, 0);
        obj.transform.rotation = transform.rotation;
        float scale = Random.Range(smallest, biggest);
        float bonusScale = scale/biggest;
        obj.GetComponent<StarController>().setScale(xScale*bonusScale, yScale*bonusScale);
        obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.SetActive(true);
    }

    public void shiftStars(float shift) 
    {
        List<GameObject> pool = starPool.getPool();
        foreach (GameObject o in pool) 
        {
            if (o.activeInHierarchy)
                o.GetComponent<StarController>().shiftX(shift);
        }
    }

}
