using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int spriteID;
    public int nextSpriteID;
    public InputHandler inputHandler;
    public LayerMask tileMask;
    public BoardManager boardManager;
    public Vector2Int boardPosition;
    public bool waiting;
    public float changeTimer;
    public float changeDelay;
    public UIManager uiManager;
    public Sprite nextSprite;
    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        boardManager = FindObjectOfType<BoardManager>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetUpChangeTimer(changeDelay);
        nextSpriteID = spriteID;
        while (nextSpriteID == spriteID)
        {
            nextSpriteID = Random.Range(0, boardManager.playerSprites.Count);
        }
        nextSprite = boardManager.playerSprites[nextSpriteID];
        uiManager.SetNextSpriteImage(nextSprite);
        changeTimer = changeDelay;
        uiManager.SetChangeTimer(changeTimer);
        waiting = true;
    }
    private void Update()
    {
        if (waiting) { return; }

        if (changeTimer <= 0)
        {
            spriteID = nextSpriteID;
            this.GetComponent<SpriteRenderer>().sprite = nextSprite;

            while (nextSpriteID == spriteID)
            {
                nextSpriteID = Random.Range(0, boardManager.playerSprites.Count);
            }

            nextSprite = boardManager.playerSprites[nextSpriteID];
            uiManager.SetNextSpriteImage(nextSprite);
            changeTimer = changeDelay;
        }
        else
        {
            changeTimer -= Time.deltaTime;
            uiManager.SetChangeTimer(changeTimer);
        }

        inputHandler.TickInput(Time.deltaTime);
    }
    private void LateUpdate()
    {
        inputHandler.movement = Vector2.zero;
    }
    public void Move(float x, float y)
    {
        switch (x, y)
        {
            case (0,1):
                SwapTile(Vector2Int.up);
                break;
            case (0, -1):
                SwapTile(Vector2Int.down);
                break;
            case (1, 0):
                SwapTile(Vector2Int.right);
                break;
            case (-1, 0):
                SwapTile(Vector2Int.left);
                break;
            default:
                Debug.Log("unknown movement");
                break;
        }
    }

    public void SwapTile(Vector2Int direction)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(this.transform.position + (Vector3Int)direction, 0.1f, tileMask);
        if (hitCollider == null) { return; }
        boardManager.FreezeTiles();
        TileManager swapTile = hitCollider.transform.gameObject.GetComponent<TileManager>();
        swapTile.MoveTile(this.transform.position);
        this.transform.position = this.transform.position + (Vector3Int)direction;
        boardPosition += direction;
    }
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        boardManager.GenerateTile(boardPosition + new Vector2Int(0,3), false);
    }
}
