namespace _PresentationModel.Scripts.LevelUp.Data
{
    public struct PlayerLevelData
    {
        public PlayerLevelData(int currentLevel, int currentExperience, int requiredExperience)
        {
            CurrentLevel = currentLevel;
            _currentExperience = currentExperience;
            _requiredExperience = requiredExperience;
        }

        public int CurrentLevel { get; private set; }
        private readonly int _currentExperience;
        private readonly int _requiredExperience;

        public string StringExperience => $"XP: {_currentExperience}/{_requiredExperience}";
        public float ProgressExperience => _requiredExperience > 0 ? (float)_currentExperience / _requiredExperience : 0;
        public bool CanLevelUp() => _currentExperience == _requiredExperience;
    }
}