using UnityEngine;

public class GameDataManager : SingletonBase<GameDataManager>
//슬라임 카운터에서 슬라임 갯수 정보를 넘겨받아 슬라임 500개가 넘으면 초기화 후 다음 단계 슬라임 생성.
{
    
    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
    }

}
