using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

    public Transform spawnPoint;
    public float xBound;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawnPlanet(int a, GameObject obj)
    {
        obj.transform.position = new Vector3(spawnPoint.transform.position.x + Random.Range(-xBound, xBound), spawnPoint.transform.position.y, 0);
        obj.transform.rotation = transform.rotation;
        obj.GetComponent<PlanetController>().shouldMove(true);
        //float scale = Random.Range(smallest, biggest);
        //obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.SetActive(true);
    }

    public void clearPlanets()
    {
        GameObject[] pg = GameObject.FindGameObjectsWithTag("Planet");
        for (int a = 0; a < pg.Length; a++)
        {
            pg[a].gameObject.SetActive(false);
        }
    }
}
