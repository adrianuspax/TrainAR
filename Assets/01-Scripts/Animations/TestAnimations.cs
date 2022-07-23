using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    private Color noPressedColor;

    private const int IDLE = 0;
    private const int BORED = 1;
    private const int OFFESINVE = 2;
    private const int SQUAT = 3;
    private const int BENTARMS = 4;

    private void Awake()
    {
        animations = this.GetComponent<CoatchAnimations>();

        gridButton = new Button[gridHeader.childCount];
        gridPressed = new bool[gridButton.Length];

        for (int i = 0; i < gridButton.Length; i++)
        {
            AssignButtons(i);
        }

        noPressedColor = gridButton[1].image.color;
    }

    private void Start()
    {
        gridButton[0].image.color = pressedColor;
        gridPressed[0] = true;
    }

    private void GridFunction(int index)
    {
        animations.Animation((Trigger)index);
    }

    private void GridUIBehaviour(int index)
    {
        gridPressed[index] = !gridPressed[index];

        for (int i = 0; i < gridButton.Length; i++)
        {
            if (i == index && gridPressed[index])
            {
                gridButton[index].image.color = pressedColor;
            }
            else
            {
                gridButton[i].image.color = noPressedColor;
                gridPressed[i] = false;
            }
        }
                
        StartCoroutine(Cor_GridUIBehaviour(index));
        
        GridFunction(index);
    }

    private IEnumerator Cor_GridUIBehaviour(int index)
    {
        float time;
        bool isLoop;
        time = animations.clipByIndex[index].length;
        isLoop = animations.clipByIndex[index].isLooping;

        if (isLoop)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(time);
            gridButton[index].image.color = noPressedColor;
            gridPressed[index] = false;
            gridButton[0].image.color = pressedColor;
            gridPressed[0] = true;
        }
    }

    private void AssignButtons(int index)
    {
        gridButton[index] = gridHeader.GetChild(index).GetComponent<Button>();
        gridButton[index].onClick.AddListener(() => GridUIBehaviour(index));
    }
}