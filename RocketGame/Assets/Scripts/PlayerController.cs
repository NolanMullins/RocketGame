using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float roationRate;
    public float velocity;

    private float rotationAngle;
    private float rotationAngleVelocity;

    private Rigidbody2D myBody;



    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        rotationAngle = Mathf.PI * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = true;
        bool left = false;
        bool right = false;
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                left = true;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                right = true;
            }
        }

        //user Input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left)
        {
            //myBody.transform.Rotate(Vector3.forward * Time.deltaTime * roationRate);
            rotationAngleVelocity += 0.05f*(roationRate * Time.deltaTime);
            if (rotationAngleVelocity > roationRate * Time.deltaTime)
            {
                rotationAngleVelocity = roationRate * Time.deltaTime;
            }
            flag = false;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right)
        {
            //myBody.transform.Rotate(Vector3.back * Time.deltaTime * roationRate);
            rotationAngleVelocity += 0.05f*(-roationRate * Time.deltaTime);
            if (rotationAngleVelocity < -roationRate * Time.deltaTime)
            {
                rotationAngleVelocity = -roationRate * Time.deltaTime;
            }
            flag = false;
        }
        if (flag && rotationAngleVelocity != 0)
            rotationAngleVelocity -= 0.04f*Time.deltaTime*(rotationAngleVelocity/Mathf.Abs(rotationAngleVelocity));
            
        rotationAngle += rotationAngleVelocity;
        if (rotationAngle >= 180)
            rotationAngle = 180;
        if (rotationAngle <= 0)
            rotationAngle = 0;

        float xVel = Mathf.Cos(rotationAngle) * velocity;
        myBody.velocity = new Vector2(xVel, Mathf.Sin(rotationAngle) * velocity);

        Vector2 moveDirection = myBody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        myBody.velocity = new Vector2(xVel, 0);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Astroids")
        {
            other.gameObject.SetActive(false);
        }
    }
}
