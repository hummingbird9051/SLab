using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var grid = new GridSystem<PathNode>(10, 10, 2.0f, Vector3.zero,
            (GridSystem<PathNode> g, int x, int y) => new PathNode(g, x, y));

        var gridVisualizer = new GridVisualizer<PathNode>(grid);

        gridVisualizer.DrawLines();
    }
}
