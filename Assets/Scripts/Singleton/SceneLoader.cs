using UnityEngine.SceneManagement;

public class SceneLoader : SingletonBase<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
