using ASP;
using ASP.Extensions;
using UnityEngine;

public class PageDefault : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.components, order = 2)]
    [SerializeField, ReadOnly] protected Animator animator;

    public const string PARAMETER = "isOpen";
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    protected void ComponentsAssignment()
    {
        this.GetComponentInChildrenIfNull(ref animator);
    }

    public float AnimationPlay(bool isOpen)
    {
        animator.SetBool(PARAMETER, isOpen);
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
