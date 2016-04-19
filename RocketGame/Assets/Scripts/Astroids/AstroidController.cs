using UnityEngine;
using System.Collections;

public class AstroidController : MonoBehaviour {

    private GameObject destroyPoint;

    private float xVel;
    private float yVel;
    private float rotation;
    private float rotationRate;

    private float sideVelocity;

    // Use this for initialization
    void Start () {
        destroyPoint = GameObject.Find("DestroyPoint");
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + xVel*Time.deltaTime, transform.position.y - yVel * Time.deltaTime);
        if (transform.position.y < destroyPoint.transform.position.y)
        {
            //destroy
            gameObject.SetActive(false);
        }
    }

    public void setVelocity(float xv, float yv)
    {
        xVel = xv;
        yVel = yv;
    }

}
