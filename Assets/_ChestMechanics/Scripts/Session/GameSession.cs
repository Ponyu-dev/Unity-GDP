using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using VContainer;

namespace _ChestMechanics.Session
{
    public interface IGameSession
    {
        public event Action OnCurrentSessionLoad;
        public event Action OnLastSessionLoad;
        public string GetCurrentTimeString();
        public DateTime GetCurrentTime();
        public string LastSessionStart();
        public string LastSessionEnd();
        public string LastSessionDuration();
        public void EndSession();
    } 
    
    public class GameSession : IGameSession
    {
        private const string TimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";

        private DateTime _serverTime;
        private DateTime _localTime;
        private bool _isActualTimeReceived = false;

        public event Action OnLastSessionLoad;
        public event Action OnCurrentSessionLoad;
        
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

        public DateTime GetCurrentTime()
        {
            if (!_isActualTimeReceived) throw new Exception("Actual Time Not Received");

            var elapsed = DateTime.Now - _localTime;
            var currentTime = _serverTime.Add(elapsed);

            return currentTime;
        }
        
        public string GetCurrentTimeString()
        {
            return $"Start: {GetCurrentTime().ToString(CultureInfo.InvariantCulture)}";
        }

        public string LastSessionStart() => $"LastStart: {_lastGameSessionData?.sessionStart}";
        public string LastSessionEnd() => $"LastEnd: {_lastGameSessionData?.sessionEnd}";

        public string LastSessionDuration()
        {
            return $"Время в игре: {_lastGameSessionData?.allSessionDuration}";
        }

        private void StartSession()
        {
            ServerTime().Forget();
        }

        private async UniTaskVoid ServerTime()
        {
            var request = UnityWebRequest.Get(TimeApiUrl);

            await request.SendWebRequest().ToUniTask();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var json = request.downloadHandler.text;
                var data = JObject.Parse(json);
                var dateTimeStr = data["utc_datetime"]?.ToString();

                _serverTime = DateTime.Parse(dateTimeStr);
                _localTime = DateTime.Now;

                _isActualTimeReceived = true;
                
                Debug.Log($"ServerTime result = {request.result} _serverTime = {_serverTime.ToString(CultureInfo.InvariantCulture)}");
                
                OnCurrentSessionLoad?.Invoke();
            }
            else
            {
                Debug.LogError($"ServerTime result = {request.result} error = {request.error}");
                await UniTask.Delay(2000);
                ServerTime().Forget();
            }
        }

        public void EndSession()
        {
            Debug.Log($"EndSession {_currentGameSessionData.sessionEnd}");
            SaveGameSession();
        }

        private void SaveGameSession()
        {
            var startTime = _serverTime;
            var endTime = GetCurrentTime();
            var sessionDuration = endTime.Subtract(startTime);
            var allSessionDuration = TimeSpan.Parse(_currentGameSessionData?.allSessionDuration ?? "0");
            
            _currentGameSessionData.sessionStart = _serverTime.ToString(CultureInfo.InvariantCulture);
            _currentGameSessionData.sessionEnd = endTime.ToString(CultureInfo.InvariantCulture);
            _currentGameSessionData.allSessionDuration = allSessionDuration.Add(sessionDuration).ToString();
            
            _gameSessionSave.SaveUnitsAsync(_currentGameSessionData).Forget();
        }
    }
}