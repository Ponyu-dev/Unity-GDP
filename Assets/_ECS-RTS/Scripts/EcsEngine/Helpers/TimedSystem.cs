using Leopotam.EcsLite;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Helpers
{
    internal abstract class TimedSystem : IEcsRunSystem
    {
        private float _lastRunTime;
        private readonly float _delay;

        protected TimedSystem(float delay)
        {
            _delay = delay;
            _lastRunTime = -_delay; // Инициализация для немедленного старта при первом запуске
        }

        public void Run(IEcsSystems systems)
        {
            // Получаем текущее время (в секундах) с начала выполнения системы
            var deltaTime = Time.deltaTime;
            _lastRunTime += deltaTime;

            if (_lastRunTime < _delay)
                return; // Пропускаем выполнение, если время задержки еще не прошло

            _lastRunTime = 0f; // Сбрасываем таймер

            Execute(systems);
        }
        
        protected abstract void Execute(IEcsSystems systems);
    }
}