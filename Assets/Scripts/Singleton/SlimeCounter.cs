using UnityEngine;

public class SlimeCounter : SingletonBase<SlimeCounter>
{
    private int _slimeCount = 0;

    public int SlimeCount => _slimeCount;
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Slime.OnSlimeEnabled += CountUp;
    }

    private void OnDisable()
    {
        Slime.OnSlimeEnabled -= CountUp;
    }

    private void CountUp()
    {
        _slimeCount++;
        Debug.Log("슬라임 한개 추가");
    }

    public void ResetSlimeCount()
    {
        _slimeCount = 0;
    }
}
