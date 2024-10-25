using System;
using UnityEngine;

[CreateAssetMenu(fileName="Fish", menuName="ScriptableObjects/Fish", order=1)]
public class FishScriptable : ScriptableObject
{
    public new string name;
    public string description;
    [Range(1, 5)]
    [Tooltip("Higher value is higher rarity (rarity counted in stars)")]
    public int rarity;
    [Range(1, 100)]
    [Tooltip("The higher the value, the higher the chance to spawn")]
    public int spawnChance;
    [Tooltip("The minimum time a fish waits before moving again")]
    public float minWaitTime = 1f;
    [Tooltip("The maximum time a fish waits before moving again")]
    public float maxWaitTime = 2f;

    public Sprite sprite;
    public Sprite shadow;
    public Mesh fishMesh;
}
