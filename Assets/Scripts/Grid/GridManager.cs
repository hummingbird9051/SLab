using System.Linq.Expressions;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;

    [SerializeField] private GridView gridView;

    public Camera gridCamera;

    private IGrid<int> grid;

    //타일 번호 저장
    private int currentTileId = 1;
    void Start()
    {
        grid = new GridSystem<int>(width, height, cellSize, transform.parent.position, 
            (g, x, y) => 0);

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

            Debug.Log(x + ", " + y);
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
