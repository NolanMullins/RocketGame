using UnityEngine;
using System.Collections;

public class PowerUpInterface : MonoBehaviour {

    public GameObject holder;
    public PowerUpManager manager;
    public PlayerController player;
    private bool moveToHolder;
    private float speed = 2;

    // Use this for initialization
    void Start() {
        
    }

    protected void init()
    {
        holder = GameObject.Find("PowerUpHolder");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        reset();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void spawn(float leftBound, float rightBound, float speed)
    {
        reset();
        this.speed = speed;
        float x = Random.Range(leftBound, rightBound);
        gameObject.SetActive(true);
        float y = 6;
        gameObject.transform.position = new Vector3(x,y,0);
    }

    protected void move()
    {
        if (!moveToHolder)
            transform.position = new Vector2(transform.position.x, transform.position.y-speed*Time.deltaTime);
        else
        {
            transform.position = new Vector2(-100, -100);//holder.transform.position.x, holder.transform.position.y);
        }
    }

    public virtual void usePower()
    {

    }

    protected void moveGameObjectToHolder()
    {
        if (manager.isOpen())
        {
            moveToHolder = true;
            manager.setPowerUp(this);
        }
    }

    public void reset()
    {
        moveToHolder = false;
    }

}
