using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GridView : MonoBehaviour
{
    private IGrid<int> grid;
    private GameObject[,] visualGridArray;
    private GameObject tempVisualObject;
    private int width;
    private int height;
    private int prevX = -1;
    private int prevY = -1;
    private Vector2Int currentTileSize;
    [SerializeField] private int eraseGridCost;

    //클래스를 만들어 인스펙터 창에 보여줄 수 있게 만들수 있는 코드
    [System.Serializable]
    public class TileObject
    {
        public GameObject tileObject;
        public Vector2Int tileSize;
        public int costSlime;
    }

    public List<TileObject> tileObjects;
    public void Initialize(IGrid<int> grid)
    {
        this.grid = grid;
        width = grid.GetWidth();
        height = grid.GetHeight();
        visualGridArray = new GameObject[width, height];
        DrawInitialGrid();
    }
    
    //초기 그리드 전체 그리기, null 오브젝트만 생성
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

    //드래그 도중에 보여줄 임시 비주얼
    public void TemporaryTileVisual(int x, int y)
    {
        if (prevX == x && prevY == y) return;

        prevX = x;
        prevY = y;

        int tileId = grid.GetValue(x, y);

        TileObject tileToPlace = tileObjects[tileId];

        if (tempVisualObject != null) Destroy(tempVisualObject);
        
        Vector2Int tileSize = tileToPlace.tileSize;
        if (!IsPlacementValid(x, y, tileSize)) return;
        tempVisualObject = Instantiate(
            tileToPlace.tileObject, 
            new Vector3(grid.GetWorldPosition(x, y).x, grid.GetWorldPosition(x, y).y, -1),
            Quaternion.identity,
            this.transform);


        //preproduction for TemporaryTileVisual Object
        for (int i = 0; i < tempVisualObject.transform.childCount; i++)
        {
            GameObject child = tempVisualObject.transform.GetChild(i).gameObject;
            if (child == null) continue;
            Collider2D childCollider2D = child.GetComponent<Collider2D>();
            if (childCollider2D == null) continue;
            childCollider2D.enabled = false;
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            if (childSpriteRenderer == null) continue;
            childSpriteRenderer.color -= new Color(0, 0, 0, 0.5f);
        }
        tempVisualObject.transform.localScale *= grid.GetCellSize();


    }

    //임시 비주얼 삭제

    public void ClearTemporaryVisual()
    {
        if (tempVisualObject != null)
        {
            Destroy(tempVisualObject);
        }

        prevX = -1;
        prevY = -1;
    }



    //특정 칸 비주얼 업데이트
    public void UpdateTileVisual(int x, int y)
    {
        CreateTileVisual(x, y);
        prevX = -1; prevY = -1;
    }


    public void DeleteOneTileVisual(int x, int y)
    //그때 이야기하기로 하나만 지우는것보단 다 지우는게 낫다고 한 것 같아서 이건 삭제해도 무방함.
    {
        if (x < 0 || y < 0 || x >= grid.GetWidth() || y >= grid.GetHeight())
        {
            return;
        }
        if (visualGridArray[x, y] != null)
        {
            Destroy(visualGridArray[x, y]);
        }
        DeleteTileVisual(x, y);
    }

    //인게임 UI로 모든 배치된 분열기 제거.
    public void DeleteAllTileVisual()
    {
        bool isEmpty = true;
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                if (visualGridArray[x, y] != null)
                {
                    Destroy(visualGridArray[x, y]);
                    isEmpty = false;
                }
            }
        }

        if (!isEmpty)
        {
            PoolManager.Instance.ConsumeSlime(eraseGridCost);
        }
    }

    //특정 위치에 타일 비주얼 생성
    private void CreateTileVisual(int x, int y)
    //분열기 프리팹은 만드는거 상관 없어도 넣을땐 직사각형으로 한정해야할듯.
    //지금의 내 실력으론 직사각형 모양이 한계..
    //프리팹을 GridView 객체에 넣고 직사각형 가로세로 사이즈 정해주면 그 크기만큼 그리드 좌표에 생성해주는 구조.
    //이건 말로 설명하는게 이해하기 쉬울듯.
    {
        int tileId = grid.GetValue(x, y); //그리드 좌표 x,y에 넣을 타일 ID 받아오기

        if (tileId <= 0 || tileId >= tileObjects.Count) return; //타일 ID가 현재 타일 종류 갯수보다 많으면 리턴

        currentTileSize = tileObjects[tileId].tileSize; //현재 타일 크기

        if (!IsPlacementValid(x, y, currentTileSize)) return;

        GameObject tileObject = Instantiate(tileObjects[tileId].tileObject, this.transform);//프리팹의 객체 복사
        tileObject.transform.position = new Vector3(grid.GetWorldPosition(x, y).x, grid.GetWorldPosition(x, y).y, -1); //위치 설정
        tileObject.transform.localScale *= grid.GetCellSize(); //크기(그리드 셀 크기만큼 조정하기)
        visualGridArray[x, y] = tileObject; //해당 그리드 배열(좌표)에 오브젝트 생성.


        for (int indX = -(currentTileSize.x / 2); indX < currentTileSize.x / 2 + 1; indX++)
        {
            //타일 크기만큼 오브젝트 생성
            for (int indY = -(currentTileSize.y / 2); indY < currentTileSize.y / 2 + 1; indY++)
            {
                if (indX == 0 && indY == 0 || visualGridArray[x + indX, y + indY] != null)
                {
                    continue;
                }

                GameObject clearObj = new GameObject();
                clearObj.transform.parent = tileObject.transform;
                visualGridArray[x + indX, y + indY] = clearObj;
            }
        }
    }


    private bool IsPlacementValid(int x, int y, Vector2Int tileSize)
    {
        if (x < tileSize.x / 2 || y < tileSize.y / 2 || x + tileSize.x / 2 + 1 > width || y + tileSize.y / 2 + 1 > height)
        {
            return false;
        }

        for (int idx = -(tileSize.x / 2); idx < tileSize.x / 2 + 1; idx++)
        {
            for (int idy = -(tileSize.y / 2); idy < tileSize.y / 2 + 1; idy++)
            {
                if (visualGridArray[x + idx, y + idy] != null) return false;
            }
        }

        return true;
    }

    private void DeleteTileVisual(int x, int y)
    {
        int tileId = grid.GetValue(x, y);
        if (tileId <= 0 || tileId >= tileObjects.Count) return;
        if (!IsPlacementValid(x, y, currentTileSize)) return;
        for (int indX = -tileObjects[tileId].tileSize.x / 2; indX < tileObjects[tileId].tileSize.x / 2 + 1; indX++)
        {
            for (int indY = -tileObjects[tileId].tileSize.y / 2; indY < tileObjects[tileId].tileSize.y / 2 + 1; indY++)
            {
                if (visualGridArray[x + indX, y + indY] == null) return;
                else Destroy(visualGridArray[x + indX, y + indY]);
            }
        }
    }

    

}
