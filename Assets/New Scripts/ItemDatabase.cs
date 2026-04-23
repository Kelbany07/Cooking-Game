using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Bun,
    PattyRaw,
    PattyCooked,
    PattyBurnt,
    BurgerComplete
}

public enum PattyState
{
    Raw,
    Cooking,
    Cooked,
    Burnt
}

[System.Serializable]
public class ItemEntry
{
    public ItemType type;
    public GameObject prefab;
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    public ItemEntry[] items;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetPrefab(ItemType type)
    {
        foreach (var e in items)
        {
            if (e.type == type) return e.prefab;
        }
        return null;
    }
}
