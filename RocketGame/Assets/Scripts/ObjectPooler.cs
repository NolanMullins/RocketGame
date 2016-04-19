using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;

    public int poolAmt;

    private List<GameObject> pool;

	// Use this for initialization
	void Start () {
        pool = new List<GameObject>();
	    for (int a = 0; a < poolAmt; a++)
        {
            GameObject obj = (GameObject)(Instantiate(pooledObject));
            obj.SetActive(false);
            pool.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getPooledObject()
    {
        for (int a = 0; a < pool.Count; a++)
        {
            if (!pool[a].activeInHierarchy)
            {
                return pool[a];
            }
        }
        GameObject obj = (GameObject)(Instantiate(pooledObject));
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
}
