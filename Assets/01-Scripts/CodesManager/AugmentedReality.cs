using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AugmentedReality : MonoBehaviour
{
    [Header("AR MANAGEMENT", order = 0)]
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private Transform pvtAnchorAR;
    [SerializeField] private Transform pool3DPivot;
    [SerializeField] private GameObject coach3DPrefab;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Variables", order = 2)]
    [SerializeField, ReadOnly] private Vector2 screenPoint;
    [SerializeField, ReadOnly] private GameObject coachInstantiated;
    [SerializeField, ReadOnly] private bool coachPlaced;
    [SerializeField, ReadOnly] private bool anchorIsHitting;
    [SerializeField, ReadOnly] private Transform cameraPivot;
    [SerializeField, ReadOnly] private Vector3 anchorPosSaver;
    [SerializeField, ReadOnly] private TrackableType trackableTypes;
    [Space(-10, order = 0)]
    [Header("Scrips")]
    [SerializeField, ReadOnly] private AnimationsManager Animations;
    [SerializeField, ReadOnly] private ButtonsManager Buttons;

    static readonly List<ARRaycastHit> hitResults = new();

    public const string ARCAMERA = "MainCamera";

    private void Awake()
    {
        cameraPivot = GameObject.FindGameObjectWithTag(ARCAMERA).transform;
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<ButtonsManager>();
    }

    private void Start()
    {
        coachPlaced = false;
        anchorIsHitting = false;
        trackableTypes = TrackableType.PlaneWithinPolygon;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        screenPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        AnchorBehaviour();
    }

    public void AnchorBehaviour()
    {
        Pose hitPose;
        Vector3 anchorPos;
        Quaternion anchorRot;
        StateButtonAR state;
        //StateButtonAR placedBasedState;
        float hitDistance;
        bool isHit;

        isHit = arRaycastManager.Raycast(screenPoint, hitResults, trackableTypes);
        /*placedBasedState = CoachIsPlaced ? StateButtonAR.Refresh : StateButtonAR.ReadyToSet;
        state = (isHit || CoachIsPlaced) ? placedBasedState : StateButtonAR.Uninteractable;*/

        /*if (isHit)
        {
            if (CoachIsPlaced)
            {
                state = StateButtonAR.Refresh;
            }
            else
            {
                state = StateButtonAR.ReadyToSet;
            }
        }
        else
        {
            state = StateButtonAR.Uninteractable;
        }*/

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

            if (CoachIsPlaced)
            {
                coach3DPrefab.transform.position = hitPose.position;
                state = StateButtonAR.Refresh;
            }
            else
            {
                state = StateButtonAR.ReadyToSet;
            }
        }
        else
        {
            state = StateButtonAR.Uninteractable;
        }

        anchorIsHitting = isHit;
        Animations.AnchorIsHit(isHit);
        Buttons.CanvasMain.ButtonARBehaviour(state);
    }

    public void SetCoachAR()
    {
        if (CoachIsPlaced)
        {
            ReplaceCoachAR(coachInstantiated);
        }
        else if (AnchorIsHitting)
        {
            coachInstantiated = Instantiate(this.coach3DPrefab, pool3DPivot);
            Animations.coach.AssignAnimator();
            coachInstantiated.transform.localScale = Vector3.one;
            coachInstantiated.transform.position = anchorPosSaver;
            coachPlaced = true;
        }
    }

    public void ReplaceCoachAR(GameObject coachInstantiated)
    {
        if (coachInstantiated.activeInHierarchy)
        {
            coachInstantiated.SetActive(false);
        }
        else if(AnchorIsHitting)
        {
            coachInstantiated.transform.position = anchorPosSaver;
            coachInstantiated.SetActive(true);
        }
    }

    public bool CoachIsInstantiated => GameObject.FindWithTag("Coach");
    public bool AnchorIsHitting => anchorIsHitting;
    public bool CoachIsPlaced => coachPlaced;
}