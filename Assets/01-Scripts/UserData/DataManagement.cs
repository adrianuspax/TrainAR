using ASP;
using ASP.Extensions;
using TreinAR.Commons;
using UnityEngine;
using Debug = ASP.Debug;
using PlayerPrefs = ASP.PlayerPrefs;

namespace TreinAR.UserData
{
    public class DataManagement : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header("SAVED DATA PROFILE", order = 2)]
#pragma warning disable IDE0052
        [SerializeField, ReadOnly] private string userName;
        [SerializeField, ReadOnly] private string gender;
        [SerializeField, ReadOnly] private string goal;
        [SerializeField, ReadOnly] private string level;
        [SerializeField, ReadOnly] private string focus;
        [SerializeField, ReadOnly] private string age;
        [SerializeField, ReadOnly] private string currentWeight;
        [SerializeField, ReadOnly] private string weightGoal;
        [SerializeField, ReadOnly] private string height;
        [SerializeField, ReadOnly] private string language;
        [Space(-10, order = 1)]
        [Header("SAVED DATA ACCOUNT", order = 2)]
        [SerializeField, ReadOnly] private string username;
        [SerializeField, ReadOnly] private string password;
#pragma warning restore IDE0052
        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private PagesAndFields pagesAndFields;
        [SerializeField, ReadOnly] private Transition transition;
        [SerializeField, ReadOnly] private UserDataButtons userDataButtons;
        [Space(20, order = 0)]
        [NonReorderable, ReadOnly] public string[] levelPhrase;

        public readonly static PlayerPrefs.String[] pageKey = new PlayerPrefs.String[]
    {
        new("name"),
        new("gender"),
        new("goal"),
        new("level"),
        new("focus")
    };

        public readonly static PlayerPrefs.String[] footerKey = new PlayerPrefs.String[]
    {
        new("age"),
        new("currentWeight"),
        new("weightGoal"),
        new("height")
    };

        public readonly string[] goalName = new string[] { "loseWeight", "moreMuscles", "keepShape" };
        public readonly string[] focusName = new string[] { "abdomen", "chest", "arms", "legs" };

        public const int PAGE1 = 1;
        public const int PAGE2 = 2;
        public const int PAGE3 = 3;
        public const int PAGE4 = 4;
        public const int PAGE5 = 5;
        public const int PAGE6 = 6;

        public const int FOOTER1 = 0;
        public const int FOOTER2 = 1;
        public const int FOOTER3 = 2;
        public const int FOOTER4 = 3;

        public const string MALE = "Male";
        public const string FEMALE = "Female";

        public void Awake()
        {
            ComponentsAssignment();

            /*pagesAndFields.PagesQuestionString = LanguageManager.pageQuestion;
            userDataButtons.NextButtonString = LanguageManager.nextButton;
            pagesAndFields.Page1PlaceHolder = LanguageManager.page1PlaceHolder;
            userDataButtons.GoalButtonsName = LanguageManager.goalPhrase;
            userDataButtons.FocusButtonsName = LanguageManager.buttonFocus;
            pagesAndFields.BlockAreaName = LanguageManager.blockAreaName;
            pagesAndFields.MoreValuesStringEdit = LanguageManager.valueDefaultButtonsEdit;
            pagesAndFields.FooterTitle = LanguageManager.footerTitle;
            pagesAndFields.FooterYerarsString = LanguageManager.valueUnits[0];
            pagesAndFields.FootersPlaceholer = LanguageManager.footerPlaceholder;
            userDataButtons.SaveButtonString = LanguageManager.saveButton;
            userDataButtons.CancelButtonString = LanguageManager.cancelButton;
            levelPhrase = LanguageManager.levelPhrase;*/
        }

        public void Start()
        {
            ValuesAssignment();
            transition.EndClip();
            userDataButtons.NextButtonInteraction = false;

        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            if (pagesAndFields.IsNull())
                pagesAndFields = FindAnyObjectByType<PagesAndFields>();
            if (userDataButtons.IsNull())
                userDataButtons = FindAnyObjectByType<UserDataButtons>();
            if (transition.IsNull())
                transition = GameObject.FindGameObjectWithTag(Tag.Transition).GetComponent<Transition>();

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void PagesSavePreferences(int page)
        {
            switch (page)
            {
                case PAGE1:
                    UserName = pagesAndFields.UserName;
                    Debug.Log("Page1: UserName = " + UserName);
                    break;
                case PAGE2:
                    IsMale = userDataButtons.Genre;
                    Debug.Log("Page2: Genre = " + (IsMale ? MALE : FEMALE));
                    break;
                case PAGE3:
                    Goal = userDataButtons.Goal;
                    userDataButtons.LevelButtonsBehaviour(2);
                    Debug.Log("Page3: Goal = " + Goal);
                    break;
                case PAGE4:
                    Level = userDataButtons.Level;
                    LevelValue = userDataButtons.LevelValue;
                    Debug.Log("Page4: Level = " + Level);
                    break;
                case PAGE5:
                    Focus = userDataButtons.Focus;
                    Debug.Log("Page5: Focus = " + Focus);
                    break;
                case PAGE6:
                    MoreEdit = pagesAndFields.FooterArray;
                    Debug.Log("Page6: Age = " + MoreEdit[0]);
                    Debug.Log("Page6: Current Weight = " + MoreEdit[1]);
                    Debug.Log("Page6: Weight Goal = " + MoreEdit[2]);
                    Debug.Log("Page6: Height = " + MoreEdit[3]);
                    break;
            }
        }

        [ContextMenu("Values Assignment")]
        private void ValuesAssignment()
        {
            userName = UserName;
            gender = (IsMale ? MALE : FEMALE);
            goal = Goal;
            level = Level;
            focus = Focus;
            age = MoreEdit[0];
            currentWeight = MoreEdit[1];
            weightGoal = MoreEdit[2];
            height = MoreEdit[3];
            language = LanguageByindex;

            username = AccountData.Username;
            password = AccountData.Password;
        }

        public string UserName
        {
            get => PlayerPrefs.String.Get(pageKey[0]);
            private set
            {
                PlayerPrefs.String.Set(pageKey[0], userName = value);
            }
        }

        public bool IsMale
        {
            get => PlayerPrefs.Boolean.Get(pageKey[1]);
            private set
            {
                PlayerPrefs.Boolean.Set(pageKey[1], value);
                gender = value ? MALE : FEMALE;
            }
        }

        public string Goal
        {
            get => PlayerPrefs.String.Get(pageKey[2]);
            private set => PlayerPrefs.String.Set(pageKey[2], goal = value);
        }

        public string Level
        {
            get => PlayerPrefs.String.Get(pageKey[3]);
            private set => PlayerPrefs.String.Set(pageKey[3], level = value);
        }

        public int LevelValue
        {
            get => PlayerPrefs.Integer.Get(AccountData.levelValue);
            set => PlayerPrefs.Integer.Set(AccountData.levelValue, value);
        }

        public string Focus
        {
            get => PlayerPrefs.String.Get(pageKey[4]);
            private set => PlayerPrefs.String.Set(pageKey[4], focus = value);
        }

        public string[] MoreEdit
        {
            get
            {
                string[] data = new string[footerKey.Length];

                for (int i = 0; i < footerKey.Length; i++)
                {
                    data[i] = PlayerPrefs.String.Get(footerKey[i]);
                }

                return data;
            }

            set
            {
                for (int i = 0; i < footerKey.Length; i++)
                {
                    PlayerPrefs.String.Set(footerKey[i], value[i]);
                    _set(i);
                }

                void _set(int i)
                {
                    switch (i)
                    {
                        case 0:
                            age = value[0];
                            break;
                        case 1:
                            currentWeight = value[1];
                            break;
                        case 2:
                            weightGoal = value[2];
                            break;
                        case 3:
                            height = value[3];
                            break;
                    }
                }
            }
        }

        private string LanguageByindex => AccountData.Language switch
        {
            0 => "English",
            1 => "Korean",
            2 => "Portuguese BR",
            _ => "English",
        };
    }
}
