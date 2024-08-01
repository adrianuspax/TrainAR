using ASP.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debug = ASP.Debug;

public static class ButtonsManager
{
    public static (Button[] buttons, bool[] booleans) Assignment(Transform header, params UnityAction<int>[] behaviour)
    {
        Button[] button;
        bool[] isPressed;
        button = new Button[header.childCount];
        isPressed = new bool[button.Length];

        for (int index = 0; index < button.Length; index++)
        {
            for (int i = 0; i < behaviour.Length; i++)
            {
                _assignmentButtons(index, button, header, behaviour[i]);
            }
        }

        return (button, isPressed);

        void _assignmentButtons(int index, Button[] button, Transform header, UnityAction<int> behaviour)
        {
            button[index] = header.GetChild(index).GetComponent<Button>();
            button[index].onClick.AddListener(() => behaviour?.Invoke(index));
        }
    }

    public static void AddListners(this Button[] buttons, bool[] isPressed, UnityAction<int, bool> call)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => call?.Invoke(index, isPressed[index] = !isPressed[index]));
        }
    }

    public static void SetInteraction(bool interactable, params Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int childCount = buttons[i].transform.childCount;

            if (childCount == 0)
            {
                buttons[i].interactable = interactable;
            }
            else
            {
                for (int j = 0; j < childCount; j++)
                {
                    if (buttons[i].transform.GetChild(j).TryGetComponent(out TextMeshProUGUI tmp))
                    {
                        _interactable(buttons[i], tmp, interactable);
                        break;
                    }
                }
            }
        }

        static void _interactable(Button button, TextMeshProUGUI tmp, bool interactable)
        {
            Color color;
            button.interactable = interactable;
            color = tmp.color;
            color.a = interactable ? 1f : 0.5f;
            tmp.color = color;
        }
    }

    public static void CloseApplication()
    {
#if UNITY_EDITOR
        Debug.Log("CloseApplication() called!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}