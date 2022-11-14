using UnityEngine;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    private static TMP_InputField inputFieldDefault;
    private static string placeholderDefault;

    private static bool keepOldTextInField;
    private static string editText;
    private static string oldEditText;


    public static void BehaviourDefault(TMP_InputField inputField, string placeholder)
    {
        inputFieldDefault = inputField;
        placeholderDefault = placeholder;
        inputField.onSelect.AddListener(OnSelectDefault);
        inputField.onDeselect.AddListener(OnDeselectDefault);
        inputField.onValueChanged.AddListener(OnValueChangedDefault);
        inputField.onEndEdit.AddListener(OnEndEditDefault);
        inputField.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatus);
    }

    public static void OnSelectDefault(string inputText)
    {
        PlaceholderText = string.Empty;
    }

    public static void OnDeselectDefault(string inputText)
    {
        PlaceholderText = inputFieldDefault.isFocused ? string.Empty : placeholderDefault;
    }

    public static void OnValueChangedDefault(string inputText)
    {
        PlaceholderText = inputFieldDefault.isFocused ? string.Empty : placeholderDefault;

        oldEditText = editText;
        editText = inputText;
    }

    public static void OnEndEditDefault(string inputText)
    {
        PlaceholderText = inputFieldDefault.isFocused ? string.Empty : placeholderDefault;

        if (keepOldTextInField && !string.IsNullOrEmpty(oldEditText))
        {
            //IMPORTANT ORDER
            editText = oldEditText;
            inputFieldDefault.text = oldEditText;

            keepOldTextInField = false;
        }

        if (inputFieldDefault.wasCanceled)
        {
            PlaceholderText = string.Empty;
        }
    }

    private static void ReportChangeStatus(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
        { keepOldTextInField = true; }
    }

    public static string PlaceholderText
    { set => inputFieldDefault.placeholder.GetComponent<TextMeshProUGUI>().text = value; }
}
