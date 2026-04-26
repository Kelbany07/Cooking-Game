using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCCollector : MonoBehaviour
{
    [Tooltip("How many points each pancake is worth")]
    public int pointsPerPancake = 1;

    [Tooltip("Ingredient name accepted as a pancake (if using Ingredient)")]
    public string pancakeIngredientName = "pancake";

    [Tooltip("Pancake prefab to instantiate when respawning")]
    public GameObject pancakePrefab;

    [Tooltip("Seconds to wait before respawning the pancake")]
    public float respawnDelay = 2f;

    [Header("Respawn position settings")]
    [Tooltip("Optional Transform to use as the respawn origin. If null, the pancake's original position is used.")]
    public Transform respawnPoint;
    [Tooltip("Offset applied to the chosen respawn origin (respawnPoint or original pancake position).")]
    public Vector3 respawnOffset = Vector3.zero;
    [Tooltip("If > 0, respawn position will be randomized inside this radius around the respawn origin.")]
    public float respawnRadius = 0f;

    [Header("Audio")]
    [Tooltip("Sound played when this NPC collects an accepted pancake")]
    public AudioClip collectClip;
    [Range(0f, 1f)]
    [Tooltip("Volume used for the collect sound")]
    public float collectVolume = 1f;

    [Header("Animation")]
    [Tooltip("Animator controlling this NPC's animations (assign the NPC's Animator)")]
    public Animator animator;
    [Tooltip("Trigger parameter name on the Animator to play the 'Get' animation")]
    public string getTriggerName = "Get";
    [Tooltip("Fallback legacy Animation clip name to play if Animator is not used")]
    public string legacyGetClipName = "Get";

    // Ensure NPC collider is set to isTrigger = true in the inspector.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (other.CompareTag("Pancake"))
        {
            Collect(other.gameObject);
            return;
        }

        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.ingredientName == pancakeIngredientName)
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject pancake)
    {
        if (pancake == null) return;

        // Add to global score
        ScoreManager.Instance?.AddScore(pointsPerPancake);

        // Reset/restart the game timer when a pancake is given to the NPC
        var timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.Restart();
        }

        // Play collect sound (one-shot at NPC position)
        if (collectClip != null)
        {
            AudioSource.PlayClipAtPoint(collectClip, transform.position, collectVolume);
        }

        // Play the "Get" animation only when an accepted pancake is collected.
        if (animator != null)
        {
            animator.SetTrigger(getTriggerName);
        }
        else
        {
            var legacy = GetComponent<Animation>();
            if (legacy != null && !string.IsNullOrEmpty(legacyGetClipName))
            {
                legacy.Play(legacyGetClipName);
            }
        }

        // Determine respawn position/rotation
        Vector3 origin = respawnPoint != null ? respawnPoint.position : pancake.transform.position;
        Quaternion spawnRot = respawnPoint != null ? respawnPoint.rotation : pancake.transform.rotation;

        Vector3 spawnPos = origin + respawnOffset;

        if (respawnRadius > 0f)
        {
            // Randomize inside a circle on the X-Y plane
            Vector2 random = Random.insideUnitCircle * respawnRadius;
            spawnPos += new Vector3(random.x, random.y, 0f);
        }

        Destroy(pancake);

        if (pancakePrefab != null)
        {
            StartCoroutine(RespawnAfterDelay(spawnPos, spawnRot));
        }
        else
        {
            Debug.LogWarning("NPCCollector: pancakePrefab is not assigned; pancake will not respawn.");
        }
    }

    IEnumerator RespawnAfterDelay(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(pancakePrefab, position, rotation);
    }
}

internal class Ingredient
{
    internal string ingredientName;
}