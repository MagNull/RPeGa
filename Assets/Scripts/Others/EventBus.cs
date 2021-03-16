using System.Collections.Generic;
using UnityEngine.Events;

namespace Others
{
    public static class EventBus
    {
        private static readonly Dictionary<string, UnityEvent> _eventDictionary = new Dictionary<string, UnityEvent>();
        
        public static void StartListening(string eventName, UnityAction listener)
        {
            if (_eventDictionary.TryGetValue(eventName, out UnityEvent newEvent))
            {
                newEvent.AddListener(listener);
            }
            else
            {
                newEvent = new UnityEvent();
                newEvent.AddListener(listener);
                _eventDictionary.Add(eventName, newEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            if (_eventDictionary.TryGetValue(eventName, out UnityEvent newEvent))
            {
                newEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            if (_eventDictionary.TryGetValue(eventName, out UnityEvent newEvent))
            {
                newEvent.Invoke();
            }
        }

        public static void RemoveEvent(string eventName)
        {
            if (_eventDictionary.ContainsKey(eventName)) _eventDictionary.Remove(eventName);
        }
    
    }
}