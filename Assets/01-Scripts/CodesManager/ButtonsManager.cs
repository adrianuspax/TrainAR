using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private CanvasButtons CanvasButtons;
    [SerializeField, ReadOnly] private DropDownMenu DDM;

    private void Awake()
    {
        CanvasButtons = this.GetComponent<CanvasButtons>();
        DDM = this.GetComponent<DropDownMenu>();
    }

    public CanvasButtons CanvasMain => CanvasButtons;
    public DropDownMenu DropDownMenu => DDM;

}