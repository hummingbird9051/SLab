using UnityEngine;

public interface IAbsorbable // 간단한 인터페이스 구현으로 컴포넌트 구분 하기위한 장치
{
    int DivideConcentration { get; set; }
    void BeAbsorbed(Transform target);

    void BeSpitOut(Vector2 pow);
}
