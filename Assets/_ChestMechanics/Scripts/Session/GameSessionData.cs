using System;
using JetBrains.Annotations;

namespace _ChestMechanics.Session
{
    [Serializable]
    public class GameSessionData
    {
        public string sessionStart;
        public string sessionEnd;
        [CanBeNull] public string allSessionDuration;
    }
}