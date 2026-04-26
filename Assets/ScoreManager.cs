using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Tooltip("Optional UI Text to show the score")]
    public TMP_Text scoreText;

    public int Score { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        if (scoreText != null) scoreText.text = Score.ToString();
    }
}