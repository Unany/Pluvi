// Created by: William Dye - 2023
// License Type: Proprietary

using UnityEngine;

namespace Mosuva.Messaging.Core
{
    /// <summary>
    /// Services implementing this interface will dispatch messages to the service
    /// </summary> 
    public interface IDispatch { }

    /// /// x<summary>
    /// Base interface required by the messaging bus to Dispatch messages
    /// </summary> 
    public interface IDispatch<T> : IDispatch
    {
        public void HandleTransmitMessage(T? message);
    }
}