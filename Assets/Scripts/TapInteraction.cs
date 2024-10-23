using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class TapInteraction : MonoBehaviour
{
    private DebugText fps = new DebugText("0fps");
    private DebugText tapping = new DebugText("Tap: False");
    private DebugText holding = new DebugText("Hold: False");
    private DebugText position = new DebugText("x:0, y:0");

    [SerializeField]
    private GameObject bobberPrefab;
    [SerializeField]
    private Material bobberPreviewMaterial;

    [SerializeField]
    private ARTrackedImageManager trackerManager;

    private GameObject bobber;
    private GameObject previewBobber;

    private bool bobberInWater = false;

    private Vector3 tapLocation;
    private bool isHolding;

    private void OnEnable()
    {
        trackerManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        trackerManager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var added in args.added)
        {
            Transform pondTransform = Pond.Instance.transform;
            previewBobber = Instantiate(bobberPrefab, pondTransform.position, pondTransform.rotation, pondTransform);
            previewBobber.GetComponentInChildren<MeshRenderer>().material = bobberPreviewMaterial;
            bobber = Instantiate(bobberPrefab, pondTransform.position, pondTransform.rotation, pondTransform);
            bobber.SetActive(false);
            previewBobber.SetActive(false);
        }
    }

    private void Start()
    {
        UIDebugManager.instance.AddDebug(fps);
        UIDebugManager.instance.AddDebug(tapping);
        UIDebugManager.instance.AddDebug(holding);
        UIDebugManager.instance.AddDebug(position);
    }

    void Update()
    {
        fps.text = (1f / Time.deltaTime).ToString("F2") + "fps";

        if (isHolding)
        {
            RaycastHit hit;
            Ray camRay = Camera.main.ScreenPointToRay(tapLocation);
            Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.magenta);
            if (Physics.Raycast(camRay.origin, camRay.direction, out hit, Mathf.Infinity))
            {
                bobberInWater = false;
                Transform objectTransform = hit.transform;
                previewBobber.transform.position = hit.point;
                bobber.transform.position = hit.point;
                bobber.SetActive(false);
                previewBobber.SetActive(true);
            }
        }
        else
        {
            if (previewBobber != null)
            {
                if (previewBobber.activeSelf) bobberInWater = true;
                previewBobber.SetActive(false);
            }
        }
        if (bobberInWater)
        {
            bobber?.SetActive(true);
        }
    }

    private void OnTap(InputValue value)
    {
        tapping.text = "Tap: "+ value.isPressed;
    }

    private void OnHold(InputValue value)
    {
        isHolding = value.isPressed;
        holding.text = "Hold: " + isHolding;
    }

    private void OnTapLocation(InputValue value)
    {
        Vector2 pos = value.Get<Vector2>();
        tapLocation = pos;
        position.text = "x:" + pos.x.ToString("F2") + ", y:" + pos.y.ToString("F2");
    }
}
