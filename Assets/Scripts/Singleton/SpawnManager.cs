using UnityEngine;

public class SpawnManager : SingletonBase<SpawnManager>
{
    public string[] tag;
    private int currentSpawnNum; // 0:ÀÏ¹Ý, 1:±ÍÁ·, 2:Å·

    void Start()
    {
        currentSpawnNum = 0;
    }
    protected override void Awake()
    {
        base.Awake();
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
            PoolManager.Instance.SpawnFromPool("MachineSlime", pos);
        }
        else if (MachineOrGround == "Ground")
        {
            PoolManager.Instance.SpawnFromPool(tag[currentSpawnNum], pos);
        }
    }

    public void UpgradeSlime()
    {
        PoolManager.Instance.RefreshPool();

        if (currentSpawnNum >= tag.Length) return;

        currentSpawnNum += 1;
    }

}
