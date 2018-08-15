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
    //bounds
    public float xBound;

    private float timer;

	// Use this for initialization
	void Start () 
    {
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
            GameObject obj = starPool.getPooledObject();
            obj.transform.position = new Vector3(generationPoint.transform.position.x + Random.Range(-xBound, xBound), y, 0);
            obj.transform.rotation = transform.rotation;
            float scale = Random.Range(smallest, biggest);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.SetActive(true);
            y += yInc;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenStars)
        {
            GameObject obj = starPool.getPooledObject();
            obj.transform.position = new Vector3(generationPoint.transform.position.x + Random.Range(-xBound, xBound), generationPoint.transform.position.y, 0);
            obj.transform.rotation = transform.rotation;
            float scale = Random.Range(smallest, biggest);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.SetActive(true);

            timer = 0;
        }
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
