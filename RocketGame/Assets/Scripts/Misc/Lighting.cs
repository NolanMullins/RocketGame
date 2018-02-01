using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {

    public GameObject leftLight;
    public GameObject rightLight;
    public GameObject middleLight;

    public Camera main;

    //private float alpha;
    private float gameWidth;

    // Use this for initialization
    void Start () {
        gameWidth = main.ViewportToWorldPoint(new Vector3(1, 0)).x * 2;
        //alpha = 1;
    }
	
	// Update is called once per frame
	void Update () {
        leftLight.transform.rotation = middleLight.transform.rotation;
        rightLight.transform.rotation = middleLight.transform.rotation;
        leftLight.transform.position = new Vector3(middleLight.transform.position.x - (gameWidth), middleLight.transform.position.y);
        rightLight.transform.position = new Vector3(middleLight.transform.position.x + (gameWidth), middleLight.transform.position.y);
    }
}
