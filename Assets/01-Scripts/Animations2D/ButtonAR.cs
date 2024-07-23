using ASP;
using ASP.Extensions;
using UnityEngine;

public class ButtonAR : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] private Animator animator;

    public const string PARAMETER = "isStarted";
    /// Awake is called when an enabled script instance is being loaded.
    private void Awake()
    {
        ComponentsAssignment();
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (animator.IsNull())
            animator = GetComponentInChildren<Animator>();
    }

    public Animator Animator => animator;
}
