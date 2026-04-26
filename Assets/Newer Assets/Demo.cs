using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{
    [SerializeField] private Timer timer1;
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private string mainSceneName = "Main Game";

    private GameObject[] gameOverRoots;
    private bool gameOverPreloaded;

    private IEnumerator Start()
    {
        // Preload the GameOver scene additively, then hide its root objects
        var preloadOp = SceneManager.LoadSceneAsync(gameOverSceneName, LoadSceneMode.Additive);
        if (preloadOp != null)
        {
            yield return preloadOp;
            var scene = SceneManager.GetSceneByName(gameOverSceneName);
            if (scene.isLoaded)
            {
                gameOverRoots = scene.GetRootGameObjects();
                foreach (var go in gameOverRoots) go.SetActive(false);
                gameOverPreloaded = true;
            }
        }

        // Start timer and show GameOver when it ends.
        // Use a coroutine to ensure we can set the active scene before unloading main.
        timer1
            .SetDuration(3)
            .OnEnd(() => StartCoroutine(ShowGameOverCoroutine()))
            .Begin();
    }

    private IEnumerator ShowGameOverCoroutine()
    {
        // Ensure the GameOver scene is loaded
        var gameOverScene = SceneManager.GetSceneByName(gameOverSceneName);
        if (!gameOverScene.isLoaded)
        {
            var loadOp = SceneManager.LoadSceneAsync(gameOverSceneName, LoadSceneMode.Additive);
            if (loadOp != null) yield return loadOp;
            gameOverScene = SceneManager.GetSceneByName(gameOverSceneName);
            if (gameOverScene.isLoaded)
            {
                gameOverRoots = gameOverScene.GetRootGameObjects();
            }
        }

        // Make GameOver the active scene so unloading main won't break references immediately
        if (gameOverScene.isLoaded)
        {
            SceneManager.SetActiveScene(gameOverScene);

            // Activate GameOver root objects (show UI)
            if (gameOverRoots != null)
            {
                foreach (var go in gameOverRoots) go.SetActive(true);
            }
        }

        // Unload the main game scene
        var mainScene = SceneManager.GetSceneByName(mainSceneName);
        if (mainScene.IsValid() && mainScene.isLoaded)
        {
            // Unload asynchronously and wait a frame (no need to block long)
            SceneManager.UnloadSceneAsync(mainScene);
        }

        yield break;
    }

    // Public helper to hide/unload GameOver if needed
    public void HideGameOverAndReloadMain()
    {
        // Load main scene (single) — this automatically unloads additively loaded scenes like GameOver.
        SceneManager.LoadScene(mainSceneName, LoadSceneMode.Single);
    }
}
