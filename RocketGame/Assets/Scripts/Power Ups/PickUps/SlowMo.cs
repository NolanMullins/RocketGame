using UnityEngine;
using System.Collections;

public class SlowMo : PowerUpInterface
{

    private bool active;

    // Use this for initialization
    void Start()
    {
        base.type = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            base.move();
    }

    public override bool usePower()
    {
        //Slow er down
        if (active)
        {
            base.player.slowGameDown();
            gameObject.SetActive(false);
        }
        
        return active;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            base.moveGameObjectToHolder();
            //gameObject.SetActive(false);
        }
    }

    public override void reset()
    {
        base.resetBase();
    }

    public override void stop()
    {
        active = false;
    }
    public override void start()
    {
        active = true;
    }
}
