using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TestAnimations : MonoBehaviour
{
    private CoatchAnimations animations;
    [SerializeField]
    private Transform gridHeader;
    [SerializeField]
    private Button[] gridButton;
    [SerializeField]
    private bool[] gridPressed;
    [SerializeField]
    private Slider idleBlend;
    [SerializeField]
    private Slider breathing;
    [SerializeField]
    private Color pressedColor;
    [SerializeField]
    private Color noPressedColor;
    [SerializeField]
    private GameObject buttonPrefab;
    private TextMeshProUGUI[] tmpButton;

    private void Awake()
    {
        animations = this.GetComponent<CoatchAnimations>();

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

                if (index != 0) { animations.StartAnimation(index); }
            }
            else
            {
                gridButton[i].image.color = noPressedColor;
                gridButton[i].interactable = false;
                tmpButton[i].color = pressedColor;
                gridPressed[i] = false;
            }
        }

        if (!gridPressed[index]) { IdleTriggered(); animations.StartAnimation(0); }

        StartCoroutine(Cor_GridUIBehaviour(index));
    }

    public void IdleTriggered()
    {
        GridButtonsBehaviour(0);

        foreach (Button buttons in gridButton)
        {
            buttons.interactable = true;
        }

        gridButton[0].interactable = false;
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
}