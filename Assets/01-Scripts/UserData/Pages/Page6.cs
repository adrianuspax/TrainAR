using ASP;
using ASP.Extensions;
using TreinAR.UserData.Footers;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.UserData.Pages
{
    public sealed class Page6 : PageDefault
    {
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;
        [SerializeField, ReadOnly] private FootersControl footersControl;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                //buttons[i].onClick.AddListener(footersControl.Footers[i].);
            }
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private new void ComponentsAssignment()
        {
            this.GetComponentsInAllChildrenIfNull(ref buttons);
            if (footersControl.IsNull())
                footersControl = FindAnyObjectByType<FootersControl>();

            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }
    }
}

