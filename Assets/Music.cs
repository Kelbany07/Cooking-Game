using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; }

    [Tooltip("Assign an MP3/AudioClip here (place file in Assets/Audio or similar).")]
    public AudioClip musicClip;

    [Tooltip("Scene name that represents the main gameplay (music restarts when this scene loads).")]
    public string mainGameSceneName = "Main Game";

    [Tooltip("Scene name that represents the main menu (music plays on menu).")]
    public string mainMenuSceneName = "Main Menu";

    [Tooltip("Scene name that stops the music when loaded.")]
    public string gameOverSceneName = "GameOver";

    [Range(0f, 1f)] public float volume = 1f;
    public bool loop = true;

    AudioSource _audio;
    bool _initialized;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _audio = GetComponent<AudioSource>();
            _audio.playOnAwake = false;
            _audio.loop = loop;
            _audio.volume = volume;
            _audio.spatialBlend = 0f; // 2D sound so distance doesn't cut it off

            if (musicClip != null)
                _audio.clip = musicClip;

            SceneManager.sceneLoaded += OnSceneLoaded;

            // Start playing if we're not already in GameOver
            var active = SceneManager.GetActiveScene().name;
            if (active != gameOverSceneName)
                StartPlaybackForScene(active);
        }
        else
        {
            Destroy(gameObject); // avoid duplicates
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartPlaybackForScene(scene.name);
    }

    void StartPlaybackForScene(string sceneName)
    {
        if (_audio == null) return;

        if (sceneName == gameOverSceneName)
        {
            _audio.Stop();
            return;
        }

        // Restart when main game starts
        if (sceneName == mainGameSceneName)
        {
            PlayFromStart();
            return;
        }

        // For main menu or other scenes, ensure music is playing (but don't restart)
        if (!_audio.isPlaying)
        {
            if (_audio.clip != null)
                _audio.Play();
        }
    }

    public void PlayFromStart()
    {
        if (_audio == null || _audio.clip == null) return;
        _audio.Stop();
        _audio.time = 0f;
        _audio.Play();
    }

    public void StopMusic()
    {
        _audio?.Stop();
    }

    // Optional helper to debug why music might stop
    public void EnsureInitialized()
    {
        if (!_initialized)
        {
            _initialized = true;
            // sanity: ensure clip assigned
            if (_audio.clip == null && musicClip != null)
                _audio.clip = musicClip;
        }
    }
}
