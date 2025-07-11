using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonBase<PoolManager>
{
    // 프리팹 보관 변수, 풀 담당을 하는 리스트
    public GameObject[] prefabs;
    List<GameObject>[] pools;
    private void Awake()
    {
        base.Awake();
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach (GameObject item in pools[index]) {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select) {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
