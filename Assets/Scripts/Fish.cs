using Unity.VisualScripting;
using UnityEngine;

public class Fish
{
    public Fish(FishScriptable fishInfo, Pond pond, GameObject connectedObject)
    {
        this.fishInfo = fishInfo;
        this.pond = pond;
        this.connectedObject = connectedObject;
        this.name = fishInfo.name;
        Start();
    }

    public string name { get; private set; }
    private GameObject connectedObject;
    private FishScriptable fishInfo;
    private Pond pond;

    private float waitTime = 2f;
    private bool reachedNewPoint = false;
    Vector3 randomPoint = Vector3.zero;
    private bool attachedToBobber = false;

    public void Start()
    {
        randomPoint = pond.RandomPondPoint();
    }

    public void Update()
    {
        //make fish move smooth and eat bobber
        waitTime -= Time.deltaTime;
        if (attachedToBobber) return;

        if (!reachedNewPoint && waitTime <= 0)
        {
            Transform transform = connectedObject.transform;
            transform.position = Vector3.MoveTowards(transform.position, randomPoint, Time.deltaTime / 2f);
            if (Vector3.Distance(connectedObject.transform.position, randomPoint) <= 0.01f)
            {
                reachedNewPoint = true;
            }
            if (Vector3.Distance(connectedObject.transform.position, pond.bobber.position) <= 0.01f && pond.bobber.isInWater)
            {
                attachedToBobber = pond.bobber.AttachFish(fishInfo);
            }
        }
        if (reachedNewPoint)
        {
            waitTime = 2f;
            if (pond.bobber.isInWater && !pond.bobber.hasFishAttached)
            {
                randomPoint = pond.bobber.position;
            }
            else
            {
                randomPoint = pond.RandomPondPoint();
            }
            reachedNewPoint = false;
        }
    }
}
