using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;

namespace TreinAR.UserData.Footers
{
    public sealed class Footer : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private TMP_InputField inputField;
        [SerializeField, ReadOnly] private CanvasGroup canvasGroup;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private int footerIndex;
        [SerializeField, ReadOnly] private string content;
        [ReadOnly] public bool isFilled;

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private FootersControl footersControl;
        /// Reset to default values.
        private void Reset()
        {
            ComponentsAssignment();
        }
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
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            footerIndex = transform.GetSiblingIndex();
            this.GetComponentInChildrenIfNull(ref inputField);
            this.GetComponentIfNull(ref canvasGroup);
            if (footersControl.IsNull())
                footersControl = FindAnyObjectByType<FootersControl>();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SetVisible(bool value)
        {
            canvasGroup.alpha = value.ToInt();
            canvasGroup.blocksRaycasts = value;
            canvasGroup.interactable = value;
        }

        public void SetClearInputField()
        {
            inputField.text = string.Empty;
        }

        public void OnSelect(string content)
        {
            SetSaveButtonInteractableByContent(content);
            this.content = content;
        }

        public void OnDeselect(string content)
        {
            SetSaveButtonInteractableByContent(content);
            this.content = content;
        }

        public void OnValueChanged(string content)
        {
            SetSaveButtonInteractableByContent(content);
            this.content = content;
        }

        public void OnEndEdit(string content)
        {
            SetSaveButtonInteractableByContent(content);
            this.content = content;
        }

        /*public void OnTouchScreenKeyboardStatusChanged(TouchScreenKeyboard.Status status)
        {

        }*/

        public void SetSaveButtonInteractableByContent(string content)
        {
            bool isFilled = !string.IsNullOrEmpty(content);
            footersControl.SetSaveButtonInteractable(isFilled);
        }

        public int Index => footerIndex;
        public string Content => content;
    }
}

