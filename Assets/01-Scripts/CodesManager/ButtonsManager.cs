using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Scripts", order = 2)]
    [SerializeField, ReadOnly] private CanvasButtons CanvasButtons;
    [SerializeField, ReadOnly] private DropDownMenu DDM;

    public delegate void BehaviourButtons(int index);

    private void Awake()
    {
        CanvasButtons = this.GetComponent<CanvasButtons>();
        DDM = this.GetComponent<DropDownMenu>();
    }

    public CanvasButtons CanvasMain => CanvasButtons;
    public DropDownMenu DropDownMenu => DDM;

    public (Button[], bool[]) Assignment(Transform header, params BehaviourButtons[] behaviour)
    {
        Button[] button;
        bool[] isPressed;
        button = new Button[header.childCount];
        isPressed = new bool[button.Length];

        for (int index = 0; index < button.Length; index++)
        {
            for (int i = 0; i < behaviour.Length; i++)
            {
                StartCoroutine(Cor_AssignmentButtons(index, button, header, behaviour[i]));
            }
        }

        return (button, isPressed);
    }

    private IEnumerator Cor_AssignmentButtons(int index, Button[] button, Transform header, BehaviourButtons behaviour)
    {
        button[index] = header.GetChild(index).GetComponent<Button>();
        button[index].onClick.AddListener(() => behaviour(index));
        yield return null;
    }


    public void Interaction(bool interactable, params Button[][] array)
    {
        foreach (Button[] buttons in array)
        {
            Interaction(interactable, buttons);
        }
    }

    public void Interaction(bool interactable, params Button[] button)
    {
        for (int i = 0; i < button.Length; i++)
        {
            int childCount;

            childCount = button[i].transform.childCount;

            if (childCount != 0)
            {
                for (int index = 0; index < childCount; index++)
                {
                    if (button[i].transform.GetChild(index).GetComponent<TextMeshProUGUI>() != null)
                    {
                        StartCoroutine(Cor_Interactable(button[i], interactable, index));
                        break;
                    }
                }
            }
            else
            {
                button[i].interactable = interactable;
            }
        }
    }

    private IEnumerator Cor_Interactable(Button button, bool interactable, int index)
    {
        Color color;

        button.interactable = interactable;
        color = button.transform.GetChild(index).GetComponent<TextMeshProUGUI>().color;

        yield return null;

        color.a = interactable ? 1f : 0.5f;

        yield return null;

        button.transform.GetChild(index).GetComponent<TextMeshProUGUI>().color = color;
    }
}