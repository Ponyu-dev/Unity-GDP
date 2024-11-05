using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    [Serializable]
    public struct UserInfo
    {
        [ShowInInspector] public string Name { get; private set; }
        [ShowInInspector] public string Description { get; private set; }
        [ShowInInspector] public Sprite Icon { get; private set; }
    }
}