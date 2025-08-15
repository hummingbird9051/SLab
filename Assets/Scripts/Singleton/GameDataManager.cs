using System;
using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
//슬라임 카운터에서 슬라임 갯수 정보를 넘겨받아 슬라임 500개가 넘으면 초기화 후 다음 단계 슬라임 생성.
{
    [SerializeField] private int _slimeNum = 1;
    [SerializeField] private int _slimeLevel;
    [SerializeField] private int _slimeConcentration = 2;

    private const string SLIME_NUM = "slimeNum";
    private const string SLIME_LEVEL = "slimeLevel";
    private const string SLIME_CONCENTRATION = "slimeConcentration";


    protected override void Awake()
    {
        base.Awake();
        LoadSlimeLevel();
        LoadSlimeNum();
        LoadSlimeConcentration();
        Debug.Log(_slimeLevel);
        Debug.Log(_slimeNum);
        Debug.Log(_slimeConcentration);
    }

    void Update()
    {
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

    void OnApplicationQuit()
    {
        SaveSlimeLevel();
        SaveSlimeNum();
        SaveSlimeConcentration();
    }
}
