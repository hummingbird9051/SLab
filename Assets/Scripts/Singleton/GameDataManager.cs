using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
//슬라임 카운터에서 슬라임 갯수 정보를 넘겨받아 슬라임 500개가 넘으면 초기화 후 다음 단계 슬라임 생성.
{
    [SerializeField] private int _slimeNum = 1;
    [SerializeField] private int _slimeLevel;
    [SerializeField] private int _slimeConcentration = 8;
    private int _kingNum = 0;
    private List<int> _gridTileId;
    private List<int> _x;
    private List<int> _y;

    public event Action<int, int, int> GridInitializer; 

    private const string SLIME_NUM = "slimeNum";
    private const string SLIME_LEVEL = "slimeLevel";
    private const string SLIME_CONCENTRATION = "slimeConcentration";
    private const string KING_SLIME_NUM = "kingSlimeNum";
    private const string GRID_TILE_ID = "gridTileId";
    private const string GRID_X = "gridX";
    private const string GRID_Y = "gridY";


    protected override void Awake()
    {
        base.Awake();
        LoadSlimeLevel();
        LoadSlimeNum();
        LoadSlimeConcentration();
        LoadKingNum();
        Debug.Log(_slimeLevel);
        Debug.Log(_slimeNum);
        Debug.Log(_slimeConcentration);
        Debug.Log(_kingNum);
    }

    void Start()
    {
        LoadGrid();
    }

    //slimeNum -----------------------------

    public void SaveSlimeNum()
    {
        _slimeNum = SlimeCounter.Instance.SlimeCount;
        PlayerPrefs.SetInt(SLIME_NUM, _slimeNum);
        PlayerPrefs.Save();
    }

    public void LoadSlimeNum()
    {
        _slimeNum = PlayerPrefs.GetInt(SLIME_NUM, 1) > 1 ? PlayerPrefs.GetInt(SLIME_NUM, 1) : 1;
    }

    public int GetSlimeNum()
    {
        return _slimeNum;
    }

    //slimeLevel---------------------------------------

    public void SaveSlimeLevel()
    {
        _slimeLevel = SpawnManager.Instance.GetCurrentSlimeLevel();
        PlayerPrefs.SetInt(SLIME_LEVEL, _slimeLevel);
        PlayerPrefs.Save();
    }

    public void LoadSlimeLevel()
    {
        _slimeLevel = PlayerPrefs.GetInt(SLIME_LEVEL, 0);
    }

    public int GetSlimeLevel() => _slimeLevel;

    //slimeConcentration----------------------------------------
    public void SaveSlimeConcentration()
    {
        _slimeConcentration = SpawnManager.Instance.GetCurrentSlimeConcentration();
        PlayerPrefs.SetInt(SLIME_CONCENTRATION, _slimeConcentration);
        PlayerPrefs.Save();
    }

    public void LoadSlimeConcentration()
    {
        _slimeConcentration = PlayerPrefs.GetInt(SLIME_CONCENTRATION, 2);
    }

    public int GetSlimeConcentration() => _slimeConcentration;

    //kingNum----------------------------------------------------

    public int GetKingNum() => _kingNum;

    public void SaveKingNum()
    {
        _kingNum = KingSlimeSpawner.Instance.GetKingIndex();
        PlayerPrefs.SetInt(KING_SLIME_NUM, _kingNum);
        PlayerPrefs.Save();
    }

    public void LoadKingNum()
    {
        _kingNum = PlayerPrefs.GetInt(KING_SLIME_NUM, 0);
    }

    //GridLoad--------------------------------------

    public void LoadGrid()
    {
        string loadedTileString = PlayerPrefs.GetString(GRID_TILE_ID);
        string loadedXString = PlayerPrefs.GetString(GRID_X);
        string loadedYString = PlayerPrefs.GetString(GRID_Y);
        string[] tileIds = loadedTileString.Split(',');
        string[] XArray = loadedXString.Split(',');
        string[] YArray = loadedYString.Split(',');
        foreach (var s in tileIds)
        {
            Debug.Log(s + "tileId");
        }

        foreach (var s in XArray)
        {
            Debug.Log(s + "x");
        }
        foreach (var s in YArray)
        {
            Debug.Log(s + "y");
        }
        _gridTileId = new List<int>();
        _x = new List<int>();
        _y = new List<int>();
        int tempTileId;
        int tempX;
        int tempY;
        for (int i = 0; i < tileIds.Length; i++)
        {
            if (int.TryParse(tileIds[i], out tempTileId))
                _gridTileId.Add(tempTileId);
            if (int.TryParse(XArray[i], out tempX))
                _x.Add(tempX);
            if (int.TryParse(YArray[i], out tempY))
                _y.Add(tempY);
        }

        for (int i = 0; i < tileIds.Length; i++)
        {
            GridInitializer?.Invoke(_gridTileId[i], _x[i], _y[i]);
        }
    }

    public void AddGridInfo(int tileId, int x, int y)
    {
        _gridTileId.Add(tileId);
        _x.Add(x);
        _y.Add(y);
        Debug.Log(_x + ", " + _y + ", " + _gridTileId);
    }

    public void DeleteAllGridInfo()
    {
        _gridTileId = new List<int>();
        _x = new List<int>();
        _y = new List<int>();
    }

    public void SaveGridInfo()
    {
        string gridTileString = string.Join(",", _gridTileId);
        string gridXString = string.Join(",", _x);
        string gridYString = string.Join(",", _y);
        Debug.Log(gridTileString + "tile, " + gridXString + "x, " + gridYString + "y");
        PlayerPrefs.SetString(GRID_TILE_ID, gridTileString);
        PlayerPrefs.SetString(GRID_X, gridXString);
        PlayerPrefs.SetString(GRID_Y, gridYString);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        SaveSlimeLevel();
        SaveSlimeNum();
        SaveSlimeConcentration();
        SaveGridInfo();
        SaveKingNum();
    }
}
