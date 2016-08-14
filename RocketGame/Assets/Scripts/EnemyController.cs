using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject despawnPos;

    public ObjectPooler laserPool;
    public GameObject laserGunPos;

    public float velocity;
    public float rotationRate;

    public Rigidbody2D myBody;

    public Camera main;

    //explosion
    public AudioSource explosionSound;
    public GameObject explosion;
    private GameObject lastExplosion;

    //private
    private float rotationVelocity;
    private float angle;
    private float DtoR;
    private float timer;
    private float gameWidth;

    //shooting mechanics
    private float delay = .5f;
    private float randomMaxAdd = .25f;
    private int maxShots = 2;
    private int numShots;
    private float timeBetweenShots = .3f;
    private float shotTime;
    private float shotTimer;

    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        DtoR = Mathf.PI / 180.0f;
        gameWidth = main.ViewportToWorldPoint(new Vector3(1, 0)).x * 2;
    }

    public void spawn(Vector3 pos)
    {
        transform.position = pos;
        if (transform.position.x < 0)
        {
            rotationVelocity = 1f;
        }
        else
        {
            rotationVelocity = -1f;
        }
        angle = 180 + 10 * rotationVelocity;
        myBody.velocity = new Vector2();
        transform.rotation = new Quaternion(0,0,angle,0);
        timer = 0;
        shotTime = Random.Range(delay, delay + randomMaxAdd);
        shotTimer = 0;
        numShots = 0;
        //transform.Rotate(Vector3.down);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < despawnPos.transform.position.y)
            gameObject.SetActive(false);

        timer += Time.deltaTime;
        if(timer > 4)
            gameObject.SetActive(false);

        //transform.Rotate(Vector3.right * Time.deltaTime);
        angle += rotationRate * Time.deltaTime * rotationVelocity;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        myBody.velocity = new Vector2(velocity*Mathf.Sin(angle*DtoR) * -1 * (Time.deltaTime * 65), velocity*Mathf.Cos(angle*DtoR)*(Time.deltaTime * 65));

        if (Mathf.Abs(transform.position.x) <= gameWidth)
        {
            //run shooting code
            shotTimer += Time.deltaTime;
            if (shotTimer > shotTime && numShots < maxShots)
            {
                shoot();
                numShots++;
                shotTimer = 0;
                shotTime = Random.Range(timeBetweenShots, timeBetweenShots+ randomMaxAdd);
            }
        }

        //this.GetComponent<Rigidbody2D>().AddForce(transform.forward.normalized * forceConst*Time.deltaTime);

	}

    public void shoot()
    {
        GameObject laser = laserPool.getPooledObject();
        laser.transform.position = laserGunPos.transform.position;
        laser.transform.rotation = this.transform.rotation;
        laser.gameObject.SetActive(true);
    }

    public void destroy()
    {
        gameObject.SetActive(false);
        if (explosionSound.isPlaying)
            explosionSound.Stop();
        explosionSound.Play();

        explosion.transform.position = transform.position;

        Destroy(lastExplosion);
        lastExplosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        lastExplosion.SetActive(true);
        gameObject.SetActive(false);
    }
}
