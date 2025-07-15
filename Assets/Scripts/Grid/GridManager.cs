using System.Linq.Expressions;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    private int width;
    private int height;
    [SerializeField] private float cellSize = 0.5f;

    [SerializeField] private GridView gridView;
    [SerializeField] private GridLineVisualizer gridLineVisualizer;

    //해당 그리드가 들어갈 위치를 정하기 위해 카메라 컴포넌트로 받음.
    public Camera gridCamera;

    private IGrid<int> grid;

    //타일 번호 저장
    private int currentTileId = 1;
    void Start()
    {
        width = (int)(gridCamera.orthographicSize * gridCamera.aspect) * 2;
        height = (int)(gridCamera.orthographicSize) * 2 - 1;
        Debug.Log(width + ", " + height);
        grid = new GridSystem<int>(width, height, cellSize, transform.parent.position - new Vector3(width / 2, height / 2 + 1, 0), 
            (g, x, y) => 0);

        if (gridLineVisualizer != null)
        {
            gridLineVisualizer.DrawGridLines(grid);
        }

        gridView.Initialize(grid);
    }

    public void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentTileId = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentTileId = 2;

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = gridCamera.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(mouseWorldPos, out int x, out int y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouseWorldPos = gridCamera.ScreenToWorldPoint(Input.mousePosition);
            grid.GetXY(mouseWorldPos, out int x, out int y);

            grid.SetValue(x, y, currentTileId);

            gridView.UpdateTileVisual(x, y);
        }
    }
}
