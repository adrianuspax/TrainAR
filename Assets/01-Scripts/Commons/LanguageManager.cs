using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    [Header(" =========================== USER DATA =========================== ", order = 0)]
    [Space(-10, order = 1)]
    [Header("PAGES", order = 2)]
    [SerializeField] private string[] pageQuestions;
    [Space(-10, order = 0)]
    [Header("Page 1", order = 1)]
    [SerializeField] private string page1PlaceHolder;
    [Space(-10, order = 0)]
    [Header("Page 3", order = 1)]
    [SerializeField] private string[] buttonGoal;
    [Space(-10, order = 0)]
    [Header("Page 4", order = 1)]
    [SerializeField] private string[] fitnessLevelName;
    [Space(-10, order = 0)]
    [Header("Page 5", order = 1)]
    [SerializeField] private string[] buttonFocus;
    [Space(-10, order = 0)]
    [Header("Page 6", order = 1)]
    [SerializeField] private string[] blockAreaName;
    
    [Header("FOOTERS", order = 0)]
    [SerializeField] private string[] footerQuestions;
    [SerializeField] private string footerPlaceholder;


}