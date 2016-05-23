using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GPGController : MonoBehaviour {

    #region PUBLIC_VAR
    public string leaderboard;
    #endregion
    private bool loggedIn;

    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        loggedIn = false;
        if (PlayerPrefs.HasKey("gpg") && PlayerPrefs.GetInt("gpg") == 1)
        {
            LogIn();
        }
    }
    #endregion

    #region BUTTON_CALLBACKS
    public void onClick()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            LogIn();
        }
        else
        {
            OnShowLeaderBoard();
        }
    }
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            loggedIn = success;
            if (success)
            {
                Debug.Log("Login Sucess");
                if (!PlayerPrefs.HasKey("gpg") || PlayerPrefs.GetInt("gpg")==0)
                {
                    PlayerPrefs.SetInt("gpg", 1);
                    if (PlayerPrefs.HasKey("HS"))
                    {
                        addHighScore((int)Mathf.Round(PlayerPrefs.GetFloat("HS")));    
                        //leaderBoard.setHighScore((int)Mathf.Round(highScore));
                    }
                }
            }
            else {
                Debug.Log("Login failed");
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
#endregion

    public void addHighScore(int score)
    {
        if (loggedIn)
        {
            OnAddScoreToLeaderBorad(score);
        }
    }
}
