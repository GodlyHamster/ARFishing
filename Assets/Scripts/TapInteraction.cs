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
    private ARTrackedImageManager trackerManager;

    private Bobber bobber;

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
            if (added.transform.gameObject.GetComponent<Pond>())
            {
                Transform pondTransform = Pond.Instance.transform;
                GameObject bobberObj = Instantiate(bobberPrefab, pondTransform.position, pondTransform.rotation, pondTransform);
                bobber = bobberObj.GetComponent<Bobber>();
                Pond.Instance.SetBobber(bobber);
            }
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

        if (bobber == null) return;
        if (isHolding)
        {
            RaycastHit hit;
            Ray camRay = Camera.main.ScreenPointToRay(tapLocation);
            Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.magenta);
            if (Physics.Raycast(camRay.origin, camRay.direction, out hit, Mathf.Infinity))
            {
                Transform objectTransform = hit.transform;

                bobber.PreviewBobber(hit.point);
            }
        }
        else if (bobber.isPreviewing)
        {
            bobber.Release();
        }
    }

    private void OnTap(InputValue value)
    {
        tapping.text = "Tap: "+ value.isPressed;
        if (CatchScreenManager.Instance.isScreenActive)
        {
            CatchScreenManager.Instance.CloseScreen();
        }
        if (bobber != null && bobber.isInWater)
        {
            bobber.ReelIn();
        }
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
