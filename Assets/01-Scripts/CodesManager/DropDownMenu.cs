using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour
{
    [Header("DROP-DOWN MENU BEHAVIOUR", order = 0)]
    [Space(-10, order = 1)]
    [Header("Buttons", order = 2)]
    [SerializeField] private Button btnOpenDropDown;
    [SerializeField] private Button btnCloseDropDown;
    [Space(-10, order = 0)]
    [Header("Animations", order = 1)]
    [SerializeField] private Animator aniDropDownMenu;
    [SerializeField] private AnimationClip dropDownCloseClip;
    [SerializeField] private AnimationClip dropDownOpenClip;
    [SerializeField] private AnimationClip dropDownNullClip;
    [Space(-10, order = 0)]
    [Header("Scrow View", order = 1)]
    [SerializeField] private ScrollRect dropDownScrollRect;

    private bool panelDropDownOpen;

    private void Awake()
    {
        btnOpenDropDown.onClick.AddListener(ButtonOpenDropDown);
        btnCloseDropDown.onClick.AddListener(ButtonCloseDropDown);
    }

    public void ButtonOpenDropDown()
    {
        if (panelDropDownOpen)
        {
            return;
        }
        else
        {
            panelDropDownOpen = true;
            aniDropDownMenu.Play(dropDownOpenClip.name);
            StartCoroutine(Cor_ScrowViewPos(dropDownOpenClip.length));
        }
    }

    public void ButtonCloseDropDown()
    {
        if (!panelDropDownOpen)
        {
            return;
        }
        else
        {
            panelDropDownOpen = false;
            aniDropDownMenu.Play(dropDownCloseClip.name);
        }
    }

    private IEnumerator Cor_ScrowViewPos(float time)
    {
        yield return new WaitForSeconds(time);
        dropDownScrollRect.verticalNormalizedPosition = 0.5f;
    }
}