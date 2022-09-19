using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesAndFields : MonoBehaviour
{
    [Header("HEADERS", order = 0)]
    [Space(-10, order = 1)]
    [Header("Header Pages", order = 2)]
    [SerializeField] private Transform pagesHeader;
    [Space(-10, order = 0)]
    [Header("Header Fields", order = 1)]
    [SerializeField] private Transform fieldsHeader;

    [Header("READY ONLY", order = 0)]
    [Space(-10, order = 1)]
    [Header("Pages", order = 2)]
    [ReadOnly, SerializeField] private GameObject[] page;
    [Space(-10, order = 0)]
    [Header("Fields", order = 1)]
    [ReadOnly, SerializeField] private GameObject[] field;
    [Space(-10, order = 0)]
    [Header("Scripts", order = 1)]
    [ReadOnly, SerializeField] private UserDataAnimations UserDataAnimations;


    public const int PAGE1 = 1;
    public const int PAGE2 = 2;
    public const int PAGE3 = 3;
    public const int PAGE4 = 4;
    public const int PAGE5 = 5;
    public const int PAGE6 = 6;

    public const int FIELD1 = 1;
    public const int FIELD2 = 2;
    public const int FIELD3 = 3;
    public const int FIELD4 = 4;

    private void Awake()
    {
        page = AssignGameObjects(pagesHeader);
        field = AssignGameObjects(fieldsHeader);

        UserDataAnimations = this.GetComponent<UserDataAnimations>();
        UserDataAnimations.AnimatorsPage = UserDataAnimations.AssignAnimatorsPages(page);
    }

    private void Start()
    {
        foreach (GameObject pages in page) { pages.SetActive(false); }

        PageActivation(PAGE1);
        FieldActivation(FIELD1);
    }

    public void PageActivation(int pageNumberToActive)
    {
        StartCoroutine(Cor_PageActivation(pageNumberToActive));
    }

    private IEnumerator Cor_PageActivation(int pageNumber)
    {
        int index;
        index = pageNumber - 1;

        if (index < TotalPages && index >= 0)
        {
            if (pageNumber > 1)
            {
                UserDataAnimations.PlayPageOutAnimation(index);
                yield return new WaitForSeconds(UserDataAnimations.OutClipTime * 0.5f);
            }

            page[index].SetActive(true);

            yield return new WaitForSeconds(UserDataAnimations.EnterClipTime);

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

    public void FieldActivation(int fieldNumberToActive)
    {
        int index;
        index = fieldNumberToActive - 1;

        if (index < TotalFields && index >= 0)
        {
            foreach (GameObject field in field)
            {
                field.SetActive(false);
            }

            field[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Field Activation: Parameter value out of range. You moke!");
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
    public int TotalFields => field.Length;
}