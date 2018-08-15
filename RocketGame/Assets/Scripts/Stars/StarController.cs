using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {

    private GameObject destroyPoint;
    public float velocity;
    private bool move;

	// Use this for initialization
	void Start () {
	    destroyPoint = GameObject.Find("DestroyPoint");
        move = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (move)
            transform.position = new Vector3(transform.position.x, transform.position.y - velocity * Time.deltaTime);
        if (transform.position.y < destroyPoint.transform.position.y)
        {
            //destroy
            gameObject.SetActive(false);
        }
	}

    public void shouldMove(bool move)
    {
        this.move = move;
    }

    public void shiftX(float shift) {
        transform.position = new Vector3(transform.position.x+shift, transform.position.y);
    }
}
