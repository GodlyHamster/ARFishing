using UnityEngine;
using UnityEngine.InputSystem;

public class TapInteraction : MonoBehaviour
{
    private DebugText fps = new DebugText("0fps");
    private DebugText tapping = new DebugText("Tap: False");
    private DebugText holding = new DebugText("Hold: False");
    private DebugText position = new DebugText("x:0, y:0");

    private Vector3 tapLocation;
    private bool isHolding;

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
            Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.red);
            if (Physics.Raycast(camRay.origin, camRay.direction, out hit, Mathf.Infinity))
            {
                holding.text = "Hold hit: " + hit.transform.gameObject.name;
            }
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
