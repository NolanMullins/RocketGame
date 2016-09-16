using UnityEngine;
using System.Collections;

public class Shield : PowerUpInterface
{
    private bool isShieldActive;
    private bool active;
    public float shieldLength;
    private float shieldTimer;

    // Use this for initialization
    void Start()
    {
        base.type = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            base.move();

            //Shield
            if (isShieldActive)
            {
                //Update User
                base.infoTxt.text = Mathf.Round(shieldLength - shieldTimer) + "";
                shieldTimer += Time.deltaTime;
                if (shieldTimer >= shieldLength)
                {
                    //base.player.setShieldActive(false);
                    gameObject.SetActive(false);
                    isShieldActive = false;
                    base.infoTxt.text = "";
                }
            }
        }
    }

    public override bool usePower()
    {
        //Use shield
        if (active)
        {
            base.player.setShieldActive(true);
            isShieldActive = true;
            shieldTimer = 0;
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
        isShieldActive = false;
        shieldTimer = 0;
    }
    public override void start()
    {
        active = true;
        isShieldActive = false;
        shieldTimer = 0;
    }
    public override void stop()
    {
        active = false;
    }
}
