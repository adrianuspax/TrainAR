using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.UserData.Pages
{
    public sealed class Page2 : PageDefault
    {
        [Space(20, order = 0)]
        [SerializeField, NonReorderable, ReadOnly] private Button[] buttons;
        [SerializeField, NonReorderable, ReadOnly] private Image[] icons;
        [SerializeField, NonReorderable, ReadOnly] private Color[] pressedColors;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private bool[] isPressed;
        [SerializeField, ReadOnly] private bool isMale;
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
            this.GetComponentsInAllChildrenIfNull(ref buttons);

            if (icons.IsNullOrEmpty())
            {
                icons = new Image[buttons.Length];
                pressedColors = new Color[buttons.Length];

                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].GetComponentIfNull(ref icons[i], 0);
                    pressedColors[i] = icons[i].color;
                }
            }

            if (isPressed.IsEmpty())
                isPressed = new bool[buttons.Length];

            base.ComponentsAssignment();
            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void ButtonsBehaviour(int index, bool isPressed)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == index && isPressed)
                {
                    buttons[index].image.color = pressedColors[index];
                    icons[index].color = PalleteColors.BlueGray_White;
                }
                else
                {
                    buttons[i].image.color = PalleteColors.BlueGray_White;
                    icons[i].color = pressedColors[i];
                    this.isPressed[i] = false;
                }
            }

            isMale = !index.ToBool();
            Content = isMale ? "Male" : "Female";
            buttonsMain.SetNextButtonInteractable(isPressed);
        }

        public bool IsMale => isMale;
    }
}

