using UnityEngine;
using System.Collections;

public class FogGenerator : MonoBehaviour {

    public float timeToSpawn;
    public float timeBounds;

    private float timeBetween;

    public ObjectPooler cubes;
    public ObjectPooler quads;

    public GameObject generationPoint;
    public GameObject destroyPoint;

    //scale
    public float smallest;
    public float biggest;
    //bounds
    public float xBound;

    private float timer;

    // Use this for initialization
    void Start()
    {
        timeBetween = timeToSpawn + Random.Range(-timeBounds, timeBounds);        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetween)
        {
            spawn();
        }
    }

    public void startGame()
    {
        spawn();
    }


    private void spawn()
    {
        GameObject obj;
        if ((int)Random.Range(0, 2) == 0)
        {
            obj = cubes.getPooledObject();
            Debug.Log("0");
        }
        else
        {
            Debug.Log("1");
            obj = quads.getPooledObject();
        }

        obj.transform.position = new Vector3(generationPoint.transform.position.x + Random.Range(-xBound, xBound), generationPoint.transform.position.y, 0);
        obj.transform.rotation = transform.rotation;
        obj.transform.Rotate(Vector3.forward * Random.Range(0, 360));

        float scale = Random.Range(smallest, biggest);
        obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.SetActive(true);

        timer = 0;
        timeBetween = timeToSpawn + Random.Range(-timeBounds, timeBounds);
    }

    public void reset()
    {
        timer = 0;
    }
}
