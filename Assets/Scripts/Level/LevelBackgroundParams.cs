using System;

namespace ShootEmUp
{
    [Serializable]
    public sealed class LevelBackgroundParams
    {
        public float startPositionY; 
        public float endPositionY;
        public float movingSpeedY;
    }
}