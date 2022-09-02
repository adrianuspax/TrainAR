using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AugmentedReality : MonoBehaviour
{
    [Header("AR MANAGEMENT", order = 0)]
    [SerializeField] private Vector2 centerVector;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private Transform pvtAnchorAR;
    [SerializeField] private Transform pool3Dpivot;
    [SerializeField] private GameObject coatch3D;

    /*[HideInInspector]*/ public bool coachPlaced;
    private bool anchorEnable;
    private Transform cameraPivot;
    private Vector3 anchorPosSaver;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public const string ARCAMERA = "MainCamera";

    private void Awake()
    {
        cameraPivot = GameObject.FindGameObjectWithTag(ARCAMERA).transform;
    }

    private void Start()
    {
        centerVector = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        AnchorBehaviour();
    }

    public void AnchorBehaviour()
    {
        Pose hitPose;
        float hitDistance;
        Vector3 anchorPos;
        Quaternion anchorRot;
        bool raycast;

        raycast = arRaycastManager.Raycast(centerVector, hits, TrackableType.PlaneWithinPolygon);

        if (raycast)
        {
            anchorEnable = true;
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


        }
        else
        {

        }
    }

    public void SetCoachAR(Vector3 pos)
    {
        coatch3D.transform.position = pos;
        coachPlaced = true;
    }
}