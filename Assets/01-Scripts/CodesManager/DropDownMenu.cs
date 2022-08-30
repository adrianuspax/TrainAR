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
    [Space(-10, order = 0)]
    [Header("Panel Float", order = 1)]
    [SerializeField] private Transform pivotCenterPanel;
    [SerializeField] private float velocityToReturn;

    private Transform cameraTransform;
    private Rigidbody cameraRigidBody;
    private bool panelDropDownOpen;

    private void Awake()
    {
        btnOpenDropDown.onClick.AddListener(ButtonOpenDropDown);
        btnCloseDropDown.onClick.AddListener(ButtonCloseDropDown);

        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        cameraRigidBody = GameObject.FindWithTag("MainCamera").GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        //PanelFloat(velocityToReturn);
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

    /*public void PanelFloat(float velocity)
    {
        if (panelDropDownOpen)
        {
            if (cameraRigidBody.IsSleeping())
            {
                pivotCenterPanel.localRotation = Quaternion.Lerp(pivotCenterPanel.localRotation, Quaternion.identity, Time.deltaTime * velocity * 5);
            }
            else
            {
                pivotCenterPanel.localRotation = Quaternion.Lerp(pivotCenterPanel.localRotation, cameraTransform.localRotation, Time.deltaTime / velocity);
            }
        }
    }*/

    private IEnumerator Cor_ScrowViewPos(float time)
    {
        yield return new WaitForSeconds(time);
        dropDownScrollRect.verticalNormalizedPosition = 0.5f;
    }
}