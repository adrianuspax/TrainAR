using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesAndFields
    : MonoBehaviour
{
    [Header("HEADERS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Header Pages", order = 2)]
    [SerializeField] private Transform pagesHeader;
    [Space(-10, order = 0)]
    [Header("Header Fields", order = 1)]
    [SerializeField] private Transform footerHeader;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Pages", order = 2)]
    [ReadOnly, SerializeField] private GameObject[] page;
    [Space(-10, order = 0)]
    [Header("Fields", order = 1)]
    [ReadOnly, SerializeField] private GameObject[] footer;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [ReadOnly, SerializeField] private AnimationsManager Animations;


    public const int PAGE1 = 1;
    public const int PAGE2 = 2;
    public const int PAGE3 = 3;
    public const int PAGE4 = 4;
    public const int PAGE5 = 5;
    public const int PAGE6 = 6;

    public const int FOOTER1 = 1;
    public const int FOOTER2 = 2;
    public const int FOOTER3 = 3;
    public const int FOOTER4 = 4;

    private void Awake()
    {
        page = AssignGameObjects(pagesHeader);
        footer = AssignGameObjects(footerHeader);

        Animations = this.GetComponent<AnimationsManager>();
        Animations.UserData.AnimatorsPage = Animations.UserData.AssignAnimatorsPages(page);
    }

    private void Start()
    {
        foreach (GameObject pages in page) { pages.SetActive(false); }
        foreach (GameObject footers in footer) { footers.SetActive(false); }

        PageActivation(PAGE1);
        //FooterActivation(FOOTER1);
    }

    public void PageActivation(int pageNumberToActive)
    {
        StartCoroutine(Cor_PageActivation(pageNumberToActive));
    }

    public void FooterActivation(int footerNumberToActive)
    {
        int index;
        index = footerNumberToActive - 1;

        if (index < TotalFooters && index >= 0)
        {
            foreach (GameObject footers in footer)
            {
                footers.SetActive(false);
            }

            footer[index].SetActive(true);
            Animations.UserData.PlayFooterEnterClip();
        }
        else
        {
            Debug.LogError("Footer Activation: Parameter value out of range. You moke!" + "// footerNumberToActive = " + footerNumberToActive);
        }
    }

    private IEnumerator Cor_PageActivation(int pageNumber)
    {
        int index;
        index = pageNumber - 1;

        if (index < TotalPages && index >= 0)
        {
            if (pageNumber > 1)
            {
                Animations.UserData.PlayPageOutAnimation(index);
                yield return new WaitForSeconds(Animations.UserData.OutClipTime * 0.5f);
            }

            page[index].SetActive(true);

            yield return new WaitForSeconds(Animations.UserData.EnterClipTime);

            for (int i = 0; i < index; i++)
            {
                page[i].SetActive(false);
            }

            for (int j = TotalPages - 1; j > index; j--)
            {
                page[j].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Page Activation: Parameter value out of range. You moke!");
        }
    }

    private GameObject[] AssignGameObjects(Transform header)
    {
        GameObject[] gameObjects;

        gameObjects = new GameObject[header.childCount];

        for (int i = 0; i < header.childCount; i++)
        {
            gameObjects[i] = header.GetChild(i).gameObject;
        }

        return gameObjects;
    }

    public int TotalPages => page.Length;
    public int TotalFooters => footer.Length;
}