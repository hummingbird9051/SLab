using System;
using System.Collections.Generic;
using UnityEngine;

public class KingSlimeSpawner : SingletonBase<KingSlimeSpawner>
{
    [SerializeField] private List<GameObject> kingSlimePrefab;
    public event Action Ending;
    private int kingIndex;

    protected void Awake()
    {
        base.Awake();
        kingIndex = GameDataManager.Instance.GetKingNum();
    }

    void Start()
    {
        for (int i = 0; i < kingIndex; i++)
        {
            Instantiate(kingSlimePrefab[i], this.transform);
            SlimeCounter.Instance.SlimeCount -= 1;
        }
    }

    public void SpawnKingSlime()
    {
        Instantiate(kingSlimePrefab[kingIndex], this.transform);
        SlimeCounter.Instance.SlimeCount -= 1;
        kingIndex++;
        if (kingIndex == 1)
        {
            Ending?.Invoke();
        }
    }

    public int GetKingIndex() => kingIndex;

    public void SetKingIndex(int index)
    {
        kingIndex = index;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
