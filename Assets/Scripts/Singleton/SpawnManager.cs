using UnityEngine;

public class SpawnManager : SingletonBase<SpawnManager>
{
    public string[] groundPrefabTag;
    public string[] machinePrefabTag;
    private int currentSpawnNum; // 0:일반, 1:귀족, 2:킹
    private int currentSlimeConcentration;
    [SerializeField] int ConcentrationUpgradeCost;

    [SerializeField] private int[] _levelNum;

    public int LevelNum
    {
        get => _levelNum[currentSpawnNum];
        //set => _levelNum = value;
    }

    protected override void Awake()
    {
        base.Awake();
        currentSpawnNum = GameDataManager.Instance.GetSlimeLevel();
        currentSlimeConcentration = GameDataManager.Instance.GetSlimeConcentration();
    }

    void Update()
    {
        if (SlimeCounter.Instance.SlimeCount >= LevelNum)
        {
            UpgradeSlime();
        }
    }

    public GameObject SpawnSlime(string MachineOrGround, Vector3 pos)
    {
        if (MachineOrGround == "Machine")
        {
            return PoolManager.Instance.SpawnFromPool(machinePrefabTag[currentSpawnNum], pos, currentSlimeConcentration);
        }
        else if (MachineOrGround == "Ground")
        {
            return PoolManager.Instance.SpawnFromPool(groundPrefabTag[currentSpawnNum], pos, currentSlimeConcentration);
        }
        return null;
    }

    public GameObject SpawnSlime(string MachineOrGround, Vector3 pos, int concentration)
    {
        if (MachineOrGround == "Machine")
        {
            return PoolManager.Instance.SpawnFromPool(machinePrefabTag[currentSpawnNum], pos, concentration);
        }
        else if (MachineOrGround == "Ground")
        {
            return PoolManager.Instance.SpawnFromPool(groundPrefabTag[currentSpawnNum], pos, concentration);
        }

        return null;
    }

    public void UpgradeSlime()
    {
        PoolManager.Instance.RefreshPool();
        currentSpawnNum += 1;
        if (currentSpawnNum >= groundPrefabTag.Length)
        {
            KingSlimeSpawner.Instance.SpawnKingSlime();
            Restart();
        }
        Debug.Log(currentSlimeConcentration);
        SpawnSlime("Ground", new Vector3(0, 0, -1));
    }

    public int GetCurrentSlimeLevel() => currentSpawnNum;

    public int GetCurrentSlimeConcentration() => currentSlimeConcentration;

    public void ConcentrationUpgrade()
    {
        if (PoolManager.Instance.ConsumeSlime(ConcentrationUpgradeCost))
        {
            currentSlimeConcentration *= 2;
        }
    }

    //나중에 무조건 지워야 하는 것.
    public void SetCurrentSlimeLevel(int level)
    {
        currentSpawnNum = level;
    }

    public void SetCurrentSlimeConcentration(int concentration)
    {
        currentSlimeConcentration = concentration;
    }

    public void Restart()
    {
        PoolManager.Instance.RefreshPool();
        SetCurrentSlimeLevel(0);
        SpawnSlime("Ground", new Vector3(0, 0, -1));
    }

}
