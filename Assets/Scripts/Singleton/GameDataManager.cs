using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
//슬라임 카운터에서 슬라임 갯수 정보를 넘겨받아 슬라임 500개가 넘으면 초기화 후 다음 단계 슬라임 생성.
{
    [SerializeField] private int _slimeNum;
    [SerializeField] private int _slimeLevel;

    private const string SLIME_NUM = "slimeNum";
    private const string SLIME_LEVEL = "slimeLevel";


    protected override void Awake()
    {
        base.Awake();
        LoadSlimeLevel();
        LoadSlimeNum();
        Debug.Log(_slimeLevel);
        Debug.Log(_slimeNum);
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
        _slimeNum = PlayerPrefs.GetInt(SLIME_NUM, 0);
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

    public int GetSlimeLevel()
    {
        return _slimeLevel;
    }

    void OnApplicationQuit()
    {
        SaveSlimeLevel();
        SaveSlimeNum();
    }
}
