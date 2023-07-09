using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2 newRowStartPoint;
    public Vector2Int boardSize;
    public List<Sprite> tileSprites;
    public List<Sprite> playerSprites;
    public GameObject tilePrefab;
    public GameObject playerPrefab;
    public int[] lastTwoSprites;
    public int[] lastRow;
    public int[] secondToLastRow;
    private void Awake()
    {
        lastRow = new int[boardSize.x];
        secondToLastRow = new int[boardSize.x];
        lastTwoSprites = new int[2];
        GenerateField();
    }
    private void GenerateField()
    {
        for(int i = 0; i < boardSize.y; i++)
        {
            secondToLastRow = lastRow;
            lastRow = GenerateRow(i, boardSize.x);
        }
    }
    private int[] GenerateRow(int rowNumber, int rowSize)
    {
        int[] newRow = new int[rowSize];
        for(int i = 0; i < rowSize; i++)
        {
            newRow[i] = GenerateTile(new Vector2Int(i, rowNumber), true);
        }
        return newRow;
    }
    public int GenerateTile(Vector2Int position, bool initialLoad)
    {
        int spriteIndex = Random.Range(0, tileSprites.Count);
        if(initialLoad && (lastTwoSprites[0] == lastTwoSprites[1] || lastRow[position.x] == secondToLastRow[position.x]))
        {
            while(spriteIndex == lastTwoSprites[0] || spriteIndex == lastRow[position.x])
            {
                spriteIndex = Random.Range(0, tileSprites.Count);
            }
        }

        if (initialLoad && position.x == (int)boardSize.x / 2 && position.y == (int)boardSize.y / 2)
        {
            GameObject player = Instantiate(playerPrefab, position + newRowStartPoint, Quaternion.identity);
            player.GetComponent<SpriteRenderer>().sprite = playerSprites[spriteIndex];
            player.GetComponent<PlayerManager>().spriteID = spriteIndex;
            player.GetComponent<PlayerManager>().boardPosition = position;
            lastTwoSprites[0] = lastTwoSprites[1];
            lastTwoSprites[1] = spriteIndex;
            return spriteIndex;
        }

        GameObject newTile = Instantiate(tilePrefab, position + newRowStartPoint, Quaternion.identity);
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[spriteIndex];
        newTile.GetComponent<TileManager>().spriteID = spriteIndex;
        newTile.GetComponent<TileManager>().boardPosition = position;

        lastTwoSprites[0] = lastTwoSprites[1];
        lastTwoSprites[1] = spriteIndex;
        return spriteIndex;
    }
}
