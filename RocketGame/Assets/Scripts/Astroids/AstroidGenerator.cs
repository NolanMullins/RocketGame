using UnityEngine;
using System.Collections;

public class AstroidGenerator : MonoBehaviour {

    public float timeBetweenAstroids;
    public ObjectPooler objPool;
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
    public float velocity;
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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAstroids)
        {
            GameObject obj = objPool.getPooledObject();
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

            if ((int)Random.Range(0, chanceOfBigOne)==0)
            {
                spawnBig(obj);
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

    private void spawnBig(GameObject obj)
    {
        float scale = Random.Range(bSmall, bBig);
        obj.transform.localScale = new Vector3(scale, scale, scale);

    } 
}
