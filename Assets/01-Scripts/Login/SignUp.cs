using ASP;
using ASP.Extensions;
using TMPro;
using TreinAR.DataManager;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.Login
{
    public class SignUp : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header("Buttons", order = 2)]
        [SerializeField, ReadOnly] private Button okButton;
        [SerializeField, ReadOnly] private Button returnButton;
        [Space(-10, order = 0)]
        [Header("Input", order = 1)]
        [SerializeField, ReadOnly] private TMP_InputField newUsernameInput;
        [SerializeField, ReadOnly] private TMP_InputField newPasswordInput;
        [SerializeField, ReadOnly] private TMP_InputField repeatPasswordInput;
        [Space(-10, order = 0)]
        [Header("TMP", order = 1)]
        [SerializeField, ReadOnly] private TextMeshProUGUI warningTMP;
        [SerializeField, ReadOnly] private TextMeshProUGUI okButtonTMP;
        [Space(-10, order = 1)]
        [Header("DATA INPUTED", order = 2)]
        [SerializeField, ReadOnly] private string username;
        [SerializeField, ReadOnly] private string newPassword;
        [SerializeField, ReadOnly] private string repeatPassword;
        [Space(-10, order = 1)]
        [Header("fix behaviour strings", order = 2)]
        [SerializeField, ReadOnly] private string oldEditTextNewUsername;
        [SerializeField, ReadOnly] private string editTextNewUsername;
        [SerializeField, ReadOnly] private string oldEditTextNewPassword;
        [SerializeField, ReadOnly] private string editTextNewPassword;
        [SerializeField, ReadOnly] private string oldEditTextRepeatPassword;
        [SerializeField, ReadOnly] private string editTextRepeatPassword;
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private string passwordsDontMatch;
        [SerializeField, ReadOnly] private string alreadyIDWarning;
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private bool usernameReleased;
        [SerializeField, ReadOnly] private bool passwordReleased;
        [SerializeField, ReadOnly] private bool buttonOkRealeased;
        [SerializeField, ReadOnly] private bool keepOldTextNewUsername;
        [SerializeField, ReadOnly] private bool keepOldTextNewPassword;
        [SerializeField, ReadOnly] private bool keepOldTextRepeatPassword;

        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            newUsernameInput.onSelect.AddListener(OnSelectNewUsername);
            newUsernameInput.onDeselect.AddListener(OnDeselectNewUsername);
            newUsernameInput.onValueChanged.AddListener(OnValueChangedNewUsername);
            newUsernameInput.onEndEdit.AddListener(OnEndEditNewUsername);
            newUsernameInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusNewUsername);

            newPasswordInput.onSelect.AddListener(OnSelectNewPassword);
            newPasswordInput.onDeselect.AddListener(OnDeselectNewPassword);
            newPasswordInput.onValueChanged.AddListener(OnValueChangedNewPassword);
            newPasswordInput.onEndEdit.AddListener(OnEndEditNewPassword);
            newPasswordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusNewPassword);

            repeatPasswordInput.onSelect.AddListener(OnSelectRepeatPassword);
            repeatPasswordInput.onDeselect.AddListener(OnDeselectRepeatPassword);
            repeatPasswordInput.onValueChanged.AddListener(OnValueChangedRepeatPassword);
            repeatPasswordInput.onEndEdit.AddListener(OnEndEditRepeatPassword);
            repeatPasswordInput.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusRepeatPassword);

            okButton.onClick.AddListener(() => Ok());
            returnButton.onClick.AddListener(() => Return());
        }

        private void Start()
        {
            LocalizationManager.SetLocalizationString("PasswordsDontMatch", "StringTableMain", (result) => passwordsDontMatch = result);
            LocalizationManager.SetLocalizationString("ThisUsernameIsAlreadyUsed", "StringTableMain", (result) => alreadyIDWarning = result);
            warningTMP.text = string.Empty;
            okButton.interactable = false;
            okButtonTMP.SetAlpha(0.5f);
        }
        /// <summary>
            /// Assignment of components and variables
            /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            if (okButton.IsNull() || returnButton.IsNull())
            {
                Button[] buttons = GetComponentsInChildren<Button>();
                okButton = buttons[0];
                returnButton = buttons[1];
            }

            if (newUsernameInput.IsNull() || newPasswordInput.IsNull() || repeatPasswordInput.IsNull())
            {
                TMP_InputField[] inputs = GetComponentsInChildren<TMP_InputField>();
                newUsernameInput = inputs[0];
                newPasswordInput = inputs[1];
                repeatPasswordInput = inputs[2];
            }

            okButton.GetComponentInChildrenIfNull(ref okButtonTMP);
            this.GetComponentIfNull(ref warningTMP, 0, 2, 0);

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }
        #region BUTTONS
        public void Ok()
        {
            if (buttonOkRealeased)
            {
                AccountData.Username = username;
                AccountData.Password = newPassword;

                if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
                    TrainARSceneManager.LoadMenu();
                else
                    TrainARSceneManager.LoadSignIn();
            }
        }

        public void Return()
        {
            if (buttonOkRealeased)
            {
                AccountData.Username = username;
                AccountData.Password = newPassword;

                if (TrainARSceneManager.GetPreviousSceneSaved() == TrainARSceneManager.menu)
                    TrainARSceneManager.LoadMenu();
                else
                    TrainARSceneManager.LoadSignIn();
            }
        }
        #endregion
        #region newUsername
        public void OnSelectNewUsername(string newUsername)
        {
            username = newUsername;
            warningTMP.text = string.Empty;
        }

        public void OnDeselectNewUsername(string newUsername)
        {
            username = newUsername;
        }

        public void OnValueChangedNewUsername(string newUsername)
        {
            username = newUsername;
            warningTMP.text = string.Empty;
            oldEditTextNewUsername = editTextNewUsername;
            editTextNewUsername = newUsername;
        }

        public void OnEndEditNewUsername(string newUsername)
        {
            if (keepOldTextNewUsername && !string.IsNullOrEmpty(oldEditTextNewUsername))
            {
                editTextNewUsername = oldEditTextNewUsername;
                newUsernameInput.text = oldEditTextNewUsername;
                username = oldEditTextNewUsername;
                keepOldTextNewUsername = false;
            }

            if (AccountData.HasUsername)
            {
                if (newUsername == AccountData.Username)
                    warningTMP.text = alreadyIDWarning;
                else if (string.IsNullOrEmpty(newUsernameInput.text))
                    warningTMP.text = string.Empty;

                usernameReleased = !(newUsername == AccountData.Username);
            }
            else if (string.IsNullOrEmpty(newUsernameInput.text))
            {
                warningTMP.text = string.Empty; 
            }
            else
            {
                usernameReleased = true; 
            }

            username = newUsername;
            ValidationPasswords();
            ReleaseValidation();
        }

        public void ReportChangeStatusNewUsername(TouchScreenKeyboard.Status newStatus)
        {
            if (newStatus == TouchScreenKeyboard.Status.Canceled)
                keepOldTextNewUsername = true;
        }
        #endregion
        #region newPassword
        public void OnSelectNewPassword(string newPassword)
        {
            this.newPassword = newPassword;
        }

        public void OnDeselectNewPassword(string newPassword)
        {
            this.newPassword = newPassword;
        }

        public void OnValueChangedNewPassword(string newPassword)
        {
            this.newPassword = newPassword;
            oldEditTextNewPassword = editTextNewPassword;
            editTextNewPassword = newPassword;
        }

        public void OnEndEditNewPassword(string newPassword)
        {
            this.newPassword = newPassword;

            if (keepOldTextNewPassword && !string.IsNullOrEmpty(oldEditTextNewPassword))
            {
                editTextNewPassword = oldEditTextNewPassword;
                newPasswordInput.text = oldEditTextNewPassword;
                this.newPassword = oldEditTextNewPassword;
                keepOldTextNewPassword = false;
            }

            ValidationPasswords();
            ReleaseValidation();
        }

        public void ReportChangeStatusNewPassword(TouchScreenKeyboard.Status newStatus)
        {
            if (newStatus == TouchScreenKeyboard.Status.Canceled)
                keepOldTextNewPassword = true;
        }
        #endregion
        #region repeatPassword
        public void OnSelectRepeatPassword(string repeatPassword)
        {
            this.repeatPassword = repeatPassword;
        }

        public void OnDeselectRepeatPassword(string repeatPassword)
        {
            this.repeatPassword = repeatPassword;
        }

        public void OnValueChangedRepeatPassword(string repeatPassword)
        {
            this.repeatPassword = repeatPassword;
            oldEditTextRepeatPassword = editTextRepeatPassword;
            editTextRepeatPassword = repeatPassword;
        }

        public void OnEndEditRepeatPassword(string repeatPassword)
        {
            this.repeatPassword = repeatPassword;

            if (keepOldTextRepeatPassword && !string.IsNullOrEmpty(oldEditTextRepeatPassword))
            {
                editTextRepeatPassword = oldEditTextRepeatPassword;
                repeatPasswordInput.text = oldEditTextRepeatPassword;
                this.repeatPassword = oldEditTextRepeatPassword;
                keepOldTextRepeatPassword = false;
            }

            ValidationPasswords();
            ReleaseValidation();
        }

        public void ReportChangeStatusRepeatPassword(TouchScreenKeyboard.Status newStatus)
        {
            if (newStatus == TouchScreenKeyboard.Status.Canceled)
                keepOldTextRepeatPassword = true;
        }

        private void ValidationPasswords()
        {
            if (usernameReleased)
            {
                bool conditional = (string.IsNullOrEmpty(newPasswordInput.text) && string.IsNullOrEmpty(repeatPasswordInput.text)) || (newPassword == repeatPassword);
                warningTMP.text = conditional ? string.Empty : passwordsDontMatch;
            }
        }

        private void ReleaseValidation()
        {
            passwordReleased = (newPassword == repeatPassword) && !string.IsNullOrEmpty(newPasswordInput.text) && !string.IsNullOrEmpty(repeatPasswordInput.text);
            buttonOkRealeased = usernameReleased && passwordReleased;
            okButton.interactable = buttonOkRealeased;
            okButtonTMP.SetAlpha(buttonOkRealeased ? 1f : 0.5f);
        }
        #endregion
    }
}

