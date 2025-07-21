using UnityEngine;

public class MachineSlime : MonoBehaviour, IAbsorbable
{
    public void BeAbsorbed(Transform target) { }
    public void BeSpitOut(Vector2 pow) { }
}
