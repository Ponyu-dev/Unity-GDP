using Popups;
using UnityEngine;

namespace _PresentationModel.Popups.Samples
{
    [PopupPresenter(typeof(TestPopupPresenter))]
    [CreateAssetMenu(menuName = "Popups/TestPopupData", fileName = "TestPopupData")]
    public class TestPopupData : PopupData
    {
        public string Description;
    }
}