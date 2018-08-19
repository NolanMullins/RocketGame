using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {

    private GameObject destroyPoint;
    public float velocity;
    private bool move;

    private float xScale=1, yScale=1;

    private float xBound;

	// Use this for initialization
	void Start () {
	    destroyPoint = GameObject.Find("DestroyPoint");
        move = true;
    }

    public void setScale(float xScale, float yScale) {
        this.xScale = xScale;
        this.yScale = yScale;
    }

    public void setXBound(float xBound) {
        this.xBound = xBound;
    }
	
	// Update is called once per frame
	void Update () {
        if (move)
            transform.position = new Vector3(transform.position.x, transform.position.y - yScale*velocity * Time.deltaTime);
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
        if (Mathf.Abs(transform.position.x+shift*xScale) > xBound)
            gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x+shift*xScale, transform.position.y);
    }
}
