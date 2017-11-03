using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EventContainer
{
    public delegate void CallBack(string evenName, GameObject entity);

    private readonly Dictionary<string, CallBack> container;

    public EventContainer()
    {
        container = new Dictionary<string, CallBack>();
    }

    public EventContainer AddEvent([NotNull] string eventName)
    {
        container.Add(eventName, null);
        return this;
    }

    public void Bind([NotNull] string eventName, [NotNull] CallBack callBack)
    {
        container[eventName] += callBack;
    }

    public void Unbind([NotNull] string eventName, [NotNull] CallBack callBack)
    {
        container[eventName] -= callBack;
    }

    public void Execute([NotNull] string eventName, GameObject entity)
    {
        CallBack callBack;
        container.TryGetValue(eventName, out callBack);
        if (callBack != null)
            callBack(eventName, entity);
    }
}