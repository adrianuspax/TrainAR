using UnityEngine;
using UnityEngine.EventSystems;
using ASP;
using Debug = ASP.Debug;

public class MusclesSelectables : MonoBehaviour
{
    [SerializeField, ReadOnly, NonReorderable] private Outline[] outlines;
    [SerializeField, ReadOnly, NonReorderable] private EventTrigger[] triggers;
    [SerializeField] private GameObject content;

    private void Awake()
    {
        (outlines, triggers) = Assign();

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
        content.SetActive(false);
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
                content.SetActive(outlines[i].enabled);
            }
        }
    }

    private (Outline[] outlines, EventTrigger[] triggers) Assign()
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
}
