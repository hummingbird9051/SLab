using UnityEngine;

public class GridVisualizer<TGridObject>
{
    private IGrid<TGridObject> grid;

    public GridVisualizer(IGrid<TGridObject> grid)
    {
        this.grid = grid;
    }

    public void DrawLines()
    {
        int width = grid.GetWidth();
        int height = grid.GetHeight();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellCenter = grid.GetWorldPosition(x, y);
                float halfCell = grid.GetCellSize() / 2f;
                Vector3 bottomLeft = cellCenter - new Vector3(halfCell, halfCell);

                Debug.DrawLine(bottomLeft, bottomLeft + new Vector3(0, grid.GetCellSize()), Color.white, 100f);
                Debug.DrawLine(bottomLeft, bottomLeft + new Vector3(grid.GetCellSize(), 0), Color.white, 100f);
            }
        }

        Vector3 origin = grid.GetWorldPosition(0, 0) - new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f;
        Debug.DrawLine(origin + new Vector3(0, height * grid.GetCellSize()),
            origin + new Vector3(width * grid.GetCellSize(), height * grid.GetCellSize()), Color.white, 100f);
        Debug.DrawLine(origin + new Vector3(width * grid.GetCellSize(), 0),
            origin + new Vector3(width * grid.GetCellSize(), height * grid.GetCellSize()), Color.white, 100f);
    }
} 
