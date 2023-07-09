using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BoardManager boardManager;
    public TMP_Text scoreCounter;
    public Slider changeTimer;
    public Image nextPlayerSprite;
    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
    }
    void Update()
    {
        scoreCounter.text = boardManager.score.ToString();
    }
    public void SetUpChangeTimer(float maxValue)
    {
        changeTimer.maxValue = maxValue;
    }
    public void SetChangeTimer(float currentValue)
    {
        changeTimer.value = currentValue;
    }
    public void SetNextSpriteImage(Sprite sprite)
    {
        nextPlayerSprite.sprite = sprite;
    }
}
