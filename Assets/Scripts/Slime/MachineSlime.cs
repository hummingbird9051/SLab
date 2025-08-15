using UnityEngine;

public class MachineSlime : Slime, IAbsorbable, IDividable //머신에 들어갈 수 있는 슬라임들은 Absorbable 인터페이스를 가지고 있음.
{
    private bool isDivided = false;

    public int DivideConcentration { get; set; }
    public bool IsDivided
    {
        get
        {
            return isDivided;
        }
    }

    //iDividable 인터페이스 구현

    public void BeDivide()
    {
        isDivided = true;
    }

    public void BeUndivide()
    {
        isDivided = false;
    }


    public void BeAbsorbed(Transform target) { }
    public void BeSpitOut(Vector2 pow) { }
}
