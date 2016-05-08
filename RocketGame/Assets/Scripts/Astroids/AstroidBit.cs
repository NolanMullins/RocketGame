using UnityEngine;
using System.Collections;

public class AstroidBit : MonoBehaviour {

    private float xVel;
    private float yVel;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + xVel * Time.deltaTime, transform.position.y + yVel * Time.deltaTime);
    }

    public void setDirection(Vector2 dir)
    {
        xVel = dir.x;
        yVel = dir.y;
    }
}
