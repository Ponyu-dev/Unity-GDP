using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups
{
    public abstract class PopupSOBase : ScriptableObject
    {
        [SerializeField] public PopupView prefab;
        
        [ValueDropdown("GetPresenterTypesDropdown")]
        [SerializeField]
        private string presenterTypeName;

        public virtual Type PresenterType
        {
            get => Type.GetType(presenterTypeName);
            set => presenterTypeName = value.AssemblyQualifiedName;
        }
        
        protected abstract IEnumerable<Type> GetPresenterTypes();

        private IEnumerable<ValueDropdownItem<string>> GetPresenterTypesDropdown()
        {
            return GetPresenterTypes()
                .Select(type => new ValueDropdownItem<string>(type.Name, type.AssemblyQualifiedName));
        }
        
        /*[ValueDropdown("GetPresenterTypesDropdown")]
        [SerializeField]
        private Type presenterType;

        public virtual Type PresenterType
        {
            get => presenterType;
            set => presenterType = value;
        }
    
        protected abstract IEnumerable<Type> GetPresenterTypes();

        private IEnumerable<ValueDropdownItem<Type>> GetPresenterTypesDropdown()
        {
            foreach (var type in GetPresenterTypes())
            {
                yield return new ValueDropdownItem<Type>(type.Name, type);
            }
        }*/
    }
}