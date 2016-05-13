using UnityEngine;
using System.Collections;

public class Shield : PowerUpInterface
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.move();
    }


    public override void usePower()
    {
        //Use shield

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Shield hit player");
            base.moveGameObjectToHolder();
        }
    }
}
