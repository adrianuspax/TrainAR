using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ASP;
using Debug = ASP.Debug;
[RequireComponent(typeof(AnimationsMain))]
[RequireComponent(typeof(DropDownMenu), typeof(FloatMenu))]
public class AugmentedReality : MonoBehaviour
{
    [Header("AR MANAGEMENT", order = 0)]
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private Transform pvtAnchorAR;
    [SerializeField] private Transform pool3DPivot;
    [SerializeField] private GameObject coach3D;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private bool anchorIsHitting;
    [SerializeField, ReadOnly] private Vector2 screenPoint;
    [SerializeField, ReadOnly] private Vector3 anchorPosSaver;
    [SerializeField, ReadOnly] private TrackableType trackableTypes;
    [SerializeField, ReadOnly] private StateButtonAR stateButtonAR;
    [Space(-10, order = 0)]
    [Header("Scrips")]
    [SerializeField, ReadOnly] private AnimationsMain animationsMain;
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private Transition transition;
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    private static readonly List<ARRaycastHit> hitResults = new();

    private void Awake()
    {
        animationsMain = GetComponent<AnimationsMain>();
        buttonsMain = GetComponent<ButtonsMain>();
        transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();
    }

    private void Start()
    {
        transition.EndClip();
        coach3D.SetActive(false);
        anchorIsHitting = false;
        trackableTypes = TrackableType.PlaneWithinPolygon;
        buttonsMain.PanelCoachInteraction = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        AnchorBehaviour();

        if (CoachIsActivated)
        {
            foreach (Transform pivots in floatMenu.FloatTransforms)
            {
                pivots.LookAt(Camera.main.transform);
            }
        }
    }

    public void AnchorBehaviour()
    {
        Pose hitPose;
        Vector3 anchorPos;
        Quaternion anchorRot;
        float hitDistance;
        bool isHit;

        isHit = arRaycastManager.Raycast(screenPoint, hitResults, trackableTypes);

        if (isHit)
        {
            hitPose = hitResults[0].pose;
            hitDistance = hitResults[0].distance;

            if (hitDistance < 0.1f) { hitDistance = 0.1f; }
            if (hitDistance > 0.5f) { hitDistance = 0.5f; }

            hitDistance = hitDistance * -0.25f + 1.45f;

            anchorPosSaver = hitPose.position;
            anchorPos = Vector3.Lerp(pvtAnchorAR.position, hitPose.position, 0.2f);
            anchorRot = Quaternion.Lerp(pvtAnchorAR.rotation, hitPose.rotation, 0.2f);

            pvtAnchorAR.localScale = new Vector3(hitDistance, hitDistance, hitDistance);
            pvtAnchorAR.SetPositionAndRotation(anchorPos, anchorRot);

            if (!CoachIsActivated)
            {
                coach3D.transform.position = hitPose.position;
                coach3D.transform.LookAt(Camera.main.transform);
            }
        }
        else
        {
            pvtAnchorAR.localPosition = Vector3.zero;
            pvtAnchorAR.localRotation = Quaternion.Euler(Vector3.zero);
        }

        anchorIsHitting = isHit;
        animationsMain.AnchorIsHit(isHit);
        buttonsMain.ButtonARBehaviour(stateButtonAR, isHit);
    }

    public void PlaceCoachAR()
    {
        Vector3 vectorCorrection;
        Quaternion rotationCorrection;

            if (coach3D.activeInHierarchy)
            {
                coach3D.SetActive(false);
                stateButtonAR = StateButtonAR.ReadyToSet;
                animationsMain.AnchorVisible(true);
            }
            else if (AnchorIsHitting)
            {
                coach3D.transform.position = anchorPosSaver;
                coach3D.SetActive(true);
                stateButtonAR = StateButtonAR.Refresh;
                animationsMain.AnchorVisible(false);
                floatMenu.SetBoolFloatAnimators(true);
            }

        vectorCorrection = new Vector3(0, coach3D.transform.rotation.eulerAngles.y, 0);
        rotationCorrection = Quaternion.Euler(vectorCorrection);

        coach3D.transform.rotation = rotationCorrection;
        buttonsMain.PanelCoachInteraction = CoachIsActivated;
        dropDownMenu.Interactable = CoachIsActivated;
    }

    public bool AnchorIsHitting => anchorIsHitting;
    public bool CoachIsActivated => coach3D.activeInHierarchy;
}