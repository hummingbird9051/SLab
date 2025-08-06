using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
//그리드 매니저로 타일 아이디 조정 후 입력
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
        width = (int)((gridCamera.orthographicSize * gridCamera.aspect) * 2 + 0.5f);
        height = (int)(gridCamera.orthographicSize) * 2 - 2;
        Debug.Log(width + ", " + height);
        grid = new GridSystem<int>(width, height, cellSize, transform.parent.position - new Vector3(width / 2, height / 2, 0) + new Vector3(-0.5f, 0f), 
            (gridSystem, cellX, cellY) => 0);

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

    public void SetCurrentTileId(int val)
    {
        currentTileId = val;
    }

    private void HandleInput()
    {
        Vector3 mouseWorldPos = gridCamera.ScreenToWorldPoint(Input.mousePosition);
        grid.GetXY(mouseWorldPos, out int x, out int y);
        if (currentTileId != 0) // 0번은 없는 오브젝트이므로
        {
            grid.SetValue(x, y, currentTileId);
            gridView.TemporaryTileVisual(x, y);
        }
        else
        {
            gridView.ClearTemporaryVisual();
        }

        if (Input.GetMouseButtonUp(0) && grid.GetValue(x, y) != 0)
        {
            if (currentTileId != 0)
            {
                grid.SetValue(x, y, currentTileId);

                gridView.UpdateTileVisual(x, y);
                currentTileId = 0;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            currentTileId = 0;
        }
    }

    
}
