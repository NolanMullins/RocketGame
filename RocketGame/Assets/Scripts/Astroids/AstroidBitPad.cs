using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBitPad : MonoBehaviour {

    //public DistancePad pad;
    public AstroidStorm storm;
    private float checkLength = 0.1f;
    private float timer;

    private GameObject obj;
    // Use this for initialization
    void Start()
    {
        timer = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((timer <= Time.time))
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (timer > Time.time && (other.gameObject.tag == "Astroids" || other.gameObject.tag == "AstroidFrag"))
        {
            obj.SetActive(false);
            gameObject.SetActive(false);            
        }
    }

    public void attemptSpawn(Vector3 pos, GameObject obj)
    {
        this.obj = obj;
        gameObject.transform.position = pos;
        timer = Time.time + checkLength;
        gameObject.SetActive(true);
    }
}
