using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public AudioSource explosionSound;

    public float roationRate;
    public float velocity;

    private float rotationAngle;
    private float rotationAngleVelocity;
    public float rotationCap;
    public float speedInc;
    public float friction;
    public float helper;
    public GameObject explosion;
    //public ParticleSystem part1;
    //public ParticleSystem part2;

    public GameManager manager;

    private GameObject lastExplosion;

    private Rigidbody2D myBody;

    //debug
    private float speedCap;
    private float frameDelta;
    private bool checkVal;


    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        rotationAngle = Mathf.PI * 0.5f;
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = true;
        bool left = false;
        bool right = false ;
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
        speedCap = roationRate * Time.deltaTime * rotationCap;
        frameDelta = Time.deltaTime;

        //user Input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left)
        {
            //myBody.transform.Rotate(Vector3.forward * Time.deltaTime * roationRate);
            rotationAngleVelocity += speedInc*(roationRate * Time.deltaTime);
            //hard cap
            if (rotationAngleVelocity > roationRate * Time.deltaTime * rotationCap)
            {
                rotationAngleVelocity = roationRate * Time.deltaTime * rotationCap;
            }
            flag = false;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right)
        {
            //myBody.transform.Rotate(Vector3.back * Time.deltaTime * roationRate);
            rotationAngleVelocity += speedInc*(-roationRate * Time.deltaTime);
            //hard cap
            if (rotationAngleVelocity < -roationRate * Time.deltaTime * rotationCap)
            {
                rotationAngleVelocity = -roationRate * Time.deltaTime * rotationCap;
            }
            flag = false;
        }

        if (flag && rotationAngleVelocity != 0)
            rotationAngleVelocity -= friction*Time.deltaTime*(rotationAngleVelocity/Mathf.Abs(rotationAngleVelocity));

        //Experimental course assist
        if (flag && rotationAngle != Mathf.PI*0.5)
        {
            rotationAngle -= (rotationAngle-Mathf.PI*0.5f) * helper * Time.deltaTime;
        }
        

        if (Time.deltaTime > 0)
        {
            rotationAngle += rotationAngleVelocity * Time.deltaTime * 60;
            if (rotationAngle >= Mathf.PI)
                rotationAngle = Mathf.PI;
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Astroids" || other.gameObject.tag == "Wall")
        {
            //other.gameObject.SetActive(false);

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = transform.position;

            Destroy(lastExplosion);
            lastExplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            lastExplosion.SetActive(true);
            gameObject.SetActive(false);
            
            manager.gameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Astroids" || other.gameObject.tag == "Wall")
        {
            //other.gameObject.SetActive(false);

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = transform.position;

            Destroy(lastExplosion);
            lastExplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            lastExplosion.SetActive(true);
            gameObject.SetActive(false);

            manager.gameOver();
        }
    }

    public void resetPlayer()
    {
        rotationAngle = Mathf.PI * 0.5f; ;
        rotationAngleVelocity = 0;
        myBody.velocity = Vector3.zero;
    }

}
