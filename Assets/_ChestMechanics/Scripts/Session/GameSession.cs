using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace _ChestMechanics.Session
{
    public interface IGameSession
    {
        public event Action OnLastSessionLoad;
        public string CurrentSessionStart();
        public string LastSessionStart();
        public string LastSessionEnd();
        public string LastSessionDuration();
        public void StartSession();
        public void EndSession();
    } 
    
    public class GameSession : IGameSession
    {
        public event Action OnLastSessionLoad;
        
        [CanBeNull] private GameSessionData _lastGameSessionData;
        private readonly GameSessionData _currentGameSessionData;
        
        private readonly GameSessionSave _gameSessionSave;

        [Inject]
        public GameSession(GameSessionSave gameSessionSave)
        {
            _gameSessionSave = gameSessionSave;
            _currentGameSessionData = new GameSessionData();
            
            LoadLastSession().Forget();
            
            StartSession();
        }

        private async UniTaskVoid LoadLastSession()
        {
            _lastGameSessionData = await _gameSessionSave.LoadUnitsAsync();
            Debug.Log($"LoadLastSession = {_lastGameSessionData?.sessionStart}");
            Debug.Log($"LoadLastSession = {_lastGameSessionData?.sessionEnd}");
            OnLastSessionLoad?.Invoke();
        }
        
        public string CurrentSessionStart() => _currentGameSessionData.sessionStart;

        public string LastSessionStart() => _lastGameSessionData?.sessionStart;
        public string LastSessionEnd() => _lastGameSessionData?.sessionEnd;

        public string LastSessionDuration()
        {
            Debug.Log($"LastSessionDuration {LastSessionStart()}");
            Debug.Log($"LastSessionDuration {_lastGameSessionData?.sessionEnd}");
            
            if (!DateTime.TryParse(LastSessionStart(), out var start) ||
                !DateTime.TryParse(_lastGameSessionData?.sessionEnd, out var end)) return "";
            
            var sessionDuration = end.Subtract(start);
            return sessionDuration.ToString();
        }

        public void StartSession()
        {
            _currentGameSessionData.sessionStart = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Debug.Log($"StartSession {_currentGameSessionData.sessionStart}");
        }

        public void EndSession()
        {
            _currentGameSessionData.sessionEnd = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Debug.Log($"EndSession {_currentGameSessionData.sessionEnd}");
            _gameSessionSave.SaveUnitsAsync(_currentGameSessionData).Forget();
        }
    }
}