using TMPro;
using UnityEngine;

namespace _PresentationModel.Scripts.UI
{
    public sealed class StatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtState;

        public void SetState(string state) => txtState.text = state;
    }
}