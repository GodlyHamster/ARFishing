using UnityEngine;

public class Bobber : MonoBehaviour
{
    public bool isPreviewing { get; private set; } = false;
    public bool isInWater { get; private set; } = false;

    public Vector3 position { get {  return transform.position; } }

    [SerializeField]
    private Material previewMaterial;
    private Material startMaterial;

    private MeshRenderer meshRenderer;

    private FishScriptable attachedFish = null;
    public bool hasFishAttached { get { return attachedFish != null; } }

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        startMaterial = meshRenderer.material;
        gameObject.SetActive(false);
    }

    public bool AttachFish(FishScriptable fish)
    {
        if (attachedFish == null)
        {
            attachedFish = fish;
            return true;
        }
        return false;
    }

    public void PreviewBobber(Vector3 pos)
    {
        gameObject.SetActive(true);
        meshRenderer.material = previewMaterial;
        transform.position = pos;
        isPreviewing = true;
        isInWater = false;
    }

    public void Release()
    {
        isPreviewing = false;
        isInWater = true;
        meshRenderer.material = startMaterial;
    }

    public void ReelIn()
    {
        isInWater = false;
        if (attachedFish != null)
        {
            CatchScreenManager.Instance.CaughtFish(attachedFish);
        }
        gameObject.SetActive(false);
    }
}
