using Popups.Helpers;
using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(menuName = "Popups/New PopupSO", fileName = "PopupSO")]
    public class PopupSO : ScriptableObject
    {
        [SerializeField] public PresenterType presenterType;
        [SerializeField] public PopupView prefab;
    }
}