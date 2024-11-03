using System;
using UnityEngine;

namespace Popups.Helpers
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class TypeSelectorAttribute : PropertyAttribute { }
}