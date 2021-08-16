using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private PathGrid parentGrid;
    public PathNode(Vector2Int index, bool isObstacle, PathGrid parentGrid)
    {
        this.Index = index;
        this.IsObstacle = isObstacle;
        this.parentGrid = parentGrid;
    }

    public Vector2Int Index { get; }

    public bool IsObstacle { get; }

    public PathNode ParentNode { get; set; }

    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; private set; }
    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }

    public Vector3 GetWorldPosition()
    {
        return this.parentGrid.GetWorldPosition(this.Index);
    }
}
