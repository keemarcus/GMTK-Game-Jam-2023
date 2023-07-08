using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public SpriteRenderer render;
    public Rigidbody2D body;
    public bool checkedMatches;
    public float checkTimer;
    public float checkDelay;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
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
        List<GameObject> upMatches = FindMatch(Vector2.up);
        List<GameObject> rightMatches = FindMatch(Vector2.right);
        List<GameObject> downMatches = FindMatch(Vector2.down);
        List<GameObject> leftMatches = FindMatch(Vector2.left);

        if(upMatches.Count >= 2)
        {
            foreach(GameObject gem in upMatches)
            {
                Destroy(gem);
            }
            Destroy(this.gameObject);
        }
        if (rightMatches.Count >= 2)
        {
            foreach (GameObject gem in rightMatches)
            {
                Destroy(gem);
            }
            Destroy(this.gameObject);
        }
        if (downMatches.Count >= 2)
        {
            foreach (GameObject gem in downMatches)
            {
                Destroy(gem);
            }
            Destroy(this.gameObject);
        }
        if (leftMatches.Count >= 2)
        {
            foreach (GameObject gem in leftMatches)
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
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir, 1f);
        }
        return matchingTiles;
    }
}
