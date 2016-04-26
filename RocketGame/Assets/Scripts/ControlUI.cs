using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour {

    public Image left;
    public Image right;
    public float blinkSpeed;
    public float length;
    private float startAlpha;
    private float alpha;
    private bool run;
    private float timer;


	// Use this for initialization
	void Start () {
        startAlpha = left.color.a;
        run = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (run && timer <= length)
        {
            alpha = startAlpha * /*(1f/2f*length)*/0.25f * (Mathf.Cos(blinkSpeed * timer) + 1) * (-Mathf.Pow(timer, 2) + length);// +startAlpha*0.5f;
            Color c = new Color(left.color.r, left.color.g, left.color.b, alpha);
            left.color = c;
            right.color = c;
            timer += Time.deltaTime;
            if (timer > length)
            {
                hideUI();
            }
        }
	}

    public void hideUI()
    {
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    public void showUI()
    {
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
    }

    public void startAnimation()
    {
        run = true;
        timer = 0;
        alpha = startAlpha;
        showUI();
    }

    public void stopAnimation()
    {
        run = false;
        hideUI();
    }
}
