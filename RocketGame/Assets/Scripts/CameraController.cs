using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public PlayerController thePlayer;

    private Vector3 lastPlayerPos;
    private float distanceToMove;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPos = thePlayer.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        distanceToMove = thePlayer.transform.position.y - lastPlayerPos.y;

        transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);

        lastPlayerPos = thePlayer.transform.position;
    }
}
