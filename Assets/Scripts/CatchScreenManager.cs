using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchScreenManager : MonoBehaviour
{
    public static CatchScreenManager Instance;

    [SerializeField]
    private GameObject catchScreen;

    [SerializeField]
    private TextMeshProUGUI fishName;
    [SerializeField]
    private Image fishImage;

    public bool isScreenActive { get { return catchScreen.activeSelf; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        catchScreen.SetActive(true);
    }

    public void CaughtFish(FishScriptable fish)
    {
        fishName.text = "You caught a: " + fish.name;
        fishImage.sprite = fish.sprite;
        catchScreen.SetActive(true);
    }

    public void CloseScreen()
    {
        catchScreen.SetActive(false);
    }
}
