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
        Slime.OnSlimeDisabled += CountDown;
    }

    

    private void OnDisable()
    {
        Slime.OnSlimeEnabled -= CountUp;
        Slime.OnSlimeDisabled -= CountDown;
    }

    private void CountUp()
    {
        _slimeCount++;
        Debug.Log("슬라임 한개 추가");
    }

    private void CountDown()
    {
        _slimeCount--;
        Debug.Log("슬라임 하나 제거");
    }

    public void ResetSlimeCount()
    {
        _slimeCount = 0;
    }
}
