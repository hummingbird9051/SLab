using UnityEngine;

public class GroundSlime : Slime
{
    private Vector3 dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        SetRandomDir();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir;
    }

    private void SetRandomDir()
    {
        dir = new Vector3(Random.Range(0.01f, -0.01f), Random.Range(0.01f, -0.01f), 0);
    }
}
