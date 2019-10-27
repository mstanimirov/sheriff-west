using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public event Action AfterSceneLoad;
    public event Action BeforeSceneUnload;

    public float fadeDuration = 1f;

    public CanvasGroup fadeCanvasGroup;

    #region Private Vars

    private bool isFading;

    #endregion

    public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;

    private IEnumerator Start()
    {

        yield return StartCoroutine(FadeAndSwitchScenes(Constants.startingSceneName));
        StartCoroutine(Fade(0));

    }

    public void FadeAndLoadScene(string sceneName)
    {

        if (!isFading)
            StartCoroutine(FadeAndSwitchScenes(sceneName));

    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {

        yield return StartCoroutine(Fade(1f));

        BeforeSceneUnload?.Invoke();

        if (SceneManager.GetActiveScene().name != Constants.persistentSceneName)
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        AfterSceneLoad?.Invoke();

        yield return StartCoroutine(Fade(0f));

    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);

    }

    private IEnumerator Fade(float finalAlpha)
    {

        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, finalAlpha))
        {

            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;

        }

        isFading = false;
        fadeCanvasGroup.blocksRaycasts = false;

    }

}
