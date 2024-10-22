using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDebugManager : MonoBehaviour
{
    public static UIDebugManager instance;

    [SerializeField]
    private TextMeshProUGUI debugText;

    private List<DebugText> _debugLines = new List<DebugText>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        debugText.text = "";
        foreach (var line in _debugLines) { 
            debugText.text += line.text + "\n";
        }
    }

    public void AddDebug(DebugText message)
    {
        _debugLines.Add(message);
    }
}

[Serializable]
public class DebugText
{
    public DebugText(string text)
    {
        this.text = text;
    }

    public string text = "Debug None";
}
