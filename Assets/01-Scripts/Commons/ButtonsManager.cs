using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ASP;
using Debug = ASP.Debug;

public class ButtonsManager : MonoBehaviour
{
    [Header(" =========================== READY ONLY =========================== ", order = 0)]

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private ButtonsMain CanvasButtons;
    [SerializeField, ReadOnly] private DropDownMenu DDM;
    [SerializeField, ReadOnly] private UserDataButtons UserDataButtons;
    [SerializeField, ReadOnly] private FloatMenu FloatMenuButtons;
    [SerializeField, ReadOnly] private SignIn SignInButtons;
    [SerializeField, ReadOnly] private SignOn SignOnButtons;
    [SerializeField, ReadOnly] private LanguageManager LanguageButtons;
    [SerializeField, ReadOnly] private Menu MenuButtons;
    [SerializeField, ReadOnly] private PanelCoach PanelCoachButtons;

    public delegate void BehaviourButtons(int index);

    private void Awake()
    {
        if (this.GetComponent<ButtonsMain>() != null) { CanvasButtons = this.GetComponent<ButtonsMain>(); }
        if (this.GetComponent<DropDownMenu>() != null) { DDM = this.GetComponent<DropDownMenu>(); }
        if (this.GetComponent<UserDataButtons>() != null) { UserDataButtons = this.GetComponent<UserDataButtons>(); }
        if (this.GetComponent<FloatMenu>() != null) { FloatMenuButtons = this.GetComponent<FloatMenu>(); }
        if (this.GetComponent<SignIn>() != null) { SignInButtons = this.GetComponent<SignIn>(); }
        if (this.GetComponent<SignOn>() != null) { SignOnButtons = this.GetComponent<SignOn>(); }
        if (this.GetComponent<LanguageManager>() != null) { LanguageButtons = this.GetComponent<LanguageManager>(); }
        if (this.GetComponent<Menu>() != null) { MenuButtons = this.GetComponent<Menu>(); }
        if (this.GetComponent<PanelCoach>() != null) { PanelCoachButtons = this.GetComponent<PanelCoach>(); }
    }

    public ButtonsMain CanvasMain => CanvasButtons;
    public DropDownMenu DropDownMenu => DDM;
    public UserDataButtons UserData => UserDataButtons;
    public FloatMenu FloatMenu => FloatMenuButtons;
    public SignIn SingIn => SignInButtons;
    public LanguageManager Language => LanguageButtons;
    public Menu Menu => MenuButtons;
    public PanelCoach PanelCoach => PanelCoachButtons;

    public (Button[] buttons, bool[] booleans) Assignment(Transform header, params BehaviourButtons[] behaviour)
    {
        Button[] button;
        bool[] isPressed;
        button = new Button[header.childCount];
        isPressed = new bool[button.Length];

        for (int index = 0; index < button.Length; index++)
        {
            for (int i = 0; i < behaviour.Length; i++)
            { StartCoroutine(Cor_AssignmentButtons(index, button, header, behaviour[i])); }
        }

        return (button, isPressed);
    }

    private IEnumerator Cor_AssignmentButtons(int index, Button[] button, Transform header, BehaviourButtons behaviour)
    {
        button[index] = header.GetChild(index).GetComponent<Button>();
        button[index].onClick.AddListener(() => behaviour(index));
        yield return null;
    }


    public void Interaction(bool interactable, params Button[][] array)
    {
        foreach (Button[] buttons in array)
        { Interaction(interactable, buttons); }
    }

    public void Interaction(bool interactable, params Button[] button)
    {
        for (int i = 0; i < button.Length; i++)
        {
            int childCount;

            childCount = button[i].transform.childCount;

            if (childCount != 0)
            {
                for (int index = 0; index < childCount; index++)
                {
                    if (button[i].transform.GetChild(index).GetComponent<TextMeshProUGUI>() != null)
                    {
                        StartCoroutine(Cor_Interactable(button[i], interactable, index));
                        break;
                    }
                }
            }
            else
            { button[i].interactable = interactable; }
        }
    }

    private IEnumerator Cor_Interactable(Button button, bool interactable, int index)
    {
        Color color;

        button.interactable = interactable;
        color = button.transform.GetChild(index).GetComponent<TextMeshProUGUI>().color;

        yield return null;

        color.a = interactable ? 1f : 0.5f;

        yield return null;

        button.transform.GetChild(index).GetComponent<TextMeshProUGUI>().color = color;
    }

    public static string PlaceHolderBehaviour(string text, bool activation)
    {
        return activation ? text : string.Empty;
    }

    #region CLOSE_BUTTON
    public static void CloseApplication()
    {
#if UNITY_EDITOR
        Debug.Log("CloseApplication() called!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion
}