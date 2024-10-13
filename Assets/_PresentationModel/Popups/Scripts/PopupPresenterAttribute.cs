using System;

namespace Popups
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PopupPresenterAttribute : Attribute
    {
        public Type PresenterType { get; }

        public PopupPresenterAttribute(Type presenterType)
        {
            PresenterType = presenterType;
        }
    }
}