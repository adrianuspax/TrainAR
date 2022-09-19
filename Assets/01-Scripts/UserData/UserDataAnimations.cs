using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataAnimations : MonoBehaviour
{
    [Header("PAGES", order = 0)]
    [SerializeField] private AnimationClip pageNullClip;
    [SerializeField] private AnimationClip pageEnterClip;
    [SerializeField] private AnimationClip pageOutClip;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Animators Controllers", order = 2)]
    [ReadOnly, SerializeField] private Animator[] animatorsPages;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [ReadOnly, SerializeField] private PagesAndFields UserDataManager;

    public void PlayPageOutAnimation(int page)
    {
        animatorsPages[page - 1].Play(pageOutClip.name);
    }

    public Animator[] AssignAnimatorsPages(GameObject[] page)
    {
        Animator[] animator;
        animator = new Animator[page.Length];

        for (int i = 0; i < page.Length; i++)
        {
            animator[i] = page[i].transform.GetChild(0).GetComponent<Animator>();
        }

        return animator;
    }

    public Animator[] AnimatorsPage { set => animatorsPages = value; }

    public float EnterClipTime => pageEnterClip.length;
    public float OutClipTime => pageOutClip.length;

}