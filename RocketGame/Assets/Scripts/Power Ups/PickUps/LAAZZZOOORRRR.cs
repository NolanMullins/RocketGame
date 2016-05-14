using UnityEngine;
using System.Collections;

public class LAAZZZOOORRRR : PowerUpInterface {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        base.move();
    }

    public override void usePower()
    {
        //Fire the laser
        base.player.immaFirinMALAZOR();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            base.moveGameObjectToHolder();
            //gameObject.SetActive(false);
        }
    }
}
