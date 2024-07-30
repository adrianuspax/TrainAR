using ASP;
using ASP.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AugmentedReality : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private ARRaycastManager aRRaycastManager;
    [SerializeField, ReadOnly] private Transform anchorARPivot;
    [SerializeField, ReadOnly] private Transform poolPivot;
    [Space(-10, order = 0)]
    [Header(Header.variables, order = 1)]
    [SerializeField, ReadOnly] private int variable;
    [SerializeField, ReadOnly] private bool anchorIsHitting;
    [SerializeField, ReadOnly] private Vector2 screenPoint;
    [SerializeField, ReadOnly] private Vector3 anchorPosSaver;
    [SerializeField, ReadOnly] private TrackableType trackableTypes;
    [SerializeField, ReadOnly] private ButtonsMain.StateButtonAR stateButtonAR;

    [Header(Header.scripts)]
    [SerializeField, ReadOnly] private AnchorAnimation animationsMain;
    [SerializeField, ReadOnly] private ButtonsFloat floatMenu;
    [SerializeField, ReadOnly] private Transition transition;
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;
    [SerializeField, ReadOnly] private Coach coach;

    private static readonly List<ARRaycastHit> hitResults = new();

    private void Awake()
    {
        ComponentsAssignment();
    }

    private void Start()
    {
        transition.EndClip();
        coach.gameObject.SetActive(false);
        anchorIsHitting = false;
        trackableTypes = TrackableType.PlaneWithinPolygon;
        buttonsMain.PanelCoachInteraction = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        AnchorBehaviour();

        if (coach.gameObject.activeInHierarchy)
        {
            foreach (Transform pivots in floatMenu.FloatTransforms)
            {
                pivots.LookAt(Camera.main.transform);
            }
        }
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        this.GetComponentInChildrenIfNull(ref aRRaycastManager);
        if (poolPivot.IsNull())
            poolPivot = GameObject.Find("3D").transform;
        if (coach.IsNull())
            coach = FindAnyObjectByType<Coach>();
        this.GetComponentIfNull(ref anchorARPivot, 0, 0, 0, 0, 0);
        if (animationsMain.IsNull())
            animationsMain = FindAnyObjectByType<AnchorAnimation>();
        if (floatMenu.IsNull())
            floatMenu = FindAnyObjectByType<ButtonsFloat>();
        if (dropDownMenu.IsNull())
            dropDownMenu = FindAnyObjectByType<DropDownMenu>();
        if (buttonsMain.IsNull())
            buttonsMain = FindAnyObjectByType<ButtonsMain>();
        if (transition.IsNull())
            transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void AnchorBehaviour()
    {
        bool isHit = aRRaycastManager.Raycast(screenPoint, hitResults, trackableTypes);

        if (isHit)
        {
            Pose hitPose = hitResults[0].pose;
            float hitDistance = hitResults[0].distance;

            hitDistance = Mathf.Clamp(hitDistance, 0.1f, 0.5f);
            hitDistance = hitDistance * -0.25f + 1.45f;

            anchorPosSaver = hitPose.position;
            Vector3 anchorPos = Vector3.Lerp(anchorARPivot.position, hitPose.position, 0.2f);
            Quaternion anchorRot = Quaternion.Lerp(anchorARPivot.rotation, hitPose.rotation, 0.2f);

            anchorARPivot.localScale = new Vector3(hitDistance, hitDistance, hitDistance);
            anchorARPivot.SetPositionAndRotation(anchorPos, anchorRot);

            if (!coach.gameObject.activeInHierarchy)
            {
                coach.transform.position = hitPose.position;
                coach.transform.LookAt(Camera.main.transform);
            }
        }
        else
        {
            anchorARPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        anchorIsHitting = isHit;
        animationsMain.AnchorIsHit(isHit);
        buttonsMain.ButtonARBehaviour(stateButtonAR, isHit);
    }

    public void PlaceCoachAR()
    {
        Vector3 vectorCorrection;
        Quaternion rotationCorrection;

        if (coach.gameObject.activeInHierarchy)
        {
            coach.gameObject.SetActive(false);
            stateButtonAR = ButtonsMain.StateButtonAR.ReadyToSet;
            animationsMain.AnchorVisible(true);
        }
        else if (anchorIsHitting)
        {
            coach.gameObject.transform.position = anchorPosSaver;
            coach.gameObject.SetActive(true);
            stateButtonAR = ButtonsMain.StateButtonAR.Refresh;
            animationsMain.AnchorVisible(false);
            floatMenu.SetBoolFloatAnimators(true);
        }

        vectorCorrection = new Vector3(0f, coach.gameObject.transform.rotation.eulerAngles.y, 0f);
        rotationCorrection = Quaternion.Euler(vectorCorrection);

        coach.gameObject.transform.rotation = rotationCorrection;
        buttonsMain.PanelCoachInteraction = coach.gameObject.activeInHierarchy;
        dropDownMenu.Interactable = coach.gameObject.activeInHierarchy;
    }

    public bool CoachIsActivated => coach.gameObject.activeInHierarchy;
}