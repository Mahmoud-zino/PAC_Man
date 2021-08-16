using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)] //start after other scripts
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.3f;

    private PathAgent pathAgent;
    private Transform[] targets;
    private List<PathNode> pathNodes = new List<PathNode>();

    private int lastTarget = -1;
    private Vector2 direction = Vector2.right;

    private void Start()
    {
        pathAgent = GetComponent<PathAgent>();
        targets = GameObject.Find("PathPoints").GetComponentsInChildren<Transform>();
        pathNodes = pathAgent.FindPath(this.transform, GetNewTarget());
        StartCoroutine(MoveBlockOverTime());
    }

    private Transform GetNewTarget()
    {
        int targetIndex = Random.Range(0, targets.Length);

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

    public IEnumerator MoveBlockOverTime()
    {
        //search until a path was found
        while (pathNodes.Count == 0)
        {
            pathNodes = pathAgent.FindPath(this.transform, GetNewTarget());
        }

        this.direction = CalculateDirection(pathNodes[0].ParentGrid.GetWorldPosition(pathNodes[0].Index));

        pathNodes.RemoveAt(0);

        //to move one block at a time
        float elepsedTime = 0;
        Vector3 startPos = this.transform.position;
        Vector3 endPos = this.transform.position + new Vector3(direction.x, direction.y, 0);

        while (elepsedTime < speed)
        {
            this.transform.position = Vector3.Lerp(startPos, endPos, (elepsedTime / speed));
            elepsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        this.transform.position = endPos;

        yield return MoveBlockOverTime();
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
}
