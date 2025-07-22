using UnityEngine;
using System;

public class GridSystem<TGridObject> : IGrid<TGridObject> // 그리드 시스템으로 분열기 배치
{
    private int width;
    private int height;
    private int xCellCount;
    private int yCellCount;
    private TGridObject[,] gridArray; //,를 중간에 둔 변수 두개를 가지고 이차원 gridObject 배열 생성 
    private Vector3 originPos;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize, Vector3 originalPos,
        Func<GridSystem<TGridObject>, int, int, TGridObject> createGridObject)
    //Func 제네릭은 Func<A, B, C, D>라고 가정했을 때 A형 변수, B형 변수, C형 변수를 넣으면 D형 변수를
    //리턴한다의 구조
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.xCellCount = (int)(width / cellSize);
        this.yCellCount = (int)(height / cellSize);
        this.originPos = originalPos;
        gridArray = new TGridObject[xCellCount, yCellCount];

        for (int x = 0; x < xCellCount; x++)
        {
            for (int y = 0; y < yCellCount; y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);//초반에 null값을 가지고 있는 GridObject 이차원 배열 생성
            }
        }
    }


    public int GetWidth() => xCellCount;
    public int GetHeight() => yCellCount;
    public float GetCellSize() => cellSize;

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos + new Vector3(cellSize, cellSize) * 0.5f;
    }

    public void GetXY(Vector3 worldPos, out int x, out int y)//월드 위치를 그리드 좌표(?)로 변환
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        //grid 좌표 안의 값 지정(컴퓨터에 저장되는. 실제로 눈에 보이진 않음.)
        if (x >= 0 && y >= 0 && x < xCellCount && y < yCellCount)
        {
            gridArray[x, y] = value;
        }
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < xCellCount && y < yCellCount)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
}
