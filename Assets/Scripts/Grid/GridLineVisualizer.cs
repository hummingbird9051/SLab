using UnityEngine;

public class GridLineVisualizer : MonoBehaviour
{
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Color lineColor = Color.white;
    [SerializeField] private float lineWidth = 0.01f;
    [SerializeField] private int sortingOrder = 0;

    public void DrawGridLines<T>(IGrid<T> grid)
    {
        int width = grid.GetWidth();
        int height = grid.GetHeight();
        float cellSize = grid.GetCellSize();

        Vector3 originPos = grid.GetWorldPosition(0, 0) - new Vector3(cellSize, cellSize) * 0.5f;

        for (int x = 0; x <= width; x++)
        { 
            DrawLine(
                originPos + new Vector3(x * cellSize, 0, 0),
                originPos + new Vector3(x * cellSize, height * cellSize, 0)
                );
        }

        for (int y = 0; y <= height; y++)
        {
            DrawLine(
                originPos + new Vector3(0, y * cellSize, 0), 
                originPos + new Vector3(width * cellSize, y * cellSize, 0)
                );
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("GridLine");
        lineObject.transform.SetParent(this.transform);

        LineRenderer lr = lineObject.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = 2;
        lr.sortingOrder = sortingOrder;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
