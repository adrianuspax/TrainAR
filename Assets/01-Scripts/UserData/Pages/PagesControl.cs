using ASP;
using ASP.Extensions;
using System.Collections;
using UnityEngine;
using Debug = ASP.Debug;

public class PagesControl : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.variables, order = 2)]
    [SerializeField, ReadOnly] private int currentIndex;
    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private ButtonsMainUserData buttonsMainUserData;
    [SerializeField, NonReorderable, ReadOnly] private PageDefault[] pages;
    /// Reset to default values.
    private void Reset()
    {
        ComponentsAssignment();
    }
    /// Awake is called when an enabled script instance is being loaded.
    private void Awake()
    {
        ComponentsAssignment();
    }
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    private void Start()
    {
        SetActivePageByIndex(0);
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        this.GetComponentsInChildrenHeadersIfNull(ref pages);
        if (buttonsMainUserData.IsNull())
            buttonsMainUserData = FindAnyObjectByType<ButtonsMainUserData>();

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void SetActivePageByIndex(int index)
    {
        int length = pages.Length;

        if (index < 0 || index >= length)
        {
            Debug.LogError($"Parameter value out of range! min: 0 and max: {length - 1}.");
            return;
        }

        StartCoroutine(_pageActivation());

        IEnumerator _pageActivation()
        {
            if (index > 0)
            {
                float s = pages[currentIndex].AnimationPlay(false);
                yield return new WaitForSeconds(s * 0.5f);
            }

            pages[index].AnimationPlay(true);
            index.ComparativeAssignment(ref currentIndex);
        }
    }

    public bool SetActiveNextPage()
    {
        if (currentIndex < 0 || currentIndex > pages.Length - 1)
            return false;

        SetActivePageByIndex(currentIndex + 1);
        return true;
    }

    public int CurrentIndex => currentIndex;
}
