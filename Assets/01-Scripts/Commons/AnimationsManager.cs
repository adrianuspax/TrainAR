using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private UserDataAnimations userData;
    [SerializeField, ReadOnly] private AnimationsMain main;
    [SerializeField, ReadOnly] private FloatMenu floatMenu;
    [SerializeField, ReadOnly] private CoachAnimations coach;
    [SerializeField, ReadOnly] private Transition transition;


    private void Awake()
    {
        if (this.GetComponent<UserDataAnimations>() != null) { userData = this.GetComponent<UserDataAnimations>(); }
        if (this.GetComponent<AnimationsMain>() != null) { main = this.GetComponent<AnimationsMain>(); }
        if (this.GetComponent<FloatMenu>() != null) { floatMenu = this.GetComponent<FloatMenu>(); }
        if (this.GetComponent<CoachAnimations>() != null) { coach = this.GetComponent<CoachAnimations>(); }
    }

    private void OnEnable()
    {
        transition = GameObject.FindGameObjectWithTag(TAG.Transition).GetComponent<Transition>();
    }

    public UserDataAnimations UserData => userData;
    public AnimationsMain Main => main;
    public FloatMenu FloatMenu => floatMenu;
    public CoachAnimations Coach => coach;
    public Transition Transition => transition;
}
