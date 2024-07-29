using ASP;
using ASP.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;


public class MusclesSelectables : MonoBehaviour
{
    [Header(Header.READONLY, order = 0)]
    [Space(-10, order = 1)]
    [Header(Header.variables, order = 2)]
    [SerializeField, ReadOnly]                 private Animator animator;
    [SerializeField, ReadOnly, NonReorderable] private Outline[] outlines;
    [SerializeField, ReadOnly, NonReorderable] private EventTrigger[] triggers;

    private void Awake()
    {
        ComponentsAssignment();
    }
    /// This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        foreach (EventTrigger trigger in triggers)
        {
            EventTrigger.Entry entry = new()
            {
                eventID = EventTriggerType.PointerClick
            };

            entry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data); });
            trigger.triggers.Add(entry);
        }
    }

    private void Start()
    {
        animator.gameObject.SetActive(false);
    }
    /// <summary>
    /// Assignment of components and variables
    /// </summary>
    [ContextMenu("Components Assignment")]
    private void ComponentsAssignment()
    {
        if (animator.IsNull())
            animator = GetComponentInChildren<Animator>();

        if (outlines.IsNullOrEmpty() || triggers.IsNullOrEmpty())
        {
            (outlines, triggers) = _assign();
        }

        static (Outline[] outlines, EventTrigger[] triggers) _assign()
        {
            GameObject[] gameObjcts;
            Outline[] outlines;
            EventTrigger[] triggers;

            gameObjcts = GameObject.FindGameObjectsWithTag(Tag.Muscle);

            outlines = new Outline[gameObjcts.Length];
            triggers = new EventTrigger[gameObjcts.Length];

            for (int i = 0; i < gameObjcts.Length; i++)
            {
                outlines[i] = gameObjcts[i].transform.GetComponent<Outline>();
                triggers[i] = gameObjcts[i].transform.GetComponent<EventTrigger>();
            }

            return (outlines, triggers);
        }

        ASP.SceneManagement.SceneManager.SaveCurrentScene();
    }

    private void OnPointerClickDelegate(PointerEventData data)
    {
        GameObject gameObject;
        gameObject = data.pointerCurrentRaycast.gameObject;

        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = !outlines[i].enabled && (outlines[i].gameObject == gameObject);

            if (outlines[i].gameObject == gameObject)
            {
                animator.gameObject.SetActive(outlines[i].enabled);
            }
        }
    }
}
