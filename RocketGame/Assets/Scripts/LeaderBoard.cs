using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LeaderBoard : MonoBehaviour {

    public List<GameObject> ui;
    public GameObject signInUI;
    public GameObject leaderBoardUI;
    public GameObject backGroundUI;

    //user info
    public InputField userName;
    public InputField password;
    public InputField email;

    public InputField signInName;
    public InputField signInPassword;
    //
    public Text headerTxt;

    private bool inLeaderBoard;
    private bool hasAccount;
    private bool isSignedIn;

	// Use this for initialization
	void Start () {
        App42API.Initialize("f9be5d84fe0db7822e423961bb3ad4b5caf2c1c484ad829cd7a8ec35906b685f", "a83342e929a1b40c95b7283ac692a60f74a07db8646c790402b0f8192adb4c20");
        inLeaderBoard = false;
        hasAccount = false;
        isSignedIn = false;
        if (hasAccount)
        {
            UserService userService = App42API.BuildUserService();
            UnityCallBack callBack = new UnityCallBack();
            userService.Authenticate(userName.text, password.text, callBack);
            if (callBack.wasSuccessful())
            {
                isSignedIn = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            back(0);
        }
	}

    //called on Btn press
    public void showLeaderBoard()
    {
        backGroundUI.SetActive(true);
        inLeaderBoard = true;
        editUI(false);
        if (hasAccount)
        {
            //show leader board
            showLeaderBoardUI();
        }
        else
        {
            showSignIn();
        }
    }

    public void showLeaderBoardUI()
    {
        headerTxt.text = "name i rank";
    }

    public void showSignIn()
    {
        headerTxt.text = "Sign Up";
        signInUI.SetActive(true);
    }

    public void back(int todo)
    {
        if (todo == 0)
        {
            closeLeaderBoard();
        }
        else if (todo == 1)
        {
            closeAuthenticationUI();
        }
    }

    public void closeLeaderBoard()
    {
        backGroundUI.SetActive(false);
        inLeaderBoard = false;
        editUI(true);
    }

    public void closeAuthenticationUI()
    {
        if (!isSignedIn)
            closeLeaderBoard();
        //Hide shit
        signInUI.SetActive(false);
        showLeaderBoardUI();
    }

    public void signIn()
    {
        UserService userService = App42API.BuildUserService();
        UnityCallBack callBack = new UnityCallBack();
        if (userName.text == "")
        {
            Debug.Log("Signing in");
            userService.Authenticate(signInName.text, signInPassword.text, callBack);
        }
        else
        {
            Debug.Log("Signing up");
            userService.CreateUser(userName.text, password.text, email.text, callBack);
        }
        
        if (callBack.wasSuccessful())
        {
            isSignedIn = true;
            back(1);
        }
    }



    public class UnityCallBack : App42CallBack
    {
        private bool wasSuc = false;
        public void OnException(Exception ex)
        {
            Debug.Log("Exception: "+ex);
            wasSuc = false;
        }

        public void OnSuccess(object response)
        {
            User user = (User)response;
            Debug.Log("Welcome: " + user.GetUserName());
            wasSuc = true;
        }

        public bool wasSuccessful()
        {
            return wasSuc;
        }
    }

    public void editUI(bool edit)
    {
        for(int a = 0; a < ui.Count; a++)
        {
            ui[a].SetActive(edit);
        }
    }
}
