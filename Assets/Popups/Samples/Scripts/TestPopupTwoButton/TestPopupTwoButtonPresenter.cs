using System;
using Popups;
using Popups.Helpers;
using UnityEngine;
using VContainer;

namespace _PresentationModel.Popups.Samples
{
    public class TestPopupTwoButtonPresenter : PopupPresenter
    {
        [Inject]
        public TestPopupTwoButtonPresenter()
        {
            Debug.Log("[TestPopupTwoButtonPresenter] Constructor");
        }
        
        public override void Init(Type type, PopupView popupView, PopupData popupData)
        {
            base.Init(type, popupView, popupData);

            if (popupView is not TestPopupTwoButtonView testPopupView) return;
            if (popupData is not TestPopupTwoButtonData data) return;
            
            testPopupView.SetTextTitle(data.Title);
            testPopupView.SetTextDescription(data.Description);
            testPopupView.SetTextButtonApply(data.Apply);
            testPopupView.SetTextButtonCancel(data.Cancel);
        }
    }
}