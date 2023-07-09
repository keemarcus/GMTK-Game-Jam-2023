using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public SpriteRenderer render;
    public Rigidbody2D body;
    public BoardManager boardManager;
    public Vector2Int boardPosition;
    public int spriteID;
    public bool checkedMatches;
    public float checkTimer;
    public float checkDelay;
    public List<GameObject> matches;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        boardManager = FindObjectOfType<BoardManager>();
        checkedMatches = true;
        checkTimer = 0f;
    }

    private void Update()
    {
        if(body.velocity.magnitude > 0)
        {
            checkedMatches = false;
            checkTimer = checkDelay;
        }
        else
        {
            checkTimer -= Time.deltaTime;
            if (!checkedMatches && checkTimer <= 0f)
            {
                CheckAdjacentGems();
            }
        }
    }

    public void CheckAdjacentGems()
    {
        matches.Clear();

        List<GameObject> upMatches = FindMatch(Vector2.up);
        List<GameObject> rightMatches = FindMatch(Vector2.right);
        List<GameObject> downMatches = FindMatch(Vector2.down);
        List<GameObject> leftMatches = FindMatch(Vector2.left);

        if (upMatches.Count >= 2 || rightMatches.Count >= 2 || downMatches.Count >= 2 || leftMatches.Count >= 2)
        {
            foreach (GameObject gem in matches)
            {
                Destroy(gem);
            }
            Destroy(this.gameObject);
        }

        checkedMatches = true;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir, 1f);
        while (hit.collider != null && ((hit.collider.GetComponent<TileManager>() != null && hit.collider.GetComponent<TileManager>().spriteID == this.spriteID) || (hit.collider.GetComponent<PlayerManager>() != null && hit.collider.GetComponent<PlayerManager>().spriteID == this.spriteID)))// hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            matches.Add(hit.collider.gameObject);
            /*foreach(GameObject tile in hit.collider.gameObject.GetComponent<TileManager>().matches)
            {
                matches.Add(tile);
            }*/
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir, 1f);
        }
        return matchingTiles;
    }

    public void MoveTile(Vector2 newPosition)
    {
        boardPosition += new Vector2Int((int)(newPosition.x - this.transform.position.x), (int)(newPosition.y - this.transform.position.y));
        this.transform.position = newPosition;
        checkedMatches = false;
        checkTimer = checkDelay;
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        boardManager.GenerateTile(boardPosition, false);
    }
}
