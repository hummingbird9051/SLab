using System;
using System.Collections.Generic;
using UnityEngine;

public class GridShowingManager : SingletonBase<GridShowingManager>
{
    public event Action<int> SelectIndexAction;

    [Serializable]
    struct Criteria
    {
        public int level;
        public int count;
    }
    [SerializeField] private List<Criteria> criteria;
    private int index;

    protected void Awake()
    {
        base.Awake();
    }

    void OnEnable()
    {
        index = 0;
    }

    void Update()
    {
        if (index < criteria.Count)
        {
            if (criteria[index].level * SpawnManager.Instance.LevelNum + criteria[index].count
                <= SpawnManager.Instance.GetCurrentSlimeLevel() * SpawnManager.Instance.LevelNum + SlimeCounter.Instance.SlimeCount)
            {
                SelectIndexAction?.Invoke((index + 1) * 2 + 1);
                index += 1;
            }
        }
    }
}
