using ShootEmUp;
using UnityEngine;

namespace _ShootEmUp.UI.Scripts
{
    public abstract class DefaultScreen : MonoBehaviour
    {
        [SerializeField]
        protected GameObject view;
        
        [SerializeField] 
        protected GameManager m_GameManager;

        protected virtual void Show()
        {
            view.SetActive(true);
        }

        protected virtual void Hide()
        {
            view.SetActive(false);
        }
    }
}