using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {

    private GameObject destroyPoint;
    public float velocity;

	// Use this for initialization
	void Start () {
	    destroyPoint = GameObject.Find("DestroyPoint");
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y - velocity * Time.deltaTime);
        if (transform.position.y < destroyPoint.transform.position.y)
        {
            //destroy
            gameObject.SetActive(false);
        }
	}
}
