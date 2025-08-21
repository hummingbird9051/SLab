using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeCanvasController : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float fadeDuration;

    void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void GameStart()
    {
        StartFadeOutAndLoadScene("InGameScene");

        foreach (Transform child in transform)
        {
            if (child.name != "Fade")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void StartFadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(DoFade(0, 1, true, "InGameScene"));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DoFade(1, 0, false));
    }

    private IEnumerator DoFade(float startValue, float endValue, bool loadAfterFade, string sceneName = "")
    {
        float timeElapsed = 0f;
        canvasGroup.alpha = startValue;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startValue, endValue, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endValue;

        // ÆäÀÌµå ¾Æ¿ô ÈÄ ¾ÀÀ» ·Îµå
        if (loadAfterFade && !string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
