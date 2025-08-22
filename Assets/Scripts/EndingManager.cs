using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void OnEnable()
    {
        KingSlimeSpawner.Instance.Ending += EndGame;
    }


    private void EndGame()
    {
        target.SetActive(true);
    }
}
