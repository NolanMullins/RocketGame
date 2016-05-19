using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AstroidGenerator : MonoBehaviour {

    public float startTimeBetweenAstroids;
    public float minTimeBetween;
    public float speedUpPerSecond;
    private float timeBetween;
    public ObjectPooler[] objPools;
    public ObjectPooler bitPool;
    public GameObject generationPoint;
    //scale Small
    public float sSmall;
    public float sBig;
    //scale Big
    public float bSmall;
    public float bBig;
    //Chances
    public int chanceOfBigOne;
    //Velocity
    public float startVelocity;
    public float velocityCap;
    public float velocityIncPerSecond;
    private float velocity;
    //bounds
    public float xBound;
    public float minSpace;
    private float lastX;

    private float timer;
    private int throwAtPlayer;
    private int chance;
    public GameObject player;

    public float explosionForce;
    public float explosionSpread;

    //explosions
    public GameObject explosion;
    private GameObject lastExplosion;
    public AudioSource explosionSound;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        throwAtPlayer = 0;
        chance = (int)Random.Range(1, 3);
        velocity = startVelocity;
        timeBetween = startTimeBetweenAstroids;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetween -= speedUpPerSecond * Time.deltaTime;
        if (timeBetween < minTimeBetween)
            timeBetween = minTimeBetween;

        velocity += (velocityIncPerSecond * Time.deltaTime)/(velocity*0.4f);
        if (velocity > velocityCap)
            velocity = velocityCap;
        timer += Time.deltaTime;
        if (timer > timeBetween)
        {
            int num = Random.Range(0, objPools.Length);
            GameObject obj = objPools[num].getPooledObject();
            float shift;
            
            if (throwAtPlayer >= chance)
            {
                throwAtPlayer = 0;
                shift = generationPoint.transform.position.x + player.transform.position.x;
                chance = (int)Random.Range(1, 1);
            }
            else
            {
                throwAtPlayer++;
                shift = Random.Range(-xBound, xBound);
            }

            //dont allow them to stack
            if (Mathf.Abs(lastX - shift) < minSpace)
            {
                shift = getNewShift();
            }


            //keep track of last X
            lastX = shift;

            obj.transform.position = new Vector3(generationPoint.transform.position.x + shift, generationPoint.transform.position.y, 0);
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(Vector3.forward * Random.Range(0, 360));

            //float yVel = Random.Range(sMinVel, sMaxVel); ;
            AstroidController ac = obj.GetComponent<AstroidController>();
            ac.setVelocity(0, velocity);
            ac.shouldMove(true);

            //big ones disabled
            if ((int)Random.Range(0, chanceOfBigOne)==-1)
            {
                spawnBig(obj, num);
            }
            else
            {
                spawnSmall(obj);
            }

            obj.SetActive(true);

            timer = 0;
        }
    }

    private float getNewShift()
    {
        float shift = Random.Range(-xBound, xBound);
        if (Mathf.Abs(lastX - shift) < minSpace)
        {
            return getNewShift();
        }
        return shift;
    }

    private void spawnSmall(GameObject obj)
    {
        float scale = Random.Range(sSmall, sBig);
        obj.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void spawnBig(GameObject obj, int num)
    {
        float scale = Random.Range(bSmall, bBig);
        obj.transform.localScale = new Vector3(scale, scale, scale);

    } 

    public void reset()
    {
        velocity = startVelocity;
        timeBetween = startTimeBetweenAstroids;
    }

    public void blowAstroidUp(GameObject astroid, Vector3 pointOfContact)
    {
        List<GameObject> bits = new List<GameObject>();
        int listSize = Random.Range(4, 6);

        float x = astroid.transform.position.x;
        float y = astroid.transform.position.y;

        float theta = solveForTheta(astroid, pointOfContact);

        for (int a = 0; a < listSize; a++)
        {
            bits.Add(bitPool.getPooledObject());

            //set position
            bits[a].transform.position = new Vector3(x, y, 0);
            bits[a].SetActive(true);
            float newTheta = Random.Range(-explosionSpread, explosionSpread) + theta;

            Vector2 force;
            force = new Vector2(Mathf.Cos(newTheta) * explosionForce, Mathf.Sin(newTheta) * explosionForce);
            //bits[a].GetComponent<Rigidbody2D>().AddForce(force);
            bits[a].GetComponent<AstroidBit>().setDirection(force);
            astroid.SetActive(false);
        }
    }

    private float solveForTheta(GameObject astroid, Vector3 pointOfContact)
    {
        float x = astroid.transform.position.x;
        float y = astroid.transform.position.y;
        ////inverse slope
        float rise = y - pointOfContact.y;
        float run = x - pointOfContact.x;
        float hyp = Mathf.Sqrt(Mathf.Pow(rise, 2) + Mathf.Pow(run, 2));

        //get theta
        
        float theta = 1;
        //Debug.Log("Rise: " + rise + " Run: " + run);
        //Debug.Log("Ax: " + x + " Ay: " + y);
        //Debug.Log("Cx: " + pointOfContact.x + " Cy: " + pointOfContact.y);
        //Q1
        if (rise >= 0 && run >= 0)
        {
            theta = Mathf.Asin(rise / hyp);
        }
        //Q2
        else if (rise >= 0 && run <= 0)
        {
            theta = Mathf.PI - Mathf.Asin(rise / hyp);
        }
        //Q3
        else if (rise <= 0 && run <= 0)
        {

        }
        //Q4
        else
        {

        }

        return theta;
    }

    public void createExplosion(Transform point)
    {
        if (explosionSound.isPlaying)
            explosionSound.Stop();
        explosionSound.Play();

        explosion.transform.position = point.position;

        Destroy(lastExplosion);
        lastExplosion = (GameObject)Instantiate(explosion, point.position, transform.rotation);
        lastExplosion.SetActive(true);
    }

    public float getSpeed()
    {
        return velocity;
    }

    /*public float getDistToClosest(Vector3 position)
    {
        float dist = 1000;
        for (int a = 0; a < objPools.Length; a++)
        {
            List<GameObject> pool = objPools[a].getPool();
            for (int b = 0; b < pool.Count; b++)
            {
                if (pool[b].gameObject.activeInHierarchy)
                {
                    float temp = getDist(position, pool[a].transform.position);
                    if (temp < dist)
                        dist = temp;
                }
            }
        }

        return dist;
    }*/

    private float getDist(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }

}
