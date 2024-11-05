using System;
using System.Collections.Generic;
using System.Linq;
using Popups;
using UnityEngine;

namespace _PresentationModel.Scripts.Config
{
    [CreateAssetMenu(fileName = "PopupLevelUpSo", menuName = "Popups/PopupLevelUpSo")]
    public class PopupLevelUpSo : PopupSOBase
    {
        protected override IEnumerable<Type> GetPresenterTypes()
        {
            Debug.Log(typeof(PopupLevelUpSo).Assembly.FullName);
            return typeof(PopupLevelUpSo).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(PopupPresenter)) && !type.IsAbstract);
        }
    }
}