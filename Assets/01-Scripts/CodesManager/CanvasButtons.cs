using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasButtons : MonoBehaviour
{
    [Header("BUTTONS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Top Area", order = 2)]
    [SerializeField] private Button btnTopL;
    [SerializeField] private Button btnTopR;
    [Space(-10, order = 0)]
    [Header("Top Area", order = 1)]
    [SerializeField] private Button btnBarL;
    [SerializeField] private Button btnBarR;
    [SerializeField] private Button btnBarC;

    private AugmentedReality AR;

    private void Awake()
    {
        btnTopR.onClick.AddListener(CloseApplication);
        AR = this.GetComponent<AugmentedReality>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void CloseApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SetCoachInPlace()
    {
        
    }
}