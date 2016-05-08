using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnChanger : MonoBehaviour {

    public Sprite[] imgs;
    private int num;
    private bool mute;
    private Button btn;

    // Use this for initialization
    void Start () {
        num = 0;
        
        if (PlayerPrefs.HasKey("mute"))
        {
            int temp = PlayerPrefs.GetInt("mute");
            if (temp == 1)
            {
                mute = true;
                num = 1;
                AudioListener.volume = 0;
            }
            else
            {
                mute = false;
            }
        }

        btn = GetComponent<Button>();
        btn.image.overrideSprite = imgs[num];
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void onClick()
    {
        num++;
        if (num >= imgs.Length)
        {
            num = 0;
        }
        btn.image.overrideSprite = imgs[num];
        handleSound();
    }

    private void handleSound()
    {
        if (!mute)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
        mute = !mute;
        if (mute)
            PlayerPrefs.SetInt("mute", 1);
        else
            PlayerPrefs.SetInt("mute", 0);
    }
}
