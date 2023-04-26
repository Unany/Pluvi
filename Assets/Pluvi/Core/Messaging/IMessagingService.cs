// Created by: William Dye - 2023
// License Type: Proprietary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mosuva.Pluvi.Services.Core;

namespace Mosuva.Messaging.Core
{
    public interface IMessagingService : IService
    {
        void Subscribe<T>(ISubscribe<T> subscriber);
        void Unsubscribe<T>(ISubscribe<T> subscriber);
        void Dispatch<T>(T message);
    }
}