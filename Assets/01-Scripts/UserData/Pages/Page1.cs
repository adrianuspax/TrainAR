using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;

namespace TreinAR.UserData.Pages
{
    public sealed class Page1 : PageDefault
    {
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private TMP_InputField inputField;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            inputField.onSelect.AddListener(OnSelect);
            inputField.onDeselect.AddListener(OnDeselect);
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.onEndEdit.AddListener(OnEndEdit);
            //inputField.onTouchScreenKeyboardStatusChanged.AddListener(OnTouchScreenKeyboardStatusChanged);
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void Start()
        {
            NextButtonInteraction();
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private new void ComponentsAssignment()
        {
            this.GetComponentInChildrenIfNull(ref inputField);
            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }
        /// <summary>
        /// Method that can be called from the context menu in the Inpector for function tests
        /// </summary>
        [ContextMenu("Test")]
        public void Test()
        {
            return;
        }


        public void OnSelect(string content)
        {
            Content = content;
        }

        public void OnDeselect(string content)
        {
            NextButtonInteraction();
            Content = content;
        }

        public void OnValueChanged(string content)
        {
            NextButtonInteraction();
            Content = content;
        }

        public void OnEndEdit(string content)
        {
            NextButtonInteraction();
            Content = content;
        }

        /*public void OnTouchScreenKeyboardStatusChanged(TouchScreenKeyboard.Status status)
        {

        }*/

        public void NextButtonInteraction()
        {
            bool isFilled = !string.IsNullOrEmpty(inputField.text);
            buttonsMain.SetNextButtonInteractable(isFilled);
        }
    }
}

