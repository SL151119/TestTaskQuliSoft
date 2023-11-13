using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ButtonEvents : MonoBehaviour
{
    public event Action OnGasButtonHoldStart;
    public event Action OnGasButtonHoldEnd;
    public event Action OnBrakeButtonHoldStart;
    public event Action OnBrakeButtonHoldEnd;

    [Header("Buttons")]
    public GameObject gasButton;
    public GameObject brakeButton;

    private void Start()
    {
        if (gasButton != null)
        {
            AddEventTriggerListener(gasButton, EventTriggerType.PointerDown,
                (_) => OnGasButtonHoldStart?.Invoke());
            AddEventTriggerListener(gasButton, EventTriggerType.PointerUp,
                (_) => OnGasButtonHoldEnd?.Invoke());
        }

        if (brakeButton == null) return;
        {
            AddEventTriggerListener(brakeButton, EventTriggerType.PointerDown,
                (_) => OnBrakeButtonHoldStart?.Invoke());
            AddEventTriggerListener(brakeButton, EventTriggerType.PointerUp,
                (_) => OnBrakeButtonHoldEnd?.Invoke());
        }
    }

    private static void AddEventTriggerListener(GameObject obj, EventTriggerType type,
        Action<BaseEventData> action)
    {
        var trigger = obj.GetComponent<EventTrigger>() 
            ?? obj.AddComponent<EventTrigger>(); //If the component null, add a new EventTrigger component to the GameObject.

        var entry = new EventTrigger.Entry
        {
            eventID = type // Sets the eventID of the entry to the specified EventTriggerType (type), such as PointerDown or PointerUp.
        };
        entry.callback.AddListener(action.Invoke);
        trigger.triggers.Add(entry); //Adds the created entry to the list of triggers in the EventTrigger component (trigger).
    }
}