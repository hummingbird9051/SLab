using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

public class GridView : MonoBehaviour
{
    private IGrid<int> grid;
    private GameObject[,] visualGridArray;
    private int width;
    private int height;
    private int prevX = -1;
    private int prevY = -1;
    private Vector2Int currentTileSize;

    //클래스를 만들어 인스펙터 창에 보여줄 수 있게 만들수 있는 코드
    [System.Serializable]
    public class TileObject
    {
        public GameObject tileObject;
        public Vector2Int tileSize;
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
        if (x < 0 || y < 0 || x >= grid.GetWidth() || y >= grid.GetHeight() || !CheckBound(x, y, currentTileSize))
        {
            return;
        }

        for (int indX = 0; indX < currentTileSize.x; indX++)
        {
            for (int indY = 0; indY < currentTileSize.y; indY++)
            {
                if (visualGridArray[x + indX, y + indY] != null)
                {
                    return;
                }
            }
        }

        SpriteRenderer renderer = visualGridArray[x, y] == null ? null : visualGridArray[x, y].GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            if (prevX != -1 || prevY != -1)
            {
                DeleteTileVisual(prevX, prevY);
            }

            CreateTileVisual(x, y);
            prevX = x;
            prevY = y;
        }
    }

    //특정 칸 비주얼 업데이트
    public void UpdateTileVisual(int x, int y)
    {
        if (x < 0 || y < 0 || x >= grid.GetWidth() || y >= grid.GetHeight())
        {
            return;
        }
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
    //분열기 프리팹은 만드는거 상관 없어도 넣을땐 직사각형으로 한정해야할듯.
    //지금의 내 실력으론 직사각형 모양이 한계..
    //프리팹을 GridView 객체에 넣고 직사각형 가로세로 사이즈 정해주면 그 크기만큼 그리드 좌표에 생성해주는 구조.
    //이건 말로 설명하는게 이해하기 쉬울듯.
    {
        int tileId = grid.GetValue(x, y); //그리드 좌표 x,y에 넣을 타일 ID 받아오기

        if (tileId <= 0 || tileId >= tileObjects.Count) return; //타일 ID가 현재 타일 갯수보다 많으면 리턴

        GameObject tileObject = Instantiate(tileObjects[tileId].tileObject, this.transform);//프리팹의 객체 복사
        tileObject.transform.position = new Vector3(grid.GetWorldPosition(x, y).x, grid.GetWorldPosition(x, y).y, -1); //위치 설정
        tileObject.transform.localScale *= grid.GetCellSize(); //크기(그리드 셀 크기만큼 조정하기)
        visualGridArray[x, y] = tileObject; //해당 그리드 배열(좌표)에 오브젝트 생성.
        currentTileSize = tileObjects[tileId].tileSize; //현재 타일 크기

        if(!CheckBound(x, y, currentTileSize)) return;

        for (int indX = 0; indX < tileObjects[tileId].tileSize.x; indX++)  
            //타일 크기만큼 각 좌표에 오브젝트 생성
        {
            for (int indY = 0; indY < tileObjects[tileId].tileSize.y; indY++)
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


    private void DeleteTileVisual(int x, int y)
    {
        int tileId = grid.GetValue(x, y);
        if (tileId <= 0 || tileId >= tileObjects.Count) return;
        for (int indX = 0; indX < tileObjects[tileId].tileSize.x; indX++)
        {
            for (int indY = 0; indY < tileObjects[tileId].tileSize.y; indY++)
            {
                if (visualGridArray[x + indX, y + indY] == null) return;
                else Destroy(visualGridArray[x + indX, y + indY]);
            }
        }
    }

    private bool CheckBound(int x, int y, Vector2Int tileSize)
    {
        if(x + tileSize.x > width) return false;
        else if(y + tileSize.y > height) return false;
        return true;
    }

}
