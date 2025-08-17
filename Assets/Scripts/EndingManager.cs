using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void Update()
    {
        if (SpawnManager.Instance.GetCurrentSlimeLevel() >= 2)
        {
            target.SetActive(true);
        }
    }
}
