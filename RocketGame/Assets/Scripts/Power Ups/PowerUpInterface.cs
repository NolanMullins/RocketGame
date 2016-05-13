using UnityEngine;
using System.Collections;

public class PowerUpInterface : MonoBehaviour {

    public Transform holderPosition;
    private bool moveToHolder;
    private float speed = 2;

    // Use this for initialization
    void Start() {
        
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

        }
    }

    public virtual void usePower()
    {

    }

    protected void moveGameObjectToHolder()
    {
        moveToHolder = true;
    }

    public void reset()
    {
        moveToHolder = false;
    }

}
