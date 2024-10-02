using TMPro;
using UnityEngine;
using VContainer;

namespace _ChestMechanics.Session
{
    public class GameSessionView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txtCurrentSessionDate;
        
        [SerializeField]
        private TextMeshProUGUI txtLastSessionStart;
        
        [SerializeField]
        private TextMeshProUGUI txtLastSessionEnd;
        
        [SerializeField]
        private TextMeshProUGUI txtLastSessionDuration;
        
        private IGameSession _gameSession;
        
        [Inject]
        public void Construct(IGameSession gameSession)
        {
            Debug.Log("[GameSessionView] Construct");
            _gameSession = gameSession;
        }

        private void OnEnable()
        {
            _gameSession.OnCurrentSessionLoad += OnUpdateCurrentSession;
            _gameSession.OnLastSessionLoad += OnUpdateLastSession;
        }
        
        private void OnDisable()
        {
            _gameSession.OnCurrentSessionLoad -= OnUpdateCurrentSession;
            _gameSession.OnLastSessionLoad -= OnUpdateLastSession;
        }

        private void OnUpdateCurrentSession()
        {
            txtCurrentSessionDate.text = _gameSession.GetCurrentTimeString();
        }

        private void OnUpdateLastSession()
        {
            Debug.Log("[GameSessionView] Start");   
            txtLastSessionStart.text = _gameSession.LastSessionStart();
            txtLastSessionEnd.text = _gameSession.LastSessionEnd();
            txtLastSessionDuration.text = _gameSession.LastSessionDuration();
        }
        
        private void OnApplicationQuit()
        {
            _gameSession.EndSession();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Приложение ушло в фон
                _gameSession.EndSession();
            }
            else
            {
                // Приложение вернулось на передний план
                //_gameSession.StartSession();
            }
        }
    }
}