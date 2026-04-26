using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "Main Game";

    [Header("Audio")]
    [Tooltip("Sound played when the GameOver scene opens")]
    [SerializeField] private AudioClip gameOverClip;
    [Range(0f, 1f)]
    [SerializeField] private float gameOverVolume = 1f;

    private void Start()
    {
        if (gameOverClip != null)
        {
            Vector3 playPosition = Camera.main != null ? Camera.main.transform.position : Vector3.zero;
            AudioSource.PlayClipAtPoint(gameOverClip, playPosition, gameOverVolume);
        }
    }

    // Hook this to your Retry button's OnClick in the GameOver scene UI.
    public void OnRetryButtonPressed()
    {
        // Load the main game scene in Single mode which will unload the GameOver scene.
        SceneManager.LoadScene(mainSceneName, LoadSceneMode.Single);
    }
}
