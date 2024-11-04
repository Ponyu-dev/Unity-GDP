using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Popups.Samples.Scripts
{
    [CreateAssetMenu(fileName = "SamplesPopupSO", menuName = "PopupsSamples/SamplesPopupSO")]
    public class PopupSO : PopupSOBase
    {
        protected override IEnumerable<Type> GetPresenterTypes()
        {
            Debug.Log(typeof(PopupSO).Assembly.FullName);
            return typeof(PopupSO).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(PopupPresenter)) && !type.IsAbstract);
        }
    }
}