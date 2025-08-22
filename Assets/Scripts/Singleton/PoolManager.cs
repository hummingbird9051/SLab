using System;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PoolManager : SingletonBase<PoolManager>
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private GameObject parentObject;

    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDict;

    public event Action NotEnoughSlimes;

    protected override void Awake()
    {
        base.Awake();
        InitializePools();
    }

    public void InitializePools()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(parentObject.transform);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objects);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, int concentration)
    {
        //해당 키 없으면 반환
        if (!poolDict.ContainsKey(tag))
        {
            return null;
        }

        for (int i = 0; i < poolDict[tag].Count; i++)
        {
            //풀에서 하나 뽑아서
            GameObject objectFromPool = poolDict[tag].Dequeue();

            if (objectFromPool.activeSelf == false)
            {
                //활성화하고
                objectFromPool.SetActive(true);
                objectFromPool.transform.position = position;
                IAbsorbable absorbable = objectFromPool.GetComponent<IAbsorbable>();
                if (absorbable != null)
                    absorbable.DivideConcentration = concentration;

                //다시 큐에 넣음(재사용)
                poolDict[tag].Enqueue(objectFromPool);
                return objectFromPool;
            }
            else
            {
                poolDict[tag].Enqueue(objectFromPool);
            }
        }

        return null;
    }

    public void DeSpawnToPool(GameObject obj) {
        obj.SetActive(false);
    }

    public bool ConsumeSlime(int num)
    {
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform childTransform in parentObject.transform)
        {
            if (!childTransform.gameObject.name.Contains("KS"))
               if(childTransform.gameObject.activeSelf)
                    childList.Add(childTransform.gameObject);
        }

        if (childList.Count <= num)
        {
            // 팝업
            Debug.Log("슬라임 수 부족");
            NotEnoughSlimes?.Invoke();
            return false;
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                childList[i].SetActive(false);
            }
            return true;
        }
    }

    public void RefreshPool()
    { 
        List<Transform> childList = new List<Transform>();

        foreach (Transform child in parentObject.transform) 
        {
            childList.Add(child);
        }

        for (int i = parentObject.transform.childCount - 1; i >= 0; i--)
        {
            DeSpawnToPool(childList[i].gameObject);
        }
    }
}
