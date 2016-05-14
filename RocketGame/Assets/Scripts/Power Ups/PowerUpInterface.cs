using UnityEngine;
using System.Collections;

public class PowerUpInterface : MonoBehaviour {

    public GameObject holder;
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

    protected void move()
    {
        if (!moveToHolder)
            transform.position = new Vector2(transform.position.x, transform.position.y-speed*Time.deltaTime);
        else
        {
            transform.position = new Vector2(holder.transform.position.x, holder.transform.position.y);
        }
    }

    public virtual void usePower()
    {

    }

    protected void moveGameObjectToHolder()
    {
        moveToHolder = true;
        holder.GetComponent<PowerUpHolder>().setPowerUp(this);
    }

    public void reset()
    {
        moveToHolder = false;
    }

}
