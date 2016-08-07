using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {

    public AudioSource click;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        if (click.isPlaying)
            click.Stop();
        click.Play();
    }
}
