using UnityEngine;
using System.Collections;

public class SlowMo : PowerUpInterface
{

    private bool active;
    private bool slowed;

    private float timer;
    private float length = 2;

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

        if (slowed)
        {
            timer += Time.deltaTime;
            if (timer > length)
            {
                //despawn
                gameObject.SetActive(false);
                slowed = false;
                base.infoTxt.text = "";
            }
            else
            {
                base.infoTxt.text = ""+Mathf.Round(length*1/*.5385f*/-timer);
            }
        }
    }

    public override bool usePower()
    {
        //Slow er down
        if (active)
        {
            base.player.slowGameDown();
            infoTxt.gameObject.SetActive(true);
            slowed = true;
            timer = 0;
            //gameObject.SetActive(false);
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
    public override void resume()
    {
        active = true;
    }
}
