using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.UserData.Pages
{
    public sealed class Page4 : PageDefault
    {
        [Space(20, order = 0)]
        [SerializeField, ReadOnly] private TextMeshProUGUI fitnessLevelTMP;
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private bool[] isPressed;
        [SerializeField, ReadOnly] private int index;

        public readonly string[] entry = new string[] { "Sedentary", "Walker", "Runner", "Athletic", "BodyBuilder" };

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
                int index = i;
                buttons[index].onClick.AddListener(() => ButtonsBehaviour(index));
            }
        }
        /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        private void Start()
        {
            ButtonsBehaviour(0);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private new void ComponentsAssignment()
        {
            if (buttons.IsNullOrEmpty())
            {
                buttons = GetComponentsInChildren<Button>();
                isPressed = new bool[buttons.Length];
            }

            animator.GetComponentIfNull(ref fitnessLevelTMP, 2);

            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void ButtonsBehaviour(int index)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                bool conditional = (i <= index);
                buttons[i].image.color = conditional ? PalleteColors.Main : PalleteColors.BlueGray_WhiteBlue;
                isPressed[i] = conditional;
            }

            this.index = index;
            fitnessLevelTMP.SetLocalizationString(entry[index], "StringTableMain");
            buttonsMain.SetNextButtonInteractable(true);
            Content = fitnessLevelTMP.text;
        }

        public int Index => index;
    }
}

