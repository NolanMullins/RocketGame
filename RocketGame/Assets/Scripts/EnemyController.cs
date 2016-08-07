using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject despawnPos;

    public ObjectPooler laserPool;
    public GameObject laserGunPos;

    public float velocity;

    // Use this for initialization
    void Start () {
	
	}

    public void spawn(Vector3 pos)
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > despawnPos.transform.position.y)
            gameObject.SetActive(false);


	}

    public void shoot()
    {
        GameObject laser = laserPool.getPooledObject();
        laser.transform.position = laserGunPos.transform.position;
        laser.transform.rotation = this.transform.rotation;
        laser.gameObject.SetActive(true);
    }
}
