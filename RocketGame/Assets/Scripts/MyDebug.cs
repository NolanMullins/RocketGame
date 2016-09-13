using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyDebug : MonoBehaviour {

    public Text logger;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        logger.text = Mathf.Round(1.0f / Time.smoothDeltaTime) + "";
    }
}
