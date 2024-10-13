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
    public interface IGameSession : IServerTimeSession
    {
        public event Action OnLastSessionLoad;
        public string LastSessionStart();
        public string LastSessionEnd();
        public string LastSessionDuration();
        public void EndSession();
    }

    public interface IServerTimeSession
    {
        public event Action OnCurrentSessionLoad;
        public bool IsActualTimeReceived();
        public DateTime GetCurrentTime();
        public string GetCurrentTimeString();
    }
    
    public class GameSession : IGameSession
    {
        private const string TimeApiUrl = "https://worldtimeapi.org/api/timezone/Etc/UTC";

        private DateTime _serverTime;
        private DateTime _localTime;
        private bool _isActualTimeReceived = false;
        public bool IsActualTimeReceived() => _isActualTimeReceived;

        public event Action OnLastSessionLoad;
        public event Action OnCurrentSessionLoad;
        
        [CanBeNull] private GameSessionData _lastGameSessionData;
        private readonly GameSessionData _currentGameSessionData;
        
        private readonly GameSessionProvider _gameSessionProvider;

        [Inject]
        public GameSession(GameSessionProvider gameSessionProvider)
        {
            _gameSessionProvider = gameSessionProvider;
            _currentGameSessionData = new GameSessionData();
            
            LoadLastSession().Forget();
            
            StartSession();
        }

        private async UniTaskVoid LoadLastSession()
        {
            //TODO Надо вернуть загрузку
            //_lastGameSessionData = await _gameSessionProvider.LoadUnitsAsync();
            //Debug.Log($"LoadLastSession = {_lastGameSessionData?.sessionStart}");
            //Debug.Log($"LoadLastSession = {_lastGameSessionData?.sessionEnd}");
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
            var gameDuration = TimeSpan.Parse(_lastGameSessionData?.allSessionDuration ?? "0");
            return $"Time game: {gameDuration.Hours:D2}:{gameDuration.Minutes:D2}.{gameDuration.Seconds:D2}";
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
                
                OnCurrentSessionLoad?.Invoke();
            }
            else
            {
                await UniTask.Delay(2000);
                ServerTime().Forget();
            }
        }

        public void EndSession()
        {
            SaveGameSession();
        }
        
        private void SaveGameSession()
        {
            var startTime = _serverTime;
            var endTime = GetCurrentTime();
            var sessionDuration = endTime.Subtract(startTime);
            var allSessionDuration = TimeSpan.Parse(_lastGameSessionData?.allSessionDuration ?? "0");
            
            _currentGameSessionData.sessionStart = _serverTime.ToString(CultureInfo.InvariantCulture);
            _currentGameSessionData.sessionEnd = endTime.ToString(CultureInfo.InvariantCulture);
            _currentGameSessionData.allSessionDuration = allSessionDuration.Add(sessionDuration).ToString();
            
            //TODO Надо вернуть сохранение
            //_gameSessionProvider.SaveUnitsAsync(_currentGameSessionData).Forget();
        }
    }
}