using UnityEngine;

public class GridView : MonoBehaviour
{
    private IGrid<int> grid;
    private GameObject[,] visualGridArray;

    public Sprite[] tileSprites;

    public void Initialize(IGrid<int> grid)
    {
        this.grid = grid;
        visualGridArray = new GameObject[grid.GetWidth(), grid.GetHeight()];
        DrawInitialGrid();
    }
    
    //초기 그리드 전체 그리기
    private void DrawInitialGrid()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                CreateTileVisual(x, y);
            }
        }
    }

    

    //특정 칸 비주얼 업데이트
    public void UpdateTileVisual(int x, int y)
    {
        if (x < 0 || y < 0 || x >= grid.GetWidth() || y >= grid.GetHeight())
        {
            return;
        }
        if (visualGridArray[x, y] != null)
        {
            Destroy(visualGridArray[x, y]);
        }

        CreateTileVisual(x, y);
    }

    public void DeleteAllTileVisual()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                DeleteTileVisual(x, y);
            }
        }
    }

    //특정 위치에 타일 비주얼 생성
    private void CreateTileVisual(int x, int y)
    {
        int tileId = grid.GetValue(x, y);

        if (tileId <= 0 || tileId >= tileSprites.Length) return;

        GameObject tileObject = new GameObject("Tile_" + x + "_" + y);
        tileObject.transform.SetParent(this.transform);
        tileObject.transform.position = grid.GetWorldPosition(x, y);

        SpriteRenderer spriteRenderer = tileObject.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = tileSprites[tileId];

        tileObject.transform.localScale *= grid.GetCellSize();
        tileObject.AddComponent<PolygonCollider2D>();
        visualGridArray[x, y] = tileObject;
    }


    private void DeleteTileVisual(int x, int y)
    {
        int tileId = grid.GetValue(x, y);
        if (tileId <= 0 || tileId >= tileSprites.Length) return;

        if (visualGridArray[x, y] == null) return;
        else Destroy(visualGridArray[x, y]);
    }
    
}
