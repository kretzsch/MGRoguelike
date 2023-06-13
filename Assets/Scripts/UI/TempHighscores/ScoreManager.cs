using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class HighScore
{
    public string playerName;
    public float score;
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;

    public List<HighScore> highScores = new List<HighScore>();
    public LeaderBoardManager leaderboardManager;

    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject); // Add this line to make ScoreManager persistent
    }

    public void SubmitScore()
    {
        string playerName = playerNameInputField.text;
        float score = Timer.endTime;
        AddHighScore(playerName, score);

        // Clear the leaderboard text at the start of each game
         //leaderboardManager.ClearLeaderboard();
        leaderboardManager.UpdateLeaderboard();
    }


    private void AddHighScore(string playerName, float score)
    {
        HighScore newHighScore = new HighScore();
        newHighScore.playerName = playerName;
        newHighScore.score = score;

        highScores.Add(newHighScore);

        // Sort the list in descending order based on score
        highScores.Sort((score1, score2) => score2.score.CompareTo(score1.score));
    }

    public List<HighScore> GetHighScores()
    {
        return highScores;
    }
}
