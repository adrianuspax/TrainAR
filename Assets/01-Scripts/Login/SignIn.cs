using ASP;
using ASP.Extensions;
using TMPro;
using TreinAR.DataManager;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.Login
{
    public class SignIn : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [Space(-10, order = 3)]
        [Header(Header.buttons, order = 4)]
        [SerializeField, ReadOnly] private Button signInButton;
        [SerializeField, ReadOnly] private Button signUpButton;
        [Space(-10, order = 0)]
        [Header("Input", order = 1)]
        [SerializeField, ReadOnly] private TMP_InputField usernameInput;
        [SerializeField, ReadOnly] private TMP_InputField passwordInput;
        [Space(-10, order = 0)]
        [Header(Header.TMPro, order = 1)]
        [SerializeField, ReadOnly] private TextMeshProUGUI warningTMP;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 2)]
        [Space(-10, order = 3)]
        [Header("DATA INPUTED", order = 4)]
        [SerializeField, ReadOnly] private string username;
        [SerializeField, ReadOnly] private string password;
        [Space(-10, order = 1)]
        [Header("fix behaviour strings", order = 2)]
        [SerializeField, ReadOnly] private string oldEditTextUsername;
        [SerializeField, ReadOnly] private string editTextUsername;
        [SerializeField, ReadOnly] private string oldEditTextPassword;
        [SerializeField, ReadOnly] private string editTextPassword;
        [Space(-10, order = 0)]
        [Header("Error", order = 1)]
        [SerializeField, ReadOnly] private string fieldEmptyWarning;
        [SerializeField, ReadOnly] private string incorrectWarning;
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private bool keepOldTextUsername;
        [SerializeField, ReadOnly] private bool keepOldTextPassword;

        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            signInButton.onClick.AddListener(() => SignInFunction());
            signUpButton.onClick.AddListener(() => SignOnFunction());
            ///username
            usernameInput.onSelect.AddListener(OnSelectUsername);
            usernameInput.onDeselect.AddListener(OnDeselectUsername);
            usernameInput.onValueChanged.AddListener(OnValueChangedUsername);
            usernameInput.onEndEdit.AddListener(OnEndEditUsername);
            usernameInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusUsername);
            ///pssword
            passwordInput.onSelect.AddListener(OnSelectPassword);
            passwordInput.onDeselect.AddListener(OnDeselectPassword);
            passwordInput.onValueChanged.AddListener(OnValueChangedPassword);
            passwordInput.onEndEdit.AddListener(OnEndEditPassword);
            passwordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusPassword);
        }

        private void Start()
        {
            warningTMP.text = string.Empty;
            LocalizationManager.SetLocalizationString("ThereAreEmptyFields", "StringTableMain", (result) => fieldEmptyWarning = result);
            LocalizationManager.SetLocalizationString("WrongUsernameOrPassword", "StringTableMain", (result) => incorrectWarning = result);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            if (signInButton.IsNull() || signUpButton.IsNull())
            {
                Button[] buttons = GetComponentsInChildren<Button>();
                signInButton = buttons[0];
                signUpButton = buttons[1];
            }

            if (usernameInput.IsNull() || passwordInput.IsNull())
            {
                TMP_InputField[] inputs = GetComponentsInChildren<TMP_InputField>();
                usernameInput = inputs[0];
                passwordInput = inputs[1];
            }

            this.GetComponentIfNull(ref warningTMP, 0, 3, 0);

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SignInFunction()
        {
            bool conditional = username == AccountData.Username && password == AccountData.Password && AccountData.HasAccount;

            if (conditional)
            {
                AccountData.Signed = true;

                if (SceneManager.GetPreviousSceneSaved() == SceneManager.signUp)
                    SceneManager.LoadUserData();
                else
                    SceneManager.LoadTrainAR();
            }
            else
            {
                warningTMP.text = (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text)) ? fieldEmptyWarning : incorrectWarning;
            }
        }

        public void SignOnFunction()
        {
            SceneManager.LoadSignUp();
        }

        public void OnSelectUsername(string username)
        {
            this.username = username;
            warningTMP.text = string.Empty;
        }

        public void OnDeselectUsername(string username)
        {
            this.username = username;
        }

        public void OnValueChangedUsername(string username)
        {
            this.username = username;
            oldEditTextUsername = editTextUsername;
            editTextUsername = username;
        }

        public void OnEndEditUsername(string username)
        {
            this.username = username;

            if (keepOldTextUsername && !string.IsNullOrEmpty(oldEditTextUsername))
            {
                editTextUsername = oldEditTextUsername;
                usernameInput.text = oldEditTextUsername;
                this.username = oldEditTextUsername;
                keepOldTextUsername = false;
            }

            if (usernameInput.wasCanceled)
            {
                usernameInput.placeholder.gameObject.SetActive(false);
            }
        }

        public void ReportChangeStatusUsername(TouchScreenKeyboard.Status newStatus)
        {
            if (newStatus == TouchScreenKeyboard.Status.Canceled)
                keepOldTextUsername = true;
        }

        public void OnSelectPassword(string password)
        {
            this.password = password;
            warningTMP.text = string.Empty;
        }

        public void OnDeselectPassword(string password)
        {
            this.password = password;
        }

        public void OnValueChangedPassword(string password)
        {
            this.password = password;
            oldEditTextPassword = editTextPassword;
            editTextPassword = password;
        }

        public void OnEndEditPassword(string password)
        {
            this.password = password;

            if (keepOldTextPassword && !string.IsNullOrEmpty(oldEditTextPassword))
            {
                editTextPassword = oldEditTextPassword;
                passwordInput.text = oldEditTextPassword;
                this.password = oldEditTextPassword;
                keepOldTextPassword = false;
            }

            if (passwordInput.wasCanceled)
            {
                passwordInput.placeholder.gameObject.SetActive(false);
            }
        }

        public void ReportChangeStatusPassword(TouchScreenKeyboard.Status newStatus)
        {
            if (newStatus == TouchScreenKeyboard.Status.Canceled)
                keepOldTextPassword = true;
        }
    }
}
