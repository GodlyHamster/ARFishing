using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour
{
    public static Pond Instance { get; private set; }

    [SerializeField]
    private List<FishScriptable> fishLibrary = new List<FishScriptable>();

    [SerializeField]
    private List<Fish> pondFishes = new List<Fish>();

    [SerializeField]
    private GameObject fishPrefab;

    [SerializeField]
    private bool debugPond;
    [SerializeField]
    private Bounds pondBounds = new Bounds();

    private DebugText fishPondDebug = new DebugText("null");

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            AddRandomFish();
        }

        if (debugPond)
        {
            fishPondDebug.text = "Pond:";
            for (int i = 0; i < pondFishes.Count; i++)
            {
                fishPondDebug.text += "\n\t" + pondFishes[i].name;
            }
            UIDebugManager.instance.AddDebug(fishPondDebug);
        }
    }

    private void Update()
    {
        foreach (var fish in pondFishes)
        {
            fish.Update();
        }
    }

    private void AddRandomFish()
    {
        FishScriptable fish = Instantiate(fishLibrary[Random.Range(0, fishLibrary.Count)]);
        GameObject fishObject = Instantiate(fishPrefab, transform.position, transform.rotation, transform);
        fishObject.transform.position = RandomPondPoint();
        //fishObject.GetComponentInChildren<SpriteRenderer>().sprite = fish.shadow;
        fishObject.GetComponent<MeshFilter>().mesh = fish.fishMesh;
        pondFishes.Add(new Fish(fish, this, fishObject));
    }

    public Vector3 RandomPondPoint()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(pondBounds.min.x, pondBounds.max.x),
            Random.Range(pondBounds.min.y, pondBounds.max.y),
            Random.Range(pondBounds.min.z, pondBounds.max.z)
            );
        Vector3 localPoint = transform.TransformPoint(randomPoint);
        return localPoint;
    }

    private void OnDrawGizmos()
    {
        if (debugPond)
        {
            Matrix4x4 objectMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = objectMatrix;
            Gizmos.color = new Color(200, 0, 0, 0.5f);
            Gizmos.DrawCube(pondBounds.center, pondBounds.size);
        }
    }
}
