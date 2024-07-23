using ASP;
using ASP.Extensions;
using UnityEngine;

public class AnchorAnimation : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.animators, order = 2)]
    [SerializeField, ReadOnly] private Animator animator;

    [Header(Header.scripts, order = 0)]
    [SerializeField, ReadOnly] private ButtonAR buttonAR;

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
        if (buttonAR.IsNull())
            buttonAR = FindAnyObjectByType<ButtonAR>();

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    public void AnchorIsHit(bool isHit) => animator.SetBool(Parameter.AnchorHit, isHit);
    public void AnchorVisible(bool isVisible) => animator.SetBool(Parameter.AnchorEnable, isVisible);
    public void MovePhoneButton(bool isPlaying) =>  buttonAR.Animator.SetBool(ButtonAR.PARAMETER, isPlaying);

    public (string AnchorHit, string AnchorEnable) Parameter
    {
        get => ("isHit", "anchorEnable");
    }
}