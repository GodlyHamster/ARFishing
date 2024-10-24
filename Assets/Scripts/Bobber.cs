using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public bool isPreviewing = false;

    [SerializeField]
    private Material previewMaterial;
    private Material startMaterial;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        startMaterial = meshRenderer.material;
    }

    public void PreviewBobber(Vector3 pos)
    {
        meshRenderer.material = previewMaterial;
        transform.position = pos;
        isPreviewing = true;
    }

    public void Release()
    {
        isPreviewing = false;
        meshRenderer.material = startMaterial;
    }
}
