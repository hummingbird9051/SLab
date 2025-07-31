using UnityEngine;

public class SpawnManager : SingletonBase<SpawnManager>
{
    public string[] groundPrefabTag;
    public string[] machinePrefabTag;
    private int currentSpawnNum; // 0:일반, 1:귀족, 2:킹

    protected override void Awake()
    {
        base.Awake();
        currentSpawnNum = GameDataManager.Instance.GetSlimeLevel();
    }

    void Update()
    {
        if (SlimeCounter.Instance.SlimeCount > 500)
        {
            UpgradeSlime();
        }
    }

    public void SpawnSlime(string MachineOrGround, Vector3 pos)
    {
        if (MachineOrGround == "Machine")
        {
            PoolManager.Instance.SpawnFromPool(machinePrefabTag[currentSpawnNum], pos);
        }
        else if (MachineOrGround == "Ground")
        {
            PoolManager.Instance.SpawnFromPool(groundPrefabTag[currentSpawnNum], pos);
        }
    }

    public void UpgradeSlime()
    {
        PoolManager.Instance.RefreshPool();

        if (currentSpawnNum >= groundPrefabTag.Length) return;

        currentSpawnNum += 1;
    }

    public int GetCurrentSlimeLevel()
    {
        return currentSpawnNum;
    }

    //나중에 무조건 지워야 하는 것.
    public void SetCurrentSlimeLevel(int level)
    {
        currentSpawnNum = level;
    }

}
