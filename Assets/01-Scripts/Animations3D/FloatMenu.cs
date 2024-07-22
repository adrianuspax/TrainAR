using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class FloatMenu : MonoBehaviour
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
    [SerializeField, ReadOnly] private Transform buttonsFloatHeader;
    [SerializeField, NonReorderable, ReadOnly] private Button[] floatButtons;
    [SerializeField, NonReorderable, ReadOnly] private Transform[] floatTransforms;
    [SerializeField, NonReorderable, ReadOnly] private Canvas[] floatCanvas;
    [SerializeField, NonReorderable, ReadOnly] private Animator[] floatAnimators;
    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private ButtonsMain buttonsMain;
    [SerializeField, ReadOnly] private DropDownMenu dropDownMenu;

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
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
        if (buttonsFloatHeader.IsNull())
            buttonsFloatHeader = GameObject.Find("ButtonsFloat").transform;

        int count = buttonsFloatHeader.childCount;

        if (buttonsMain.IsNull())
            buttonsMain = GetComponent<ButtonsMain>();
        if (dropDownMenu.IsNull())
            dropDownMenu = GetComponent<DropDownMenu>();
        if (floatAnimators.IsNullOrEmpty())
            floatAnimators = _assignAnimators(buttonsFloatHeader);
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
                header.GetComponentFrom(out components[i], i, 0);
            }

            return components;
        }

        T[] _assignComponents<T>(Component[] components, params int[] indexChild) where T : Component
        {
            T[] returnedComponents = new T[count];

            for (int i = 0; i < count; i++)
            {
                components[i].GetComponentFrom(out returnedComponents[i], indexChild);
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
            return i == ButtonDDM.BURPEE ||
                   i == ButtonDDM.BICYCLECRUNCH ||
                   i == ButtonDDM.CIRCLECRUNCH ||
                   i == ButtonDDM.PIKEWALK ||
                   i == ButtonDDM.PUSHUP ||
                   i == ButtonDDM.SITUPS;
        }

        bool _abdomen(int i)
        {
            return i == ButtonDDM.BURPEE ||
                   i == ButtonDDM.BICYCLECRUNCH ||
                   i == ButtonDDM.PIKEWALK ||
                   i == ButtonDDM.PUSHUP ||
                   i == ButtonDDM.SITUPS;
        }

        bool _legs(int i)
        {
            return i == ButtonDDM.SQUAT ||
                   i == ButtonDDM.SQUATBENTARMS ||
                   i == ButtonDDM.BOXJUMP ||
                   i == ButtonDDM.BURPEE ||
                   i == ButtonDDM.BICYCLECRUNCH ||
                   i == ButtonDDM.JUMPINGJACKS ||
                   i == ButtonDDM.PISTOL ||
                   i == ButtonDDM.PLANK ||
                   i == ButtonDDM.PUSHUP;
        }

        bool _arms(int i)
        {
            return i == ButtonDDM.SQUATBENTARMS ||
                   i == ButtonDDM.BICEPCURL ||
                   i == ButtonDDM.BURPEE ||
                   i == ButtonDDM.JUMPINGJACKS ||
                   i == ButtonDDM.PLANK ||
                   i == ButtonDDM.PIKEWALK ||
                   i == ButtonDDM.PUSHUP;
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
            floatAnimator.SetBool(Parameter.floatIsOpen, value);
        }
    }

    public Transform[] FloatTransforms => floatTransforms;
}