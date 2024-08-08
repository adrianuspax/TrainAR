using ASP;
using ASP.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayerPrefs = ASP.PlayerPrefs;

namespace TreinAR.Menu
{
    public class QuestionPanel : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Animator animator;
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;

        public const int YES = 0;
        public const int NO = 1;
        public const string PARAMETER = "isOpen";

        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            buttons[YES].onClick.AddListener(Yes);
            buttons[NO].onClick.AddListener(No);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentInChildrenIfNull(ref animator);
            this.GetComponentsInAllChildrenIfNull(ref buttons);
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void OpenPanel()
        {
            animator.SetBool(PARAMETER, true);
        }

        public void Yes()
        {
            float time;
            animator.SetBool(PARAMETER, false);
            time = animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(_delayCallScene(time));
            PlayerPrefs.DeleteAll();

            static IEnumerator _delayCallScene(float time)
            {
                yield return new WaitForSeconds(time);
                SceneManager.LoadRedirection();
            }
        }

        public void No()
        {
            animator.SetBool(PARAMETER, false);
        }
    }
}

