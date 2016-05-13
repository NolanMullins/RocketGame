using UnityEngine;
using System.Collections;

public class PowerUpInterface : MonoBehaviour {

    private Transform holderPosition;
    private bool moveToHolder;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        move();
    }

    private void move()
    {

    }

    public void usePower()
    {

    }

    public void moveGameObjectToHolder(Transform position)
    {
        moveToHolder = true;
    }

    public void reset()
    {
        moveToHolder = false;
    }
}
