using System;
using UnityEngine;

namespace Game.Scripts.Common.Team
{
    public enum TeamType
    {
        PLAYER = 0,
        ZOMBIE = 1,
        NONE = 2
    }

    public static class TeamTypeExtensions
    {
        public static Color GetColor(this TeamType teamType)
        {
            return teamType switch
            {
                TeamType.PLAYER => Color.blue,
                TeamType.ZOMBIE => Color.red,
                TeamType.NONE => Color.black,
                _ => throw new ArgumentOutOfRangeException(nameof(teamType), teamType, null)
            };
        }
    }
}