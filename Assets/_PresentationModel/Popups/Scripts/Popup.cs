using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups
{
    public interface IPopup
    {
        public event UnityAction OnCloseClicked;
        public event UnityAction OnCancelClicked;
        public event UnityAction OnApplyClicked;

        void SetData(PopupData data);
    }
    
    public abstract class Popup : MonoBehaviour, IPopup
    {
        [SerializeField] private Button buttonClose;
        public event UnityAction OnCloseClicked
        {
            add => this.buttonClose.onClick.AddListener(value);
            remove => this.buttonClose.onClick.RemoveListener(value);
        }
        
        [SerializeField] private Button buttonCancel;
        public event UnityAction OnCancelClicked
        {
            add => this.buttonCancel.onClick.AddListener(value);
            remove => this.buttonCancel.onClick.RemoveListener(value);
        }
        
        [SerializeField] private Button buttonApply;
        public event UnityAction OnApplyClicked
        {
            add => this.buttonApply.onClick.AddListener(value);
            remove => this.buttonApply.onClick.RemoveListener(value);
        }

        public abstract void SetData(PopupData data);
    }
}