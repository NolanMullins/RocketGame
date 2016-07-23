using UnityEngine;
using System.Collections;

public class PlayerShell : MonoBehaviour {

    public PlayerController player;
    public GameObject shield;
    public Camera main;
    private float gameWidth;

    private float swapBuffer = 0.1f;
    private float timer;

    // Use this for initialization
    void Start()
    {
        gameWidth = main.ViewportToWorldPoint(new Vector3(1,0)).x*2;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!(player.transform.position.x <= gameWidth / 2.0 && player.transform.position.x >= -gameWidth / 2.0) && timer > swapBuffer)
        {
            swap();
            timer = 0;
        }
        int side = 1;
        //check for player position side
        if (player.transform.position.x >= 0)
        {
            side = -1;
        }

        gameObject.transform.rotation = player.transform.rotation;
        gameObject.transform.position = new Vector3(player.transform.position.x+(gameWidth * side), player.transform.position.y);
    }

    private void swap()
    {
        Vector3 pos = gameObject.transform.position;
        //swap this object to player
        gameObject.transform.position = player.transform.position;
        gameObject.transform.rotation = player.transform.rotation;
        //swap player to old position of shell
        player.transform.position = pos;
    }

    public float getX()
    {
        if (player.transform.position.x <= gameWidth/2.0 && player.transform.position.x >= -gameWidth/2.0)
            return player.transform.position.x;
        return gameObject.transform.position.x;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        for (int a = 0; a < other.contacts.Length; a++)
            if (other.contacts[a].point.x <= gameWidth / 2.0 && other.contacts[a].point.x >= -gameWidth / 2.0)
                player.colide(other, a);
    }

    public GameObject getShield()
    {
        return shield;
    }
}
