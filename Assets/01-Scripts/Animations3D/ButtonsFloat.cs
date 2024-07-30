using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsFloat : MonoBehaviour
{
    public enum BodyPart
    {
        Chest = 0,
        Abdomen = 1,
        Legs = 2,
        Arms = 3,
        All = 4
    }

    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, NonReorderable, ReadOnly] private Button[] floatButtons;
    [SerializeField, NonReorderable, ReadOnly] private Transform[] floatTransforms;
    [SerializeField, NonReorderable, ReadOnly] private Canvas[] floatCanvas;
    [SerializeField, NonReorderable, ReadOnly] private Animator[] floatAnimators;

    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    public const string PARAMETER = "floatIsOpen";

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void OnEnable()
    {
        floatButtons[(int)BodyPart.Chest].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Chest));
        floatButtons[(int)BodyPart.Abdomen].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Abdomen));
        floatButtons[(int)BodyPart.Legs].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Legs));
        floatButtons[(int)BodyPart.Arms].onClick.AddListener(() => ButtonsFloatBehaviour(BodyPart.Arms));
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        int count = transform.childCount;

        if (buttonsMain.IsNull())
            buttonsMain = FindAnyObjectByType<ButtonsMain>();
        if (dropDownMenu.IsNull())
            dropDownMenu = FindAnyObjectByType<DropDownMenu>();
        if (floatAnimators.IsNullOrEmpty())
            floatAnimators = _assignAnimators(transform);
        if (floatTransforms.IsNullOrEmpty())
            floatTransforms = _assignComponents<Transform>(floatAnimators, 0);
        if (floatCanvas.IsNullOrEmpty())
            floatCanvas = _assignComponents<Canvas>(floatTransforms, 0, 1);
        if (floatButtons.IsNullOrEmpty())
            floatButtons = _assignComponents<Button>(floatCanvas, 0);

        ASP.SceneManagement.SceneManager.SaveCurrentScene();

        Animator[] _assignAnimators(Transform header)
        {
            Animator[] components = new Animator[count];

            for (int i = 0; i < count; i++)
            {
                header.GetComponentIfNull(ref components[i], i, 0);
            }

            return components;
        }

        T[] _assignComponents<T>(Component[] components, params int[] indexChild) where T : Component
        {
            T[] returnedComponents = new T[count];

            for (int i = 0; i < count; i++)
            {
                components[i].GetComponentIfNull(ref returnedComponents[i], indexChild);
            }

            return returnedComponents;
        }
    }

    public void ButtonsFloatBehaviour(BodyPart bodyPart)
    {
        bool buttonsToActivate;

        _buttonsFloatFunction();

        for (int i = 0; i < dropDownMenu.ButtonGameObjects.Length; i++)
        {
            buttonsToActivate = bodyPart switch
            {
                BodyPart.Chest => _chest(i),
                BodyPart.Abdomen => _abdomen(i),
                BodyPart.Legs => _legs(i),
                BodyPart.Arms => _arms(i),
                BodyPart.All => true,
                _ => false,
            };

            dropDownMenu.ButtonGameObjects[i].SetActive(buttonsToActivate);
        }

        bool _chest(int i)
        {
            return i == DropDownMenuIndex.BURPEE ||
                   i == DropDownMenuIndex.BICYCLECRUNCH ||
                   i == DropDownMenuIndex.CIRCLECRUNCH ||
                   i == DropDownMenuIndex.PIKEWALK ||
                   i == DropDownMenuIndex.PUSHUP ||
                   i == DropDownMenuIndex.SITUPS;
        }

        bool _abdomen(int i)
        {
            return i == DropDownMenuIndex.BURPEE ||
                   i == DropDownMenuIndex.BICYCLECRUNCH ||
                   i == DropDownMenuIndex.PIKEWALK ||
                   i == DropDownMenuIndex.PUSHUP ||
                   i == DropDownMenuIndex.SITUPS;
        }

        bool _legs(int i)
        {
            return i == DropDownMenuIndex.SQUAT ||
                   i == DropDownMenuIndex.SQUATBENTARMS ||
                   i == DropDownMenuIndex.BOXJUMP ||
                   i == DropDownMenuIndex.BURPEE ||
                   i == DropDownMenuIndex.BICYCLECRUNCH ||
                   i == DropDownMenuIndex.JUMPINGJACKS ||
                   i == DropDownMenuIndex.PISTOL ||
                   i == DropDownMenuIndex.PLANK ||
                   i == DropDownMenuIndex.PUSHUP;
        }

        bool _arms(int i)
        {
            return i == DropDownMenuIndex.SQUATBENTARMS ||
                   i == DropDownMenuIndex.BICEPCURL ||
                   i == DropDownMenuIndex.BURPEE ||
                   i == DropDownMenuIndex.JUMPINGJACKS ||
                   i == DropDownMenuIndex.PLANK ||
                   i == DropDownMenuIndex.PIKEWALK ||
                   i == DropDownMenuIndex.PUSHUP;
        }

        void _buttonsFloatFunction()
        {
            SetBoolFloatAnimators(false);
            dropDownMenu.ButtonOpenDropDown();
            buttonsMain.PanelCoachInteraction = false;
        }
    }

    public void SetBoolFloatAnimators(bool value)
    {
        foreach (Animator floatAnimator in floatAnimators)
        {
            floatAnimator.SetBool(PARAMETER, value);
        }
    }

    public Transform[] FloatTransforms => floatTransforms;
}