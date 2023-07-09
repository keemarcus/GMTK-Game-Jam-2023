using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public BoardManager boardManager;
    public TMP_Text scoreCounter;
    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
    }
    void Update()
    {
        scoreCounter.text = boardManager.score.ToString();
    }
}
