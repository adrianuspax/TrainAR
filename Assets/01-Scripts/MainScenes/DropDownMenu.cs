using ASP;
using ASP.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TreinAR.MainScenes
{
    public class DropDownMenu : MonoBehaviour
    {
        [Header(Header.READONLY, order = 0)]
        [Space(-10, order = 1)]
        [Header(Header.components, order = 2)]
        [SerializeField, ReadOnly] private Animator aniDropDownMenu;
        [SerializeField, ReadOnly] private ScrollRect dropDownScrollRect;
        [SerializeField, ReadOnly] private Button btnOpenDropDown;
        [SerializeField, ReadOnly] private Button btnCloseDropDown;
        [Space(-10, order = 0)]
        [Header(Header.variables, order = 1)]
        [SerializeField, ReadOnly] private bool panelDropDownOpen;
        [SerializeField, ReadOnly] private Transform buttonsHeader;
        [NonReorderable, SerializeField, ReadOnly] private Button[] gridButtonDDM;
        [NonReorderable, SerializeField, ReadOnly] private bool[] isPressDDM;
        [NonReorderable, SerializeField, ReadOnly] private TextMeshProUGUI[] tmps;
        [Space(-10, order = 0)]
        [Header(Header.colors, order = 1)]
        [SerializeField, ReadOnly] private Color pressedColor;
        [SerializeField, ReadOnly] private Color pressedTxtColor;
        [SerializeField, ReadOnly] private Color unpressedColor;
        [SerializeField, ReadOnly] private Color unpressedTxtColor;

        [Header(Header.scripts, order = 0)]
        [SerializeField, ReadOnly] private ButtonsFloat floatMenu;
        [SerializeField, ReadOnly] private Coach coachAnimations;
        [SerializeField, ReadOnly] private ButtonsMain buttonsMain;

        public const string PARAMETER = "isOpen";
        private bool singlePressed;

        private void Awake()
        {
            ComponentsAssignment();
        }
        /// This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            btnOpenDropDown.onClick.AddListener(() => ButtonOpenDropDown());
            btnCloseDropDown.onClick.AddListener(() => ButtonCloseDropDown());
            gridButtonDDM.AddListners(isPressDDM, ButtonsDDMBehaviour);
        }
        /// <summary>
        /// Assignment of components and variables
        /// </summary>
        [ContextMenu("Components Assignment")]
        private void ComponentsAssignment()
        {
            this.GetComponentIfNull(ref aniDropDownMenu);
            this.GetComponentIfNull(ref dropDownScrollRect, 0, 0, 0);
            this.GetComponentIfNull(ref btnOpenDropDown, 0, 0, 1);
            dropDownScrollRect.GetComponentIfNull(ref btnCloseDropDown, 0);
            dropDownScrollRect.GetComponentIfNull(ref buttonsHeader, 1, 0);
            if (floatMenu.IsNull())
                floatMenu = FindAnyObjectByType<ButtonsFloat>();
            if (coachAnimations.IsNull())
                coachAnimations = FindAnyObjectByType<Coach>();
            if (buttonsMain.IsNull())
                buttonsMain = FindAnyObjectByType<ButtonsMain>();
            if (gridButtonDDM.IsNullOrEmpty())
                this.GetComponentsInAllChildrenIfNull(ref gridButtonDDM);
            if (tmps.IsNullOrEmpty())
            {
                tmps = new TextMeshProUGUI[buttonsHeader.childCount];

                for (int i = 0; i < tmps.Length; i++)
                {
                    tmps[i] = buttonsHeader.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
                }
            }

            pressedColor = PalleteColors.DoubleSplitComplementary(2);
            pressedTxtColor = PalleteColors.Square(1);
            unpressedColor = gridButtonDDM[0].image.color;
            unpressedTxtColor = gridButtonDDM[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;

            ASP.SceneManagement.SceneManager.SaveCurrentScene();
        }

        public void ButtonOpenDropDown()
        {
            if (panelDropDownOpen)
            {
                return;
            }
            else
            {
                panelDropDownOpen = true;
                aniDropDownMenu.SetBool(PARAMETER, true);
                StartCoroutine(_scrowViewPos(aniDropDownMenu.GetCurrentAnimatorStateInfo(0).length));
                floatMenu.ButtonsFloatBehaviour(ButtonsFloat.BodyPart.All);
                floatMenu.SetBoolFloatAnimators(false);
            }

            IEnumerator _scrowViewPos(float time)
            {
                yield return new WaitForSeconds(time);
                dropDownScrollRect.verticalNormalizedPosition = 0.5f;
            }
        }

        public void ButtonCloseDropDown()
        {
            bool conditional =
        coachAnimations.CurrentAnimation.isWorkout ||
        coachAnimations.CurrentAnimation.isEnter ||
        coachAnimations.NextAnimation.willBeWorkout ||
        coachAnimations.NextAnimation.willBeEnter;

            if (panelDropDownOpen)
            {
                panelDropDownOpen = false;
                aniDropDownMenu.SetBool(PARAMETER, false);
                floatMenu.SetBoolFloatAnimators(true);
                buttonsMain.PanelCoachInteraction = true;

                if (conditional)
                {
                    coachAnimations.StartIdleAnimation();
                    SetAllButtonsDefault();
                }
            }
        }

        private void ButtonsDDMBehaviour(int index, bool isPressed)
        {
            Vector3 deslocation;

            for (int i = 0; i < gridButtonDDM.Length; i++)
            {
                if (i == index && isPressed)
                {
                    deslocation.x = gridButtonDDM[index].transform.localPosition.x;
                    deslocation.y = gridButtonDDM[index].transform.localPosition.y;
                    deslocation.z = -30f;

                    tmps[index].color = pressedTxtColor;
                    gridButtonDDM[index].image.color = pressedColor;
                    gridButtonDDM[index].transform.localPosition = deslocation;
                    coachAnimations.StartAnimation(index + 3);
                }
                else
                {
                    deslocation.x = gridButtonDDM[i].transform.localPosition.x;
                    deslocation.y = gridButtonDDM[i].transform.localPosition.y;
                    deslocation.z = 0f;

                    tmps[i].color = unpressedTxtColor;
                    gridButtonDDM[i].image.color = unpressedColor;
                    gridButtonDDM[i].transform.localPosition = deslocation;
                    gridButtonDDM[i].interactable = !isPressed;
                    tmps[i].SetAlpha(!isPressed ? 1f : 0.5f);
                    isPressDDM[i] = false;
                }
            }

            if (!isPressed)
                coachAnimations.StartIdleAnimation();

            IsPressed = isPressed;
        }

        public void SetAllButtonsDefault()
        {
            Vector3 deslocation;

            for (int i = 0; i < buttonsHeader.childCount; i++)
            {
                deslocation.x = gridButtonDDM[i].transform.localPosition.x;
                deslocation.y = gridButtonDDM[i].transform.localPosition.y;
                deslocation.z = 0f;

                tmps[i].color = unpressedTxtColor;
                tmps[i].SetAlpha(1f);
                gridButtonDDM[i].image.color = unpressedColor;
                gridButtonDDM[i].transform.localPosition = deslocation;
                gridButtonDDM[i].interactable = true;
                isPressDDM[i] = false;
            }
        }

        public bool Interactable
        {
            get => btnOpenDropDown.interactable;
            set => btnOpenDropDown.interactable = value;
        }

        public bool IsPressed
        {
            get => singlePressed;
            private set => singlePressed = value;
        }

        public GameObject[] ButtonGameObjects
        {
            get
            {
                GameObject[] gameObjects = new GameObject[buttonsHeader.childCount];

                for (int i = 0; i < buttonsHeader.childCount; i++)
                {
                    gameObjects[i] = gridButtonDDM[i].gameObject;
                }

                return gameObjects;
            }
        }
    }
}
