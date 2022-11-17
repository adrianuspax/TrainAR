using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AnimationsManager), typeof(ButtonsManager))]
public class Menu : MonoBehaviour
{
    [Header("UI", order = 0)]
    [Space(-10, order = 1)]
    [Header("Canvas Buttons", order = 2)]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button updateProfileButton;
    [SerializeField] private Button editAccountButton;
    [SerializeField] private Button languageButton;
    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private Button signOutButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [Space(-10, order = 0)]
    [Header("Text Mesh Pro", order = 1)]
    [SerializeField] private TextMeshProUGUI usernameTMP;
    [SerializeField] private TextMeshProUGUI signedInAsTMP;
    [SerializeField] private TextMeshProUGUI areYouSureTMP;
    [Space(-10, order = 0)]
    [Header("Animation Panel", order = 1)]
    [SerializeField] private Animator questionPanelAnimator;
    [SerializeField] private AnimationClip questionPanelEndClip;

    [Header(" =========================== READY ONLY =========================== ", order = 0)]

    [Header("Scripts", order = 0)]
    [SerializeField, ReadOnly] private AnimationsManager Animations;

    private void Awake()
    {
        Animations = this.GetComponent<AnimationsManager>();
        closeButton.onClick.AddListener(() => ButtonsManager.CloseApplication());
        updateProfileButton.onClick.AddListener(() => UpdateProfile());
        editAccountButton.onClick.AddListener(() => EditAccount());
        languageButton.onClick.AddListener(() => Language());
        deleteAccountButton.onClick.AddListener(() => DeleteAccount());
        signOutButton.onClick.AddListener(() => SignOut());
        backButton.onClick.AddListener(() => Back());
        yesButton.onClick.AddListener(() => Yes());
        noButton.onClick.AddListener(() => No());

        signedInAsTMP.text = LanguageManager.signedInAs;
        areYouSureTMP.text = LanguageManager.areYouSure;
        UpdateProfileButtonName = LanguageManager.updateProfile;
        EditAccountButtonName = LanguageManager.editAccount;
        LanguageButtonName = LanguageManager.language;
        DeleteAccountButtonName = LanguageManager.deleteAccount;
        SignOutButtonName = LanguageManager.signOut;
        YesButtonName = LanguageManager.yes;
        NoButtonName = LanguageManager.nop;
    }

    private void Start()
    {
        Animations.Transition.EndClip();
        usernameTMP.text = AccountData.Username;
    }

    public void UpdateProfile()
    { ScenesManagement.LoadUserData(); }

    public void EditAccount()
    { ScenesManagement.LoadSignOn(); }

    public void Language()
    { ScenesManagement.LoadLanguage(); }

    public void DeleteAccount()
    { PanelQuestionAnimation(State.PanelStart); }

    public void SignOut()
    {
        AccountData.Signed = false;
        ScenesManagement.LoadSignIn();
    }

    public void Back()
    { ScenesManagement.LoadTrainAR(); }

    public void Yes()
    {
        PanelQuestionAnimation(State.PanelEnd);
        PlayerPrefs.DeleteAll();
        ScenesManagement.LoadRedirection();
    }

    public void No()
    { PanelQuestionAnimation(State.PanelEnd); }

    private void PanelQuestionAnimation(string animationName) => questionPanelAnimator.Play(animationName);
    public string UpdateProfileButtonName
    { set => updateProfileButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string EditAccountButtonName
    { set => editAccountButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string LanguageButtonName
    { set => languageButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string DeleteAccountButtonName
    { set => deleteAccountButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string SignOutButtonName
    { set => signOutButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string YesButtonName
    { set => yesButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
    public string NoButtonName
    { set => noButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = value; }
}
