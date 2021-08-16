using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid
{
    public float CellSize { get; }
    public int Width { get; }
    public int Height { get; }


    private readonly PathNode[,] gridArray;

    private Vector3 originPosition;

    public PathGrid(int width, int height, Vector3 originPosition, float cellSize = 1)
    {
        this.Width = width;
        this.Height = height;
        this.originPosition = originPosition;
        this.CellSize = cellSize;

        gridArray = new PathNode[width, height];
    }

    private void GetXY(Vector3 position, out int x, out int y)
    {
        x = Mathf.FloorToInt((position - originPosition).x / CellSize);
        y = Mathf.FloorToInt((position - originPosition).y / CellSize);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + originPosition;
    }

    public Vector3 GetWorldPosition(Vector2 index)
    {
        return new Vector3(index.x, index.y) * CellSize + originPosition;
    }

    public void SetValue(int x, int y, PathNode node)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return;

        this.gridArray[x, y] = node;
    }

    public PathNode GetValue(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return default;

        return gridArray[x, y];
    }

    public PathNode GetValue(Vector3 position)
    {
        GetXY(position, out int x, out int y);
        return GetValue(x, y);
    }
}
