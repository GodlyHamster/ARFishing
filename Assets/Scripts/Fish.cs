using UnityEngine;

public class Fish
{
    public Fish(FishScriptable fishInfo, Pond pond, GameObject connectedObject)
    {
        this.fishInfo = fishInfo;
        this.pond = pond;
        this.connectedObject = connectedObject;
        Start();
    }

    public FishScriptable fishInfo { get; private set; }
    public GameObject connectedObject { get; private set; }
    private Pond pond;

    private float waitTime = 0f;
    private float moveTime = 0f;
    private bool reachedNewPoint = false;
    private Vector3 startingPoint = Vector3.zero;
    private Vector3 endPoint = Vector3.zero;
    private float distance = 0f;
    private bool attachedToBobber = false;

    public void Start()
    {
        startingPoint = connectedObject.transform.localPosition;
        endPoint = pond.RandomPondPoint();
    }

    public void Update()
    {
        waitTime -= Time.deltaTime;
        if (attachedToBobber) return;

        if (!reachedNewPoint && waitTime <= 0f)
        {
            Transform transform = connectedObject.transform;
            moveTime += Time.deltaTime / distance;
            transform.localPosition = Vector3.Lerp(startingPoint, endPoint, moveTime);
            if (Vector3.Distance(connectedObject.transform.localPosition, endPoint) <= 0.01f)
            {
                reachedNewPoint = true;
            }
            if (Vector3.Distance(connectedObject.transform.localPosition, pond.bobber.position) <= 0.01f && pond.bobber.isInWater)
            {
                attachedToBobber = pond.bobber.AttachFish(this);
            }
        }
        if (reachedNewPoint)
        {
            waitTime = Random.Range(fishInfo.minWaitTime, fishInfo.maxWaitTime);
            startingPoint = connectedObject.transform.localPosition;
            if (pond.bobber.isInWater && !pond.bobber.hasFishAttached)
            {
                endPoint = pond.bobber.position;
            }
            else
            {
                endPoint = pond.RandomPondPoint();
            }
            distance = Vector3.Distance(startingPoint, endPoint);
            moveTime = 0f;
            reachedNewPoint = false;
        }
    }
}
