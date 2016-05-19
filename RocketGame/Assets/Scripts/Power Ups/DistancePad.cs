using UnityEngine;
using System.Collections;

public class DistancePad : MonoBehaviour
{
    public PowerUpManager manager;
    private PowerUpInterface power; 
    private int checkLength = 10;
    private int timer;
    // Use this for initialization
    void Start()
    {
        timer = checkLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= checkLength)
            timer++;
        else
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (timer < checkLength && other.gameObject.tag == "Astroids")
        {
            gameObject.SetActive(false);
            power.spawn(manager.leftBound, manager.rightBound, manager.getGenerator().getSpeed());
        }
    }

    public void holdPowerUp(PowerUpInterface power)
    {
        this.power = power;
        timer = 0;
        gameObject.SetActive(true);
    }

    public void setPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
}
