using UnityEngine;
using System.Collections;

public class LAAZZZOOORRRR : PowerUpInterface {

    private bool active;
    private int clip = 4;
    private int shotCount;

    // Use this for initialization
    void Start () {
        base.type = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
            base.move();
    }

    public override bool usePower()
    {
        //Fire the laser
        if (active)
        {
            shotCount++;
            base.player.immaFirinMALAZOR();
            if (shotCount >= clip)
            {
                gameObject.SetActive(false);
            }
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
        shotCount = 0;
    }

    public override void stop()
    {
        active = false;
    }
    public override void start()
    {
        active = true;
        shotCount = 0;
    }

    public override bool finsihed()
    {
        if (shotCount >= clip)
            return true;
        return false;
    }
}
