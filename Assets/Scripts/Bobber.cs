using UnityEngine;

public class Bobber : MonoBehaviour
{
    public bool isPreviewing { get; private set; } = false;
    public bool isInWater { get; private set; } = false;

    public Vector3 position { get {
            return Pond.Instance.transform.InverseTransformPoint(transform.position);
        } }

    [SerializeField]
    private ParticleSystem splashParticle;

    [SerializeField]
    private Material previewMaterial;
    private Material startMaterial;

    private MeshRenderer meshRenderer;

    private Fish fish = null;
    public bool hasFishAttached { get { return fish != null; } }

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        startMaterial = meshRenderer.material;
        gameObject.SetActive(false);
    }

    public bool AttachFish(Fish fish)
    {
        if (this.fish == null)
        {
            this.fish = fish;
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
        splashParticle.Play();
    }

    public void ReelIn()
    {
        isInWater = false;
        if (fish != null)
        {
            CatchScreenManager.Instance.CaughtFish(fish);
            fish = null;
        }
        gameObject.SetActive(false);
    }
}
