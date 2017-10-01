using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlertLight : MonoBehaviour {

    public new Image light;

    private float timer;
    private float alpha;
    private Color c;
    private bool running;

	// Use this for initialization
	void Start () {
        running = false;
        timer = 0;
        setColor(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (running)
            if (timer < 3.0f)
            {
                timer += Time.deltaTime;
                alpha = -.4f * Mathf.Cos(4.19f * timer) + 0.4f; //6.28 -> 3s || 4.19 -> 2s
                setColor(alpha);
            }
            else
            {
                endAnimation();
            }
        

	}

    private void setColor(float alpha)
    {
        c = new Color(255, 255, 255, alpha);
        light.color = c;
    }

    public void endAnimation()
    {
        running = false;
        setColor(0);
    }

    public void startAnimation()
    {
        running = true;
        timer = 0;
        alpha = 0;
    }
}
