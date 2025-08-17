using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameStart()
    {
        SceneLoader.Instance.LoadTargetScene("InGameScene");
    }
}
