using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)] //start after other scripts
public class EnemyMovement : Movement
{
    private PathAgent pathAgent;
    private Transform[] targets;
    private List<PathNode> pathNodes = new List<PathNode>();
    private GameManager gameManager;

    private int lastTarget = -1;

    private void Start()
    {
        pathAgent = GetComponent<PathAgent>();
        targets = GameObject.Find("PathPoints").GetComponentsInChildren<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pathNodes = pathAgent.FindPath(this.transform, GetNewTarget());
        StartCoroutine(MoveEnemy());
    }

    private Transform GetNewTarget()
    {
        int targetIndex = Random.Range(1, targets.Length);

        if(targetIndex == lastTarget)
        {
            //last target index in the list
            if (targetIndex == targets.Length - 1)
            {
                targetIndex--;
            }
            else
                targetIndex++;

        }
        lastTarget = targetIndex;
        return targets[targetIndex];
    }

    private Vector2 CalculateDirection(Vector2 pos)
    {
        if (this.transform.position.x == pos.x)
        {
            if (this.transform.position.y > pos.y)
                return Vector2.down;
            else if (this.transform.position.y < pos.y)
                return Vector2.up;
            else
                return Vector2.zero;
        }
        else
        {
            if (this.transform.position.x > pos.x)
                return Vector2.left;
            else if (this.transform.position.x < pos.x)
                return Vector2.right;
            return Vector2.zero;
        }
    }

    private IEnumerator MoveEnemy()
    {
        if (gameManager.IsGameOver)
            yield return null;
        else
        {
            //search until a path was found
            while (pathNodes.Count == 0)
            {
                pathNodes = pathAgent.FindPath(this.transform, GetNewTarget());
            }

            base.Move(CalculateDirection(pathNodes[0].GetWorldPosition()));

            yield return base.MoveBlockOverTime();

            pathNodes.RemoveAt(0);

            yield return MoveEnemy();
        }
    }
}
