using UnityEngine;

public interface IAbsorbable
{
    void BeAbsorbed(Transform target);

    void BeSpitOut(Vector2 pow);
}
