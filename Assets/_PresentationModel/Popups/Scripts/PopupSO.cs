using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(menuName = "Popups/New PopupSO", fileName = "PopupSO")]
    public class PopupSO : ScriptableObject
    {
        [SerializeField] public PopupType type;
        [SerializeField] public Popup prefab;
        [SerializeField] public PopupData data;
    }
}