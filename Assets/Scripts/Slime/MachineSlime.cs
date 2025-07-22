using UnityEngine;

public class MachineSlime : Slime, IAbsorbable //머신에 들어갈 수 있는 슬라임들은 Absorbable 인터페이스를 가지고 있음.
{
    public void BeAbsorbed(Transform target) { }
    public void BeSpitOut(Vector2 pow) { }
}
