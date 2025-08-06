using UnityEngine;

public class GameExit : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("게임을 종료합니다.");
            Application.Quit();
        }
    }
}
