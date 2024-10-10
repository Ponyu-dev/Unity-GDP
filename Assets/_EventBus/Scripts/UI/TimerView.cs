using TMPro;
using UnityEngine;

namespace UI
{
    public interface ITimerView
    {
        public int TimerCount { get; }
        public void SetTimer(string timer);
        public void Show();
        public void Hide();
    }
    
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private TextMeshProUGUI textTimer;
        [SerializeField] private int timerCount;
        public int TimerCount => timerCount;

        public void SetTimer(string timer)
        {
            textTimer.text = timer;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            textTimer.text = string.Empty;
        }
    }
}