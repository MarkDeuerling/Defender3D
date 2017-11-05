using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventContainer
{
    public delegate void CallBack(GameObject entity);

    private readonly Dictionary<string, CallBack> container;

    public EventContainer()
    {
        container = new Dictionary<string, CallBack>();
    }

    public EventContainer AddEvent(string eventName)
    {
        container.Add(eventName, null);
        return this;
    }

    public void Bind(string eventName, CallBack callBack)
    {
        container[eventName] += callBack;
    }

    public void Unbind(string eventName, CallBack callBack)
    {
        container[eventName] -= callBack;
    }

    public void Execute(string eventName, GameObject entity)
    {
        CallBack callBack;
        container.TryGetValue(eventName, out callBack);
        if (callBack != null)
            callBack(entity);
    }
}