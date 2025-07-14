using UnityEngine;
using System;

public class GridSystem<TGridObject> : IGrid<TGridObject>
{
    private int width;
    private int height;
    private TGridObject[,] gridArray;
    private Vector3 originPos;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize, Vector3 originalPos,
        Func<GridSystem<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originalPos;
        gridArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }


    public int GetWidth() => width;
    public int GetHeight() => height;
    public float GetCellSize() => cellSize;

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos + new Vector3(cellSize, cellSize) * 0.5f;
    }

    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
}
