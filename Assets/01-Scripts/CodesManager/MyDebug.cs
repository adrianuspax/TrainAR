using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyDebug : MonoBehaviour
{
    [SerializeField] private bool DebugIsAtctivated;
    [SerializeField] private Canvas canvasDebug;
    [SerializeField] private Text debugText;
    [SerializeField] private Text updateText;
    [SerializeField] private Button closeButton;
                     private static Text staticText;
                     private static Text staticTextUpdate;

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);

        if (DebugIsAtctivated)
        {
            staticText = debugText;
            staticTextUpdate = updateText;
            return;
        }
        else
        {
            Close();
        }
    }

    public static void LogUpdate(object info)
    {
        staticTextUpdate.text = "UPDATE:\n" + info.ToString();
    }

    public static void Log(object info)
    {
        staticText.text += "\n" + info.ToString();
    }

    private void Close()
    {
        canvasDebug.enabled = false;
        this.enabled = false;
    }
}