// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections.Generic;
using UnityEngine;

namespace Mosuva.Messaging.Core
{
    public class MessagingService : MonoBehaviour, IMessagingService
    {
        private readonly Dictionary<string, List<ISubscribe>> messageDictionary = new Dictionary<string, List<ISubscribe>>();

        public void Subscribe<T>(ISubscribe<T> subscriber)
        {
            string messageName = typeof(T).Name;
            List<ISubscribe> message = new List<ISubscribe>();

            if (messageDictionary.TryGetValue(messageName, out message))
            {
                Debug.LogWarning(string.Format("WARNING: Messaging Service already contains {0}", messageName));
                message.Add(subscriber);
            }
            else
            {
                message = new List<ISubscribe>();

                message.Add(subscriber);
                messageDictionary.Add(messageName, message);
            }
        }

        public void Unsubscribe<T>(ISubscribe<T> subscriber)
        {
            string messageName = typeof(T).Name;
            List<ISubscribe> message = new List<ISubscribe>();

            if (messageDictionary.TryGetValue(messageName, out message))
            {
                if (message.Contains(subscriber))
                {
                    messageDictionary.Remove(messageName);
                }
                else
                {
                    Debug.LogWarning(string.Format("WARNING: {0} was found but {1} service was not", messageName, subscriber));
                }
            }
            else
            {
                Debug.LogWarning(string.Format("WARNING: Messaging Service already contains {0}", messageName));
            }
        }

        public void Dispatch<T>(T message)
        {
            string messageName = typeof(T).Name;
            List<ISubscribe> subscribers = new List<ISubscribe>();

            if (messageDictionary.TryGetValue(messageName, out subscribers))
            {
                foreach (var subscriber in subscribers)
                {
                    (subscriber as ISubscribe<T>).Execute(message);
                }
            }
            else
            {
                Debug.LogWarning(string.Format("WARNING: Messaging Service already contains {0}", messageName));
            }
        }

        public void Initialise() { }
    }
}