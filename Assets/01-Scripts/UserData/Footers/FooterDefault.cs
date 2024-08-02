using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;

namespace TreinAR.UserData.Footers
{
    public class FooterDefault : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private TMP_InputField inputField;
        [SerializeField, ReadOnly] private CanvasGroup canvasGroup;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private int footerIndex;
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
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            footerIndex = transform.GetSiblingIndex();
            this.GetComponentInChildrenIfNull(ref inputField);
            this.GetComponentIfNull(ref canvasGroup);
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SetVisible(bool value)
        {
            canvasGroup.alpha = value.ToInt();
            canvasGroup.blocksRaycasts = value;
            canvasGroup.interactable = value;
        }

        public int Index => footerIndex;
    }
}

