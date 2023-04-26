// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mosuva.Messaging.Core
{
    /// <summary>
    /// Services implementing this interface will retrieve messages from the service
    /// </summary> 
    public interface ISubscribe { }

    public interface ISubscribe<T> : ISubscribe
    {
        public void Execute(T message);
    }
}