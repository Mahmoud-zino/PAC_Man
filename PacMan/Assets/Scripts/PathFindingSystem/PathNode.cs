using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public PathNode(Vector2Int index, bool isObstacle, PathGrid parentGrid)
    {
        this.Index = index;
        this.IsObstacle = isObstacle;
        this.ParentGrid = parentGrid;
    }

    public Vector2Int Index { get; }

    public bool IsObstacle { get; }

    public PathNode ParentNode { get; set; }
    public PathGrid ParentGrid { get; private set; }

    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; private set; }
    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
}
