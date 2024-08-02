using System.Collections;
using TMPro;
using TreinAR.MainScenes;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimations : MonoBehaviour
{
    private Coach animations;
    [SerializeField]
    private Transform gridHeader;
    private Button[] gridButton;
    private bool[] gridPressed;
    private TextMeshProUGUI[] tmpButton;
    [SerializeField]
    private Slider breathing;
    [SerializeField]
    private Color pressedColor;
    [SerializeField]
    private Color noPressedColor;
    [SerializeField]
    private GameObject buttonPrefab;

    private void Awake()
    {
        animations = FindAnyObjectByType<Coach>();

        InstantiateButtons(animations.TotalCount);

        gridButton = new Button[gridHeader.childCount];
        gridPressed = new bool[gridButton.Length];
        tmpButton = new TextMeshProUGUI[gridButton.Length];

        for (int i = 0; i < gridButton.Length; i++)
        {
            AssignButtons(i);
            AssignTMPs(i);
        }
    }

    private void Start()
    {
        IdleTriggered();
    }

    private void GridButtonsBehaviour(int index)
    {
        gridPressed[index] = !gridPressed[index];

        for (int i = 0; i < gridButton.Length; i++)
        {
            if (i == index && gridPressed[index])
            {
                gridButton[index].image.color = pressedColor;
                tmpButton[index].color = noPressedColor;

                if (index != 0)
                { animations.StartAnimation(index); }
            }
            else
            {
                gridButton[i].image.color = noPressedColor;
                gridButton[i].interactable = false;
                tmpButton[i].color = pressedColor;
                gridPressed[i] = false;
            }
        }

        if (!gridPressed[index])
        {
            StartCoroutine(Cor_ButtonsWaiting(index));
            animations.StartAnimation(Coach.Trigger.Idle);
        }

        ExceptionsBehaviour();
        StartCoroutine(Cor_GridUIBehaviour(index));
    }

    public void IdleTriggered()
    {
        GridButtonsBehaviour(0);

        for (int i = 1; i < gridButton.Length; i++)
        {
            gridButton[i].interactable = true;
        }
    }

    public void ExceptionsBehaviour()
    {
        if (gridPressed[8] || gridPressed[9] || gridPressed[16])
        {
            gridButton[8].interactable = true;
            gridButton[9].interactable = true;
            gridButton[16].interactable = true;
        }

        if (gridPressed[3] || gridPressed[4])
        {
            gridButton[3].interactable = true;
            gridButton[4].interactable = true;
        }
    }

    private void InstantiateButtons(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(buttonPrefab, gridHeader);
        }
    }

    private void AssignButtons(int index)
    {
        TextMeshProUGUI tmpButon;

        gridButton[index] = gridHeader.GetChild(index).GetComponent<Button>();
        gridButton[index].onClick.AddListener(() => GridButtonsBehaviour(index));

        for (int i = 0; i < gridButton[index].transform.childCount; i++)
        {
            tmpButon = gridButton[index].transform.GetChild(i).GetComponent<TextMeshProUGUI>();

            if (tmpButon != null)
            {
                tmpButon.text = animations.GetName(index);
            }
        }
    }

    private void AssignTMPs(int index)
    {
        TextMeshProUGUI tmpButton;
        int childCount;
        childCount = gridButton[index].transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            tmpButton = gridButton[index].transform.GetChild(i).GetComponent<TextMeshProUGUI>();

            if (tmpButton != null)
            {
                this.tmpButton[index] = tmpButton;
            }
        }
    }

    private IEnumerator Cor_GridUIBehaviour(int index)
    {
        float time;
        bool isLoop;
        time = animations.GetClipTime(index);
        isLoop = animations.GetClipLooping(index);

        if (isLoop)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(time);
            IdleTriggered();
        }
    }

    private IEnumerator Cor_ButtonsWaiting(int index)
    {
        float time;
        time = animations.GetClipTime(index);

        yield return new WaitForSeconds(time);

        GridButtonsBehaviour(0);

        for (int i = 1; i < gridButton.Length; i++)
        {
            gridButton[i].interactable = true;
        }
    }
}