using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}