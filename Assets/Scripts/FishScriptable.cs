using UnityEngine;

[CreateAssetMenu(fileName="Fish", menuName="ScriptableObjects/Fish", order=1)]
public class FishScriptable : ScriptableObject
{
    public string description;
    [Range(1, 5)]
    [Tooltip("Higher value is higher rarity (rarity counted in stars)")]
    public int rarity;
    [Range(1, 100)]
    [Tooltip("The higher the value, the higher the chance to spawn")]
    public int spawnChance;

    public Sprite sprite;
    public Sprite shadow;
    public Mesh fishMesh;
}
