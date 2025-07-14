using UnityEngine;

public interface IGrid<TGridObject>
{
    int GetWidth();
    int GetHeight();
    float GetCellSize();

    Vector3 GetWorldPosition(int x, int y);

    void GetXY(Vector3 worldPosition, out int x, out int y);

    void SetValue(int x, int y, TGridObject value);
    TGridObject GetValue(int x, int y);
}
