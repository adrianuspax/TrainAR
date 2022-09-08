using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AugmentedReality : MonoBehaviour
{
    [Header("AR MANAGEMENT", order = 0)]
    [SerializeField] private Vector2 centerVector;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private Transform pvtAnchorAR;
    [SerializeField] private Transform pool3DPivot;
    [SerializeField] private GameObject coach3DPrefab;
                     private GameObject coachInstantiated;

    private bool coachPlaced;
    private bool anchorIsHitting;
    private Transform cameraPivot;
    private Vector3 anchorPosSaver;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public const string ARCAMERA = "MainCamera";

    private AnimationsManager Animations;
    private CanvasButtons Buttons;

    private void Awake()
    {
        cameraPivot = GameObject.FindGameObjectWithTag(ARCAMERA).transform;
        Animations = this.GetComponent<AnimationsManager>();
        Buttons = this.GetComponent<CanvasButtons>();
    }

    private void Start()
    {
        coachPlaced = false;
        anchorIsHitting = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        centerVector = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
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
        StateButtonAR raycastAndPlacedBasedState;
        StateButtonAR placedBasedState;
        float hitDistance;
        bool raycast;

        raycast = arRaycastManager.Raycast(centerVector, hits, TrackableType.PlaneWithinPolygon);
        placedBasedState = CoachIsPlaced ? StateButtonAR.Refresh : StateButtonAR.ReadyToSet;
        raycastAndPlacedBasedState = (raycast || CoachIsPlaced) ? placedBasedState : StateButtonAR.Uninteractable;

        if (raycast)
        {
            hitPose = hits[0].pose;
            hitDistance = hits[0].distance;

            if (hitDistance < 0.1f) { hitDistance = 0.1f; }
            if (hitDistance > 0.5f) { hitDistance = 0.5f; }

            hitDistance = hitDistance * -0.25f + 1.45f;

            anchorPosSaver = hitPose.position;
            anchorPos = Vector3.Lerp(pvtAnchorAR.position, hitPose.position, 0.2f);
            anchorRot = Quaternion.Lerp(pvtAnchorAR.rotation, hitPose.rotation, 0.2f);

            pvtAnchorAR.localScale = new Vector3(hitDistance, hitDistance, hitDistance);
            pvtAnchorAR.SetPositionAndRotation(anchorPos, anchorRot);

            if (CoachIsPlaced) { coach3DPrefab.transform.position = hitPose.position; }
        }

        anchorIsHitting = raycast;
        Animations.AnchorIsHit(raycast);
        Buttons.ButtonARBehaviour(raycastAndPlacedBasedState);
    }

    public void SetCoachAR()
    {
        if (CoachIsPlaced)
        {
            ReplaceCoachAR();
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

    public void ReplaceCoachAR()
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