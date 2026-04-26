csharp Assets/Scripts/MixingBowl.cs
using System.Collections.Generic;
using UnityEngine;

public class MixingBowl : MonoBehaviour
{
    [Tooltip("List the required ingredient names for this recipe (order doesn't matter).")]
    public List<string> requiredIngredients = new List<string>();

    [Tooltip("Prefab produced when required ingredients are present.")]
    public GameObject resultPrefab;

    [Tooltip("Optional offset where result should spawn (local space)")]
    public Vector3 resultOffset = Vector3.zero;

    // Ingredients currently placed in the bowl (instances)
    private readonly List<Ingredient> _currentIngredients = new List<Ingredient>();

    // Called by draggable objects when released over the bowl
    public void ReceiveIngredient(Ingredient ingredient)
    {
        if (ingredient == null) return;

        // Prevent double-adding same instance
        if (_currentIngredients.Contains(ingredient)) return;

        // Attach ingredient to bowl and lock its draggable behaviour
        ingredient.transform.SetParent(transform, true);
        ingredient.transform.position = transform.position; // center; adjust if you want different placement

        var dr = ingredient.GetComponent<MonoBehaviour>(); // used to disable DraggableReturn if present
        var draggable = ingredient.GetComponent<DraggableReturn>();
        if (draggable != null)
            draggable.enabled = false;

        // Ensure collider is trigger to avoid physics while inside bowl
        var col = ingredient.GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;

        _currentIngredients.Add(ingredient);

        TryCombine();
    }

    // Check if current ingredients satisfy the recipe; if so, create result and clear ingredients
    private void TryCombine()
    {
        if (requiredIngredients == null || requiredIngredients.Count == 0) return;

        // Create a mutable copy of required ingredients so we can remove matches
        List<string> needed = new List<string>(requiredIngredients);

        foreach (var ing in _currentIngredients)
        {
            if (ing == null) continue;
            // remove first occurrence of this ingredient name if present
            if (needed.Contains(ing.ingredientName))
                needed.Remove(ing.ingredientName);
        }

        // if nothing left, we have all required ingredients
        if (needed.Count == 0)
        {
            // spawn the result
            if (resultPrefab != null)
            {
                Instantiate(resultPrefab, transform.TransformPoint(resultOffset), Quaternion.identity);
            }

            // destroy ingredient instances and clear list
            foreach (var ing in _currentIngredients)
            {
                if (ing != null)
                    Destroy(ing.gameObject);
            }
            _currentIngredients.Clear();
        }
    }

    // Optional: let players remove ingredient by clicking inside bowl (not required)
    private void OnMouseDown()
    {
        // Example behavior could be implemented here
    }
}