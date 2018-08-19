using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region [Variables]
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

    private GameObject lastExplosion;

    private Rigidbody2D myBody;

    private bool left;
    private bool right;

    public List<StarGenerator> starGen;

    //Power ups
    public PowerUpManager powerUps;
    public GameObject shield;
    //private bool shoot;
    private bool hasShield;
    
    //slowmo power up
    private bool slowed;
    private int length = 2;
    private float timer;
    public float slowDownBoost;

    //shield power up
    public float shieldLength;
    private float shieldTimer;

    private float gameWidth;
    public PlayerShell shell;
    private bool letGo;
    public Camera main;
    #endregion

    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        rotationAngle = Mathf.PI * 0.5f;
        explosion.SetActive(false);
        left = false;
        right = false;

        gameWidth = main.ViewportToWorldPoint(new Vector3(1, 0)).x * 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        #region [Controls]
        bool flag = true;

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
            letGo = true;
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
            letGo = true;
        }
        else if (((left && right) || (flyLeft && flyRight)))
        {
            //Use power
            if (letGo)
            {
                powerUps.activatePower();
                letGo = false;
            }
        }
        else
        {
            letGo = true;
        }
        #endregion

        if (flag && rotationAngleVelocity != 0)
            rotationAngleVelocity -= friction*Time.deltaTime*(rotationAngleVelocity/Mathf.Abs(rotationAngleVelocity));

        //Experimental course assist
        if (flag && rotationAngle != Mathf.PI*0.5)
        {
            rotationAngle -= (rotationAngle-Mathf.PI*0.5f) * helper * Time.deltaTime;
        }
        

        if (Time.deltaTime > 0)
        {
            float temp = 1;
            if (Time.timeScale > 0 && Time.timeScale < 1)
                temp = slowDownBoost;
            rotationAngle += temp*rotationAngleVelocity * Time.deltaTime * 60;
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

            foreach (StarGenerator gen in starGen)
                gen.shiftStars(xVel*0.00025f);

            myBody.velocity = new Vector2(xVel, 0);
        }

        if (slowed)
        {
            //need to slow after pause
            if (timer < length && Time.timeScale > 0)//redundant
            {
                if (Time.timeScale > 0.65f)
                    Time.timeScale = 0.65f;
                timer += Time.deltaTime;
            }
            else if (Time.timeScale > 0.1)
            {
                Time.timeScale = 1;
                slowed = false;
            }
        }

        if (hasShield)
        {
            shieldTimer += Time.deltaTime;
            if (shieldTimer >= shieldLength)
            {
                setShieldActive(false);
            }
        }

    }

    #region [Collision]
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].point.x <= gameWidth / 2.0 && other.contacts[0].point.x >= -gameWidth / 2.0)
            colide(other, 0);
    }

    /* public void getHit(Collision2D other)
     {
         if (other.contacts[0].point.x <= gameWidth / 2.0 && other.contacts[0].point.x >= -gameWidth / 2.0)
             colide(other, 0);
     }*/

    //used by enemy laser to destory the player
    public void getHit(Collider2D other)
    {
        if (!hasShield)
        {
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

    public void colide(Collision2D other, int point)
    {
        if ((other.gameObject.tag == "Astroids" || other.gameObject.tag == "Wall" || other.gameObject.tag == "enemyLaser" || other.gameObject.tag == "Comet") && !hasShield)
        {
            //contact point
            //Collider2D collider = other.collider;
            Vector3 contactPoint = other.contacts[point].point;

            //blow up astroid
            if (other.gameObject.tag == "Astroids")
                astroidGenerator.blowAstroidUp(other.gameObject, contactPoint);
            else if (other.gameObject.tag == "Comet")
                astroidGenerator.blowCometUp(other.gameObject, contactPoint);

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = contactPoint;

            Destroy(lastExplosion);
            lastExplosion = (GameObject)Instantiate(explosion, contactPoint, transform.rotation);
            lastExplosion.SetActive(true);
            if (gameObject.tag != "comet")
                gameObject.SetActive(false);
            shell.gameObject.SetActive(false);
            manager.gameOver();
        }
        else if (hasShield && (other.gameObject.tag == "Astroids" || other.gameObject.tag == "Comet"))
        {
            Vector3 contactPoint = other.contacts[0].point;

            //blow up astroid
            if (other.gameObject.tag == "Astroids")
                astroidGenerator.blowAstroidUp(other.gameObject, contactPoint);
            else if (other.gameObject.tag == "Comet")
            {
                setShieldActive(false);
                astroidGenerator.blowCometUp(other.gameObject, contactPoint);
            }

            if (explosionSound.isPlaying)
                explosionSound.Stop();
            explosionSound.Play();

            explosion.transform.position = contactPoint;

            Destroy(lastExplosion);
            lastExplosion = (GameObject)Instantiate(explosion, contactPoint, transform.rotation);
            lastExplosion.SetActive(true);
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
    #endregion

    public void setShieldActive(bool active)
    {
        Debug.Log(active);
        if (active)
        {
            shield.SetActive(true);
            shell.getShield().SetActive(true);
            shieldTimer = 0;
        }
        else
        {
            shield.SetActive(false);
            shell.getShield().SetActive(false);
        }

        hasShield = active;
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
        shield.SetActive(false);
        shell.getShield().SetActive(false);
        hasShield = false;
        slowed = false;
        letGo = true;
    }

    public void immaFirinMALAZOR()
    {
            GameObject laser = laserPool.getPooledObject();
            laser.transform.position = laserGunPos.transform.position;
            laser.transform.rotation = this.transform.rotation;
            laser.gameObject.SetActive(true);
    }

    public void slowGameDown()
    {
        Time.timeScale = 0.65f;
        timer = 0;
        slowed = true;
    }

    public float getGameWidth()
    {
        return gameWidth;
    }
}
