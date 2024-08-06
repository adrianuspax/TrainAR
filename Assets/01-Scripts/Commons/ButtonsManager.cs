using UnityEngine.Events;
using UnityEngine.UI;
using Debug = ASP.Debug;

namespace TreinAR
{
    public static class ButtonsManager
    {
        public static void AddListners(this Button[] buttons, bool[] isPressed, UnityAction<int, bool> call)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[index].onClick.AddListener(() => call?.Invoke(index, isPressed[index] = !isPressed[index]));
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
}
