using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.game;

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
    public Text scoreBoard;
    public Text scoreBoardScores;
    public Text rank;

    //buttons
    //public Button refresh;

    //private bool inLeaderBoard;
    private bool hasAccount;
    private bool isSignedIn;

    private List<UnityCallBack> callBackWait;
    private GlobalScoreCallBack globalWait;
    private String user;
    private ScoreBoardService scoreBoardService;
    private int highScore;

    // Use this for initialization
    void Start() {
        App42API.Initialize("f9be5d84fe0db7822e423961bb3ad4b5caf2c1c484ad829cd7a8ec35906b685f", "a83342e929a1b40c95b7283ac692a60f74a07db8646c790402b0f8192adb4c20");
        scoreBoardService = new ScoreBoardService("f9be5d84fe0db7822e423961bb3ad4b5caf2c1c484ad829cd7a8ec35906b685f", "a83342e929a1b40c95b7283ac692a60f74a07db8646c790402b0f8192adb4c20");
        //inLeaderBoard = false;
        //hasAccount = PlayerPrefs.HasKey("Name");
        isSignedIn = false;

        //INIT
        callBackWait = new List<UnityCallBack>();
        globalWait = new GlobalScoreCallBack();
        globalWait.calledBack = true;
        refreshHighScoreBoard();
        if (hasAccount)
        {
            UserService userService = App42API.BuildUserService();
            UnityCallBack callBack = new UnityCallBack();
            userService.Authenticate(PlayerPrefs.GetString("Name"), PlayerPrefs.GetString("Pass"), callBack);
            callBack.name = PlayerPrefs.GetString("Name");
            callBack.pass = PlayerPrefs.GetString("Pass");
            callBackStack(callBack);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            back(0);
        }

        checkForCallBacks();
        checkForScoreCallBack();
    }

    //called on Btn press
    public void showLeaderBoard()
    {
        backGroundUI.SetActive(true);
        //inLeaderBoard = true;
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
        headerTxt.text = user;
        leaderBoardUI.SetActive(true);
    }

    public void showSignIn()
    {
        headerTxt.text = "Account";
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
        leaderBoardUI.SetActive(false);
        //inLeaderBoard = false;
        editUI(true);
    }

    public void closeAuthenticationUI()
    {
        if (!isSignedIn)
        {
            closeLeaderBoard();
            
        }
        else
        {
            showLeaderBoardUI();
        }
            
        //Hide shit
        signInUI.SetActive(false);
        
    }

    public void callBackStack(UnityCallBack callBack)
    {
        callBackWait.Add(callBack);
    }

    public void checkForCallBacks()
    {
        for (int a = 0; a < callBackWait.Count; a++)
        {
            if (callBackWait[a].wasSuccessful() && callBackWait[a].calledBack)
            {
                isSignedIn = true;
                back(1);
                user = callBackWait[a].name;
                hasAccount = true;
                saveUser(callBackWait[a].name, callBackWait[a].pass);
                callBackWait.Remove(callBackWait[a]);
                //submitHighScore(highScore);
            }
        }
    }

    public void signIn()
    {
        UserService userService = App42API.BuildUserService();
        UnityCallBack callBack = new UnityCallBack();
        if (userName.text == "")
        {
            Debug.Log("Signing in");
            userService.Authenticate(signInName.text, signInPassword.text, callBack);
            callBack.name = signInName.text;
            callBack.pass = signInPassword.text;
        }
        else
        {
            Debug.Log("Signing up");
            userService.CreateUser(userName.text, password.text, email.text, callBack);
            callBack.name = userName.text;
            callBack.pass = password.text;
        }

        callBackStack(callBack);

    }

    public void saveUser(String name, String pass)
    {
        PlayerPrefs.SetString("Name", name);
        PlayerPrefs.SetString("Pass", pass);
    }

    public void submitHighScore(int score)
    {
        String gameName = "Oort";
        String userName = user;
        Debug.Log("Submitting HS");
        scoreBoardService.SaveUserScore(gameName, userName, score, new ScoreCallBack());
        refreshHighScoreBoard();
    }

    public void refreshHighScoreBoard()
    {
        String gameName = "Oort";
        int max = 20;
        if (globalWait.calledBack == true)
        {
            globalWait = new GlobalScoreCallBack();
            scoreBoardService.GetTopNRankers(gameName, max, globalWait);
        }
    }

    public void checkForScoreCallBack()
    {
        if (globalWait.calledBack)
        {
            refreshHighScoreTxt(globalWait.getGame());
            globalWait.refreshable = true;
        }
    }

    public void refreshHighScoreTxt(Game game)
    {
        scoreBoard.text = "";
        scoreBoardScores.text = "";
        rank.text = "";
        for (int i = 0; i < game.GetScoreList().Count; i++)
        {
            rank.text += (i+1) + ":\n";
            scoreBoard.text += game.GetScoreList()[i].GetUserName() + "\n";
            scoreBoardScores.text += game.GetScoreList()[i].GetValue() + "\n";
        }
    }

    public class UnityCallBack : App42CallBack
    {
        private bool wasSuc = false;
        public bool calledBack = false;
        public String name;
        public String pass;
        public void OnException(Exception ex)
        {
            Debug.Log("Exception: "+ex);
            wasSuc = false;
            calledBack = true;
        }

        public void OnSuccess(object response)
        {
            User user = (User)response;
            Debug.Log("Welcome: " + user.GetUserName());
            wasSuc = true;
            calledBack = true;
        }

        public bool wasSuccessful()
        {
            return wasSuc;
        }
    }

    public class ScoreCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Game game = (Game)response;
            App42Log.Console("gameName is " + game.GetName());
            for (int i = 0; i < game.GetScoreList().Count; i++)
            {
                App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());
                App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());
                //PlayerPrefs.SetString("scoreID", game.GetScoreList()[i].GetScoreId());
            }
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }

    public class GlobalScoreCallBack : App42CallBack
    {
        private Game game;
        public bool calledBack = false;
        public bool refreshable = false;
        public void OnSuccess(object response)
        {
            calledBack = true;
            game = (Game)response;
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
            calledBack = true;
        }

        public Game getGame()
        {
            return game;
        }
    }

    public void editUI(bool edit)
    {
        for(int a = 0; a < ui.Count; a++)
        {
            ui[a].SetActive(edit);
        }
    }

    public void setHighScore(int score)
    {
        highScore = score;
    }

    public void signOut()
    {
        leaderBoardUI.SetActive(false);
        showSignIn();
    }
}
