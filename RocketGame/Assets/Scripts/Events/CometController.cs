using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometController : MonoBehaviour {

    private GameObject destroyPoint;

    private float xVel;
    private float yVel;
    private float rotation;
    private float rotationRate;

    private float sideVelocity;

    private bool move;
    
    private int health;
    public int maxHealth;

    public ParticleSystem[] particleSystems;

    // Use this for initialization
    void Start () {
        destroyPoint = GameObject.Find("DestroyPoint");
    }

    public void spawn(Vector3 pos, float xVel, float yVel) {
        this.transform.position = pos;
        this.yVel = yVel;
        this.xVel = xVel;
        move = true;
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            transform.position = new Vector3(transform.position.x - xVel*Time.deltaTime, transform.position.y - yVel * Time.deltaTime, 1);
            //rotation += rotationRate*Time.deltaTime*100;
            //transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }
		//will probably have to destory it further down
        if (transform.position.y < (destroyPoint.transform.position.y-2))
        {
            //destroy
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Astroids")
            other.gameObject.SetActive(false);
    }

    public void setVelocity(float xv, float yv)
    {
        xVel = xv;
        yVel = yv;
    }

	public void setVelocity(float y) {

	}

    public float getVelocityX()
    {
        return xVel;
    }

    public float getVelocityY()
    {
        return yVel;
    }

    private void toggleParticle(bool move) {
        foreach (ParticleSystem sys in particleSystems) 
        {
            var main = sys.main;
            if (move)
                main.simulationSpeed = 1;
            else 
                main.simulationSpeed = 0;
        }
    }

    public void shouldMove(bool move)
    {
        this.move = move;
        toggleParticle(move);        
    }
}
