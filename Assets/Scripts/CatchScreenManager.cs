using System.Collections.Generic;
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
    private TextMeshProUGUI fishDescription;
    [SerializeField]
    private Image fishImage;
    [SerializeField]
    private List<Image> stars = new List<Image>();
    [SerializeField]
    private Sprite fullStar;
    [SerializeField]
    private Sprite emptyStar;

    public bool isScreenActive { get { return catchScreen.activeSelf; } }

    private float waitTime = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        waitTime -= Time.deltaTime;
    }

    public void CaughtFish(Fish fish)
    {
        waitTime = 0.5f;
        fishName.text = fish.fishInfo.name + "!";
        fishDescription.text = fish.fishInfo.description;
        fishImage.sprite = fish.fishInfo.sprite;
        for (int i = 0; i < stars.Count - 1; i++) 
        {
            stars[i].sprite = fish.fishInfo.rarity > i ? fullStar : emptyStar;
        }
        catchScreen.SetActive(true);
        Pond.Instance.RemoveFish(fish);
    }

    public void CloseScreen()
    {
        if (waitTime <= 0f)
        {
            catchScreen.SetActive(false);
        }
    }
}
