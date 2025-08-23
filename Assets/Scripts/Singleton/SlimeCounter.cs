using UnityEngine;

public class SlimeCounter : SingletonBase<SlimeCounter>
//슬라임 카운터 - 슬라임 객체가 enable되면 countUp, disable되면 countDown
//SlimeCount라는 컴포넌트를 외부에서 접근하여 GameDataManager에서 슬라임 500개가 넘으면 다음 슬라임으로 갈 수 있게

{
    private int _slimeCount = 0;

    public int SlimeCount
    {
        get
        {
            return _slimeCount;
        }
        set
        {
            _slimeCount = value;
        }
    }
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
    }

    private void CountDown()
    {
        _slimeCount--;
    }

    public void ResetSlimeCount()
    {
        _slimeCount = 0;
    }
}
