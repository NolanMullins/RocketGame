using UnityEngine;
using System.Collections;

public class PowerUpInterface : MonoBehaviour {

    public GameObject holder;
    public PowerUpManager manager;
    public PlayerController player;
    public Transform destoryPoint;
    private bool moveToHolder;
    private float speed = 2;
    protected int type;

    // Use this for initialization
    void Start() {
        
    }

    protected void init()
    {
        holder = GameObject.Find("PowerUpHolder");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        resetBase();
    }

    public void spawn(float leftBound, float rightBound, float speed)
    {
        resetBase();
        this.speed = speed;
        gameObject.SetActive(true);
        gameObject.transform.position = getPoint(leftBound, rightBound);

        manager.getDistPad().setPosition(gameObject.transform.position);
        manager.getDistPad().holdPowerUp(this);
    }


    private Vector3 getPoint(float leftBound, float rightBound)
    {
        float x = Random.Range(leftBound, rightBound);
        float y = 6;
        return new Vector3(x, y, 0);
    }

    protected void move()
    {
        if (!moveToHolder)
            transform.position = new Vector2(transform.position.x, transform.position.y-speed*Time.deltaTime);

        if (destoryPoint.position.y > gameObject.transform.position.y)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual bool usePower()
    {
        return false;
    }

    
    protected void moveGameObjectToHolder()
    {
        if (manager.isOpen())
        {
            moveToHolder = true;
            manager.setPowerUp(this, type);
            transform.position = new Vector2(100, 100);
        }
    }

    public void resetBase()
    {
        moveToHolder = false;
    }

    public virtual void reset()
    {
        
    }

    public virtual void stop()
    {

    }
    public virtual void start()
    {

    }

}
