using ASP;
using ASP.Extensions;
using System.Collections;
using TMPro;
using TreinAR.UserData.Pages;
using UnityEngine;
using UnityEngine.UI;
using Debug = ASP.Debug;

namespace TreinAR.UserData.Footers
{
    public class FootersControl : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Animator animator;
        [SerializeField, ReadOnly] private Button saveButton;
        [SerializeField, ReadOnly] private Button cancelButton;
        [SerializeField, ReadOnly] private TextMeshProUGUI saveTMP;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private int currentFooterIndex;

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
        [SerializeField, ReadOnly] private Page6 page6;
        [SerializeField, NonReorderable, ReadOnly] private Footer[] footers;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            saveButton.onClick.AddListener(SetSaveBehaviour);
            cancelButton.onClick.AddListener(SetCancelBehaviour);
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void Start()
        {
            SetAllFootersVisible(false);
            SetSaveButtonInteractable(false);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentInChildrenIfNull(ref animator);
            animator.GetComponentIfNull(ref saveButton, 0, 0, 1, 0);
            animator.GetComponentIfNull(ref cancelButton, 0, 0, 1, 1);
            this.GetComponentsInAllChildrenIfNull(ref footers);
            saveButton.GetComponentInChildrenIfNull(ref saveTMP);
            if (buttonsMain.IsNull())
                buttonsMain = FindAnyObjectByType<ButtonsMain>();
            if (page6.IsNull())
                page6 = FindAnyObjectByType<Page6>();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SetCallFooter(int index)
        {
            if (index >= 0 && index < footers.Length)
            {
                SetSaveButtonInteractable(false);
                animator.SetTrigger(Parameter.enter);
                SetAllFootersVisible(false);
                footers[index].SetVisible(true);
                currentFooterIndex = index;
            }
            else
            {
                Debug.LogError($"The value of {nameof(index)} must be between 0 and {footers.Length - 1}, but {index} has been inserted!");
            }
        }

        public void SetSaveBehaviour()
        {
            Footer currentFooter = footers[currentFooterIndex];
            SetSaveButtonInteractable(false);
            animator.SetTrigger(Parameter.save);
            currentFooter.isFilled = true;
            page6.SetSaveInfoValue(currentFooterIndex, currentFooter.Content);

            for (int i = 0; i < footers.Length; i++)
            {
                if (!footers[i].isFilled)
                    return;
            }

            buttonsMain.SetNextButtonInteractable(true);
        }

        public void SetCancelBehaviour()
        {
            StartCoroutine(_call());

            IEnumerator _call()
            {
                Footer currentFooter = footers[currentFooterIndex];
                float s = animator.GetCurrentAnimatorStateInfo(0).length;
                animator.SetTrigger(Parameter.cancel);
                currentFooter.SetClearInputField();
                currentFooter.isFilled = false;
                page6.SetSaveInfoValue(currentFooterIndex, "--");
                buttonsMain.SetNextButtonInteractable(false);
                yield return new WaitForSeconds(s);
                SetAllFootersVisible(false);
            }
        }

        public void SetSaveButtonInteractable(bool value)
        {
            if (saveButton.IsInteractable() == value)
                return;

            saveButton.interactable = value;
            saveTMP.SetAlpha(value ? 1f : 0.5f);
        }

        private void SetAllFootersVisible(bool value)
        {
            foreach (Footer footer in footers)
                footer.SetVisible(value);
        }

        public Footer[] Footers => footers;
        public (string enter, string cancel, string save) Parameter => ("enter", "cancel", "save");
    }
}
