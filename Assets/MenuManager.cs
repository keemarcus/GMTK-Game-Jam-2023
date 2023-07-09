using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public string gameSceneName;
    public TMP_Text titleText;
    public TMP_Text subTitleText;
    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        DontDestroyOnLoad(scoreManager.gameObject);
        Debug.Log(Screen.currentResolution.ToString());
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);

        if (scoreManager.score > 0)
        {
            titleText.text = "SCORE";
            subTitleText.text = scoreManager.score.ToString();
        }
        else
        {
            titleText.text = "MATCH MAK3R";
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
