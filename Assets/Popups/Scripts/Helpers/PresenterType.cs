using System;
using Sirenix.OdinInspector;

namespace Popups.Helpers
{
    [Serializable]
    public class PresenterType
    {
        private string typeName;
        public PresenterType() {}
        
        public PresenterType(Type type)
        {
            Type = type;
        }

        public Type Type
        {
            get => Type.GetType(typeName);
            set => typeName = value?.AssemblyQualifiedName;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is PresenterType other)
            {
                return typeName == other.typeName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return typeName.GetHashCode();
        }
    }
}