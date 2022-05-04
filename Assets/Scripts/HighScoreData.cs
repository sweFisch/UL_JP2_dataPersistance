using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreData : MonoBehaviour
{
    public static HighScoreData Instance;

    private string _highscore_playerName;
    private int _highscore;

    private string _currentPlayer;
    private int _currentScore;

    private const string PATH_SAVE_FILE = "/savefile.json";

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highscore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.playerName = _highscore_playerName;
        data.highscore = _highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + PATH_SAVE_FILE, json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + PATH_SAVE_FILE;
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _highscore_playerName = data.playerName;
            _highscore = data.highscore;
        }
    }


    public void CheckAndSetHighScore(int score)
    {
        _currentScore = score;
        if(_currentScore > _highscore)
        {
            _highscore = _currentScore;
            _highscore_playerName = _currentPlayer;
            SaveHighScore();
        }
    }

    public void SetCurrentPlayerName(string playerName)
    {
        _currentPlayer = playerName;
    }

    public string GetCurrentPlayerName()
    {
        return _currentPlayer;
    }

    public void SetCurrentScore(int score)
    {
        _currentScore = score;
    }

    public int GetHighScore()
    {
        return _highscore;
    }

    public string GetHighScoreText()
    {

        return $"{_highscore_playerName} : {_highscore}";
    }

    public void ClearData()
    {
        _highscore_playerName = "Name";
        _highscore = 0;
        _currentPlayer = "Name";
        _currentScore = 0;
        SaveHighScore();

    }
}
