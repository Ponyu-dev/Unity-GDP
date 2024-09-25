using ShootEmUp;
using UnityEngine;
using VContainer;

namespace _ShootEmUp.UI.Scripts
{
    public abstract class DefaultScreen : MonoBehaviour
    {
        [SerializeField]
        protected GameObject view;

        protected IGameManager m_GameManager;
        
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            m_GameManager = gameManager;
        }
        
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