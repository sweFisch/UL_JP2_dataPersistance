using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _highScoreText;
    [SerializeField] TMP_InputField _inputPlayerName;

    private void Start()
    {
        UpdateHighScoreText();
        _inputPlayerName.text = HighScoreData.Instance.GetCurrentPlayerName();
    }


    public void UpdateHighScoreText()
    {
        _highScoreText.SetText(HighScoreData.Instance.GetHighScoreText()); // set the highscore text
    }

    public void SavePlayerName()
    {
        string playerName = _inputPlayerName.text;
        if (playerName == null || playerName == string.Empty || playerName == "Input Player Name..")
        {
            playerName = "Name";
        }
        //Debug.Log(playerName);

        HighScoreData.Instance.SetCurrentPlayerName(playerName);
        HighScoreData.Instance.SetCurrentScore(0);
    }


    public void StartGame()
    {
        SavePlayerName();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif   

    }

    public void ClearHighScore()
    {
        HighScoreData.Instance.ClearData();
        UpdateHighScoreText();
    }
}
