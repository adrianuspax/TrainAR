using ASP;
using ASP.Extensions;
using UnityEngine;

namespace TreinAR.DataManager.Pages
{
    public abstract class PageDefault : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.scripts, order = 2)]
        [SerializeField, ReadOnly] protected ButtonsMain buttonsMain;

        [Header(Header.components, order = 0)]
        [SerializeField, ReadOnly] protected Animator animator;
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] protected string content;


        public const string PARAMETER = "isOpen";
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        protected void ComponentsAssignment()
        {
            this.GetComponentInChildrenIfNull(ref animator);
            if (buttonsMain.IsNull())
                buttonsMain = FindAnyObjectByType<ButtonsMain>();
        }

        public float AnimationPlay(bool isOpen)
        {
            animator.SetBool(PARAMETER, isOpen);
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }

        public string Content => content;
    }
}
