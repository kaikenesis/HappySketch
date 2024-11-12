using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController instance = null;
    public static EventController Instance => instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private readonly IDictionary<GameEventType, UnityEvent>
        Events = new Dictionary<GameEventType, UnityEvent>();

    public void Subscribe(GameEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.AddListener(listener);

        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    public void Unsubscribe(GameEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public void Publish(GameEventType eventType)
    {
        UnityEvent thisEvent;

        if (Events.TryGetValue(eventType, out thisEvent))
            thisEvent.Invoke();
    }
}

public enum GameEventType
{
    TITLE,
    START,
    PAUSE,
    END,
    QUIT
}