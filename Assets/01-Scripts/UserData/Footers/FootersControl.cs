using ASP;
using ASP.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField, NonReorderable, ReadOnly] private FooterDefault[] footers;
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            saveButton.onClick.AddListener(SaveButtonFunction);
            cancelButton.onClick.AddListener(CancelButtonFunction);
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void Start()
        {
            currentFooterIndex = 0;

            foreach (FooterDefault footer in footers)
            {
                footer.SetVisible(false);
            }
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
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void SaveButtonFunction()
        {
            /*StartCoroutine(_delay());

            IEnumerator _delay()
            {
                float s = animator.GetCurrentAnimatorStateInfo(0).length;

                if (currentFooterIndex < footers.Length - 1)
                {
                    SaveButtonInteractable(false);
                    animator.SetTrigger(Parameter.save);
                    yield return new WaitForSeconds(s);
                    footers[currentFooterIndex].SetVisible(false);
                    currentFooterIndex++;
                    footers[currentFooterIndex].SetVisible(true);
                    animator.SetTrigger(Parameter.enter);
                }
                else
                {
                    buttonsMainUserData.SetNextButtonInteractable(true);
                }
            }*/
        }

        public void CallFooter(int index)
        {
            StartCoroutine(_delay());

            IEnumerator _delay()
            {
                float s = animator.GetCurrentAnimatorStateInfo(0).length;

                if (currentFooterIndex < footers.Length - 1)
                {
                    SaveButtonInteractable(false);
                    animator.SetTrigger(Parameter.enter);
                    yield return new WaitForSeconds(s);
                    footers[currentFooterIndex].SetVisible(false);
                    footers[index].SetVisible(true);
                    currentFooterIndex = index;
                    animator.SetTrigger(Parameter.enter);
                }
                else
                {
                    //buttonsMainUserData.SetNextButtonInteractable(true);
                }
            }
        }

        public void CancelButtonFunction()
        {
            animator.SetTrigger(Parameter.cancel);
        }

        public void SaveButtonInteractable(bool value)
        {
            if (saveButton.IsInteractable() == value)
                return;

            saveButton.interactable = value;
            saveTMP.SetAlpha(value ? 1f : 0.5f);
        }

        public FooterDefault[] Footers => footers;
        public (string enter, string cancel, string save) Parameter => ("enter", "cancel", "save");
    }
}

