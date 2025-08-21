using UnityEngine;

public class Starter : MonoBehaviour
{
    public Transform firstPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int slimeNum = GameDataManager.Instance.GetSlimeNum();
        for (int i = 0; i < slimeNum; i++)
        {
            SpawnManager.Instance.SpawnSlime(
                "Ground",
                new Vector3(Random.Range(firstPos.position.x + 0.5f, firstPos.position.x - 0.5f),
                    Random.Range(firstPos.position.y + 0.5f, firstPos.position.y - 0.5f),
                    firstPos.position.z
                    )
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
