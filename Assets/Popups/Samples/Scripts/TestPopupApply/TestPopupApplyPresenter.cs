using System;
using Popups;
using UnityEngine;
using VContainer;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupApplyPresenter : PopupPresenter
    {
        [Inject]
        public TestPopupApplyPresenter()
        {
            Debug.Log("[TestPopupApplyPresenter] Constructor");
        }
        
        public override void Init(Type type, PopupView popupView, PopupData popupData)
        {
            base.Init(type, popupView, popupData);

            if (popupView is not TestPopupApplyView testPopupView) return;
            if (popupData is not TestPopupApplyData data) return;
            
            testPopupView.SetTextTitle(data.Title);
            testPopupView.SetTextDescription(data.Description);
            testPopupView.SetTextButtonApply(data.Apply);
        }
    }
}