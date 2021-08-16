using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathMapCalculator : MonoBehaviour
{
    [SerializeField]
    private bool debugMode;
    [SerializeField]
    private LayerMask obstacleLayer;


    [HideInInspector]
    public PathGrid pathGrid;

    //important to be called before the other scripts
    private void Awake()
    {
        this.pathGrid = new PathGrid
            (Mathf.FloorToInt(this.transform.localScale.x),
             Mathf.FloorToInt(this.transform.localScale.y),
             this.transform.position);

        for (int x = 0; x < this.pathGrid.Width; x++)
        {
            for (int y = 0; y < this.pathGrid.Height; y++)
            {
                pathGrid.SetValue(x, y, new PathNode(new Vector2Int(x, y),
                                                    IsObstacleAtPosition(pathGrid.GetWorldPosition(x, y)),
                                                    pathGrid));
            }
        }
    }

    private bool IsObstacleAtPosition(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, pathGrid.CellSize / 4, obstacleLayer);

        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        if (!debugMode)
            return;

        this.pathGrid = new PathGrid
            (Mathf.FloorToInt(this.transform.localScale.x),
             Mathf.FloorToInt(this.transform.localScale.y),
             this.transform.position);

        for (int x = 0; x < this.pathGrid.Width; x++)
        {
            for (int y = 0; y < this.pathGrid.Height; y++)
            {
                pathGrid.SetValue(x, y, new PathNode(new Vector2Int(x, y),
                                                    IsObstacleAtPosition(pathGrid.GetWorldPosition(x, y)),
                                                    pathGrid));
            }
        }

        for (int x = 0; x < pathGrid.Width; x++)
        {
            for (int y = 0; y < pathGrid.Height; y++)
            {
                PathNode pn = pathGrid.GetValue(x, y);
                if (pn == null)
                    continue;
                Gizmos.color = pn.IsObstacle ? Color.red : Color.green;
                Gizmos.DrawWireSphere(pathGrid.GetWorldPosition(x, y), pathGrid.CellSize / 2);
            }
        }
    }
}
