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
    [SerializeField, ReadOnly] private PanelCoach panelCoach;
    [SerializeField, ReadOnly] private PanelResult panelResult;
    [SerializeField, ReadOnly] private Loading loading;

    private void Awake()
    {
        if (this.GetComponent<UserDataAnimations>() != null) { userData = this.GetComponent<UserDataAnimations>(); }
        if (this.GetComponent<AnimationsMain>() != null) { main = this.GetComponent<AnimationsMain>(); }
        if (this.GetComponent<FloatMenu>() != null) { floatMenu = this.GetComponent<FloatMenu>(); }
        if (this.GetComponent<CoachAnimations>() != null) { coach = this.GetComponent<CoachAnimations>(); }
        if (this.GetComponent<PanelCoach>() != null) { panelCoach = this.GetComponent<PanelCoach>(); }
        if (this.GetComponent<PanelResult>() != null) { panelResult = this.GetComponent<PanelResult>(); }
        if (this.GetComponent<Loading>() != null) { loading = this.GetComponent<Loading>(); }
    }

    private void OnEnable()
    {
        transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();
    }

    public UserDataAnimations UserData => userData;
    public AnimationsMain Main => main;
    public FloatMenu FloatMenu => floatMenu;
    public CoachAnimations Coach => coach;
    public Transition Transition => transition;
    public PanelCoach PanelCoach => panelCoach;
    public PanelResult PanelResult => panelResult;
    public Loading Loading => loading;
}
