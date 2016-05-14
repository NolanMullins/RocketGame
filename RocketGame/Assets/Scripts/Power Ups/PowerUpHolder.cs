using UnityEngine;
using System.Collections;

public class PowerUpHolder : MonoBehaviour {

    private PowerUpInterface powerUp;
    private bool used;

    // Use this for initialization
    void Start () {
        used = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPowerUp(PowerUpInterface power)
    {
        powerUp = power;
        used = false;
    }

    public void activatePower()
    {
        if (!used)
        {
            powerUp.usePower();
            used = true;
        }
        
    }
}
