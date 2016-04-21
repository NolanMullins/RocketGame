using UnityEngine;
using System.Collections;

public class AstroidGenerator : MonoBehaviour {

    public float startTimeBetweenAstroids;
    public float minTimeBetween;
    public float speedUpPerSecond;
    private float timeBetween;
    public ObjectPooler[] objPools;
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

    private float timer;
    private int throwAtPlayer;
    private int chance;
    public GameObject player;

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

        velocity += velocityIncPerSecond * Time.deltaTime;
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
            obj.transform.position = new Vector3(generationPoint.transform.position.x + shift, generationPoint.transform.position.y, 0);
            obj.transform.rotation = transform.rotation;
            obj.transform.Rotate(Vector3.forward * Random.Range(0, 360));

            //float yVel = Random.Range(sMinVel, sMaxVel); ;
            AstroidController ac = obj.GetComponent<AstroidController>();
            ac.setVelocity(0, velocity);
            ac.shouldMove(true);

            if ((int)Random.Range(0, chanceOfBigOne)==0)
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

    private void spawnSmall(GameObject obj)
    {
        float scale = Random.Range(sSmall, sBig);
        obj.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void spawnBig(GameObject obj, int num)
    {
        float scale = Random.Range(bSmall, bBig);
        //fuck that wide one, seriously
        if (num == 2)
            scale *= 0.8f;
        obj.transform.localScale = new Vector3(scale, scale, scale);

    } 

    public void reset()
    {
        velocity = startVelocity;
        timeBetween = startTimeBetweenAstroids;
    }
}
