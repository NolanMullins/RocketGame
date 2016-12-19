using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

    public Transform spawnPoint;
    public float xBound;
    public ObjectPooler[] planets;

    private int index;


    // Use this for initialization
    void Start () {
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnNextPlanet()
    {
        Debug.Log("Spawning planet index: "+index +" length: "+planets.Length);
        if (index >= planets.Length)
            return;
        planets[index].getPooledObject().transform.position = new Vector3(spawnPoint.transform.position.x + Random.Range(-xBound, xBound), spawnPoint.transform.position.y, 0);
        planets[index].getPooledObject().transform.rotation = transform.rotation;
        planets[index].getPooledObject().GetComponent<PlanetController>().shouldMove(true);
        //float scale = Random.Range(smallest, biggest);
        //obj.transform.localScale = new Vector3(scale, scale, scale);
        planets[index].getPooledObject().SetActive(true);
        index++;
    }

    public void clearPlanets()
    {
        GameObject[] pg = GameObject.FindGameObjectsWithTag("Planet");
        for (int a = 0; a < pg.Length; a++)
        {
            pg[a].gameObject.SetActive(false);
        }
    }

    public void reset()
    {
        clearPlanets();
        index = 0;
    }
}
