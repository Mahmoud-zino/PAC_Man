using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    [SerializeField]
    private PathMapCalculator pathMapCalculator;


    private List<PathNode> openList;
    private List<PathNode> closedList;

    private PathGrid pathGrid;

    private void Start()
    {
        pathGrid = pathMapCalculator.pathGrid;
    }

    private void ResetAllNodesInGrid(PathGrid pathGrid)
    {
        for (int x = 0; x < pathGrid.Width; x++)
        {
            for (int y = 0; y < pathGrid.Height; y++)
            {

                PathNode node = pathGrid.GetValue(x, y);

                node.GCost = int.MaxValue;
                node.CalculateFCost();
                node.ParentNode = null;
            }
        }
    }

    private int CalculateDistanceCost(Vector2 a, Vector2 b)
    {
        return (int)(Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathList)
    {
        PathNode lowestFCostNode = pathList[0];
        for (int i = 0; i < pathList.Count; i++)
        {
            if (pathList[i].FCost < lowestFCostNode.FCost)
                lowestFCostNode = pathList[i];
        }
        return lowestFCostNode;
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        //left neighbour
        if (currentNode.Index.x - 1 >= 0)
            neighbourList.Add(pathGrid.GetValue(currentNode.Index.x - 1, currentNode.Index.y));

        //right neighbour
        if (currentNode.Index.x + 1 < pathGrid.Width)
            neighbourList.Add(pathGrid.GetValue(currentNode.Index.x + 1, currentNode.Index.y));

        //down neighbour
        if (currentNode.Index.y - 1 >= 0)
            neighbourList.Add(pathGrid.GetValue(currentNode.Index.x, currentNode.Index.y - 1));

        //up neighbour
        if (currentNode.Index.y + 1 < pathGrid.Height)
            neighbourList.Add(pathGrid.GetValue(currentNode.Index.x, currentNode.Index.y + 1));

        return neighbourList;
    }

    private List<PathNode> CalculateEndPath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>() { endNode };

        PathNode currentNode = endNode;

        while (currentNode.ParentNode != null)
        {
            path.Add(currentNode.ParentNode);
            currentNode = currentNode.ParentNode;
        }

        path.Reverse();

        return path;
    }

    public List<PathNode> FindPath(Transform query, Transform target)
    {
        PathNode startNode = pathGrid.GetValue(query.position);
        PathNode endNode = pathGrid.GetValue(target.position);

        openList = new List<PathNode>() { startNode };
        closedList = new List<PathNode>();

        ResetAllNodesInGrid(pathGrid);

        //calculating start node
        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(pathGrid.GetWorldPosition(startNode.Index),
                                                pathGrid.GetWorldPosition(endNode.Index));
        startNode.CalculateFCost();

        //so long that we have a road to check
        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
                return CalculateEndPath(endNode);

            //move current node from open to closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                    continue;

                if (neighbourNode.IsObstacle)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeCost = currentNode.GCost +
                    CalculateDistanceCost(pathGrid.GetWorldPosition(currentNode.Index),
                                          pathGrid.GetWorldPosition(endNode.Index));

                if (tentativeCost < neighbourNode.GCost)
                {
                    neighbourNode.ParentNode = currentNode;
                    neighbourNode.GCost = tentativeCost;
                    neighbourNode.HCost = CalculateDistanceCost(
                                        pathGrid.GetWorldPosition(neighbourNode.Index),
                                        pathGrid.GetWorldPosition(endNode.Index));
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        //no path was found
        return new List<PathNode>();
    }
}
