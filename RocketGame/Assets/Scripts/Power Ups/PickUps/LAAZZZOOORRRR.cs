using UnityEngine;
using System.Collections;

public class LAAZZZOOORRRR : PowerUpInterface {

    private bool active;
    private int clip = 4;
    private int shotCount;
    private float gameWidth;

    // Use this for initialization
    void Start () {
        base.type = 0;
        gameWidth = player.getGameWidth();
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
            base.move();
        if (gameObject.transform.position.x > gameWidth/2.0)
        {
            gameObject.transform.position = new Vector3(-gameWidth/2.0f, gameObject.transform.position.y);
        }
        else if (gameObject.transform.position.x < -gameWidth / 2.0)
        {
            gameObject.transform.position = new Vector3(gameWidth/2.0f, gameObject.transform.position.y);
        }
    }

    public override bool usePower()
    {
        //Fire the laser
        if (active)
        {
            if (shotCount == 0)
                infoTxt.gameObject.SetActive(true);
            shotCount++;
            base.infoTxt.text = "" + (clip-shotCount);
            base.player.immaFirinMALAZOR();
            if (shotCount >= clip)
            {
                gameObject.SetActive(false);
                base.infoTxt.text = "";
            }
        }
        return active;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            base.moveGameObjectToHolder();
            //base.infoTxt.text = "" + clip;
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
    }

    public override bool finsihed()
    {
        if (shotCount >= clip)
            return true;
        return false;
    }
}
