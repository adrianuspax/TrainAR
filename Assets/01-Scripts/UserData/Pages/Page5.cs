using ASP;
using ASP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.DataManager.Pages
{
    public sealed class Page5 : PageDefault
    {
        [Space(20, order = 0)]
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;
        [SerializeField, NonReorderable, ReadOnly] private TextMeshProUGUI[] tmps;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private bool[] isPressed;
        [SerializeField, ReadOnly] private int index;
        /// Awake is called when an enabled script instance is being loaded.
        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            buttons.AddListners(isPressed, ButtonsBehaviour);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private new void ComponentsAssignment()
        {
            if (buttons.IsNullOrEmpty())
            {
                int length;

                buttons = GetComponentsInChildren<Button>();
                length = buttons.Length;

                tmps = new TextMeshProUGUI[length];
                isPressed = new bool[length];

                for (int i = 0; i < length; i++)
                {
                    tmps[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                }
            }

            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void ButtonsBehaviour(int index, bool isPressed)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == index && isPressed)
                {
                    tmps[index].color = PalleteColorsOld.BlueGray_White;
                    tmps[index].fontStyle = FontStyles.Bold;
                    buttons[index].image.color = PalleteColorsOld.Main;
                }
                else
                {
                    tmps[i].color = PalleteColorsOld.BlueGrey(4);
                    tmps[i].fontStyle = FontStyles.Normal;
                    buttons[i].image.color = PalleteColorsOld.BlueGray_White;
                    this.isPressed[i] = false;
                }
            }

            this.index = index;
            content = tmps[index].text;
            buttonsMain.SetNextButtonInteractable(isPressed);
        }

        public int Index => index;
    }
}
