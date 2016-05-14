using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public AstroidGenerator astroidGenerator;
    public ObjectPooler laserPool;
    public GameObject laserGunPos;

    private float healthBarY;
    public float timeBetweenShots;
    private float shotTimer;

    private GameObject lastExplosion;

    private Rigidbody2D myBody;

    private bool left;
    private bool right;

    //Power ups
    public PowerUpHolder holder;
    public GameObject shield;
    private bool shoot;
    private bool hasShield;

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        rotationAngle = Mathf.PI * 0.5f;
        explosion.SetActive(false);
        left = false;
        right = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = true;

        //shoot timer
        shotTimer += Time.deltaTime;
        if (shotTimer >= timeBetweenShots)
        {
            shoot = true;
            shotTimer = timeBetweenShots;
        }

        bool flyLeft = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
        bool flyRight = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

        if ((left && !right) || (flyLeft && !flyRight))
        {
            //myBody.transform.Rotate(Vector3.forward * Time.deltaTime * roationRate);
            rotationAngleVelocity += speedInc * (roationRate * Time.deltaTime);
            //hard cap
            if (rotationAngleVelocity > roationRate * Time.deltaTime * rotationCap)
            {
                rotationAngleVelocity = roationRate * Time.deltaTime * rotationCap;
            }
            flag = false;
        } 
        else if ((right && !left) || (flyRight && !flyLeft))
        {
            //myBody.transform.Rotate(Vector3.back * Time.deltaTime * roationRate);
            rotationAngleVelocity += speedInc * (-roationRate * Time.deltaTime);
            //hard cap
            if (rotationAngleVelocity < -roationRate * Time.deltaTime * rotationCap)
            {
                rotationAngleVelocity = -roationRate * Time.deltaTime * rotationCap;
            }
            flag = false;
        }
        else if ((left && right) || (flyLeft && flyRight))
        {
            //Use power
            holder.activatePower();
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
        if ((other.gameObject.tag == "Astroids" || other.gameObject.tag == "Wall") && !hasShield)
        {
            //contact point
            //Collider2D collider = other.collider;
            Vector3 contactPoint = other.contacts[0].point;

            //blow up astroid
            astroidGenerator.blowAstroidUp(other.gameObject, contactPoint);

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = contactPoint;

            Destroy(lastExplosion);
            lastExplosion = (GameObject)Instantiate(explosion, contactPoint, transform.rotation);
            lastExplosion.SetActive(true);
            gameObject.SetActive(false);

            manager.gameOver();
        }
        else if (hasShield && other.gameObject.tag == "Astroids")
        {
            Vector3 contactPoint = other.contacts[0].point;
            //blow up astroid
            astroidGenerator.blowAstroidUp(other.gameObject, contactPoint);

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = contactPoint;
        }
    }

    public void setShieldActive(bool active)
    {
        hasShield = active;
        if (hasShield)
            shield.SetActive(true);
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

    public void pushLeft(bool pressed)
    {
        left = pressed;
    }

    public void pushRight(bool pressed)
    {
        right = pressed;
    }

    public void resetPlayer()
    {
        rotationAngle = Mathf.PI * 0.5f; ;
        rotationAngleVelocity = 0;
        myBody.velocity = Vector3.zero;
        left = false;
        right = false;
        shotTimer = timeBetweenShots;
    }

    private void immaFirinMALAZOR()
    {
        if (shoot)
        {
            GameObject laser = laserPool.getPooledObject();
            laser.transform.position = laserGunPos.transform.position;
            laser.transform.rotation = this.transform.rotation;
            laser.gameObject.SetActive(true);
            shoot = false;
            shotTimer = 0;
        }
    }
}
