using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float roationRate;
    public float velocity;

    private float rotationAngle;

    private Rigidbody2D myBody;



	// Use this for initialization
	void Start () {
        myBody = GetComponent<Rigidbody2D>();
        rotationAngle = Mathf.PI*0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        //user Input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //myBody.transform.Rotate(Vector3.forward * Time.deltaTime * roationRate);
            rotationAngle += roationRate * Time.deltaTime;
            if (rotationAngle >= 180)
                rotationAngle = 180;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //myBody.transform.Rotate(Vector3.back * Time.deltaTime * roationRate);
            rotationAngle -= roationRate * Time.deltaTime;
            if (rotationAngle <= 0)
                rotationAngle = 0;
        }

        myBody.velocity = new Vector2(Mathf.Cos(rotationAngle)*velocity, Mathf.Sin(rotationAngle)* velocity);

        Vector2 moveDirection = myBody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg -90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        myBody.velocity = new Vector2(Mathf.Cos(rotationAngle) * velocity, 0);

    }
}
