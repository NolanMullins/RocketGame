using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject despawnPos;

    public ObjectPooler laserPool;
    public GameObject laserGunPos;

    public float velocity;
    public float rotationRate;

    //private
    private float rotationVelocity;
    private float angle;
    private Rigidbody2D myBody;
    private float DtoR;
    private float timer;

    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        DtoR = Mathf.PI / 180.0f;
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

        myBody.velocity = new Vector2(velocity*Mathf.Sin(angle*DtoR) * -1, velocity*Mathf.Cos(angle*DtoR));
        //this.GetComponent<Rigidbody2D>().AddForce(transform.forward.normalized * forceConst*Time.deltaTime);

	}

    public void shoot()
    {
        GameObject laser = laserPool.getPooledObject();
        laser.transform.position = laserGunPos.transform.position;
        laser.transform.rotation = this.transform.rotation;
        laser.gameObject.SetActive(true);
    }
}
