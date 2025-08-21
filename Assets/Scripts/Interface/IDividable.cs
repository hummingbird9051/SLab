using System.Threading.Tasks;
using UnityEngine;

public interface IDividable
{
    public bool IsDivided { get; }
    void BeDivide();
    Task BeUndivide();
}
