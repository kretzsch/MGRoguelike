using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LeaderBoardManager : MonoBehaviour
{
    public ScoreManager scoreManager; // Drag and drop ScoreManager here
    public TMP_Text leaderboardText; // Drag and drop Text UI element here

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Add this line to make ScoreManager persistent
    }

    public void UpdateLeaderboard()
    {
        List<HighScore> highScores = scoreManager.GetHighScores();
        foreach (HighScore highScore in highScores)
        {
            leaderboardText.text += highScore.playerName + ": " + FormatTime(highScore.score) + "\n";
        }
    }

    public void ClearLeaderboard()
    {
        leaderboardText.text = "";
    }
    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

}
