using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using Game.Scripts.Contexts.Base.EntityPool;
using Game.Scripts.Contexts.ZombieContext.Pool;

namespace Game.Scripts.Contexts.ZombieContext.MovementSystem
{
    public sealed class ZombieSpawnSystem : IContextInit, IContextEnable, IContextDisable, IContextUpdate
    {
        private IContext _gameContext;
        private Cycle _spawnPeriod;
        private IEntityPool _zombiePool;
        private Const<int> _zombieSpawnMax;
        private IReactiveValue<bool> _playerIsDead;

        public void Init(IContext context)
        {
            _gameContext = context;
            _spawnPeriod = context.GetZombieSystemData().spawnCycle;
            _zombieSpawnMax = context.GetZombieActiveMax();
            _zombiePool = context.GetZombieSystemData().pool;
            _playerIsDead = context.GetAttackPlayer().Value.GetIsDead();
        }

        public void Enable(IContext context)
        {
            _spawnPeriod.Start();
            _spawnPeriod.OnCycle += Spawn;
            _playerIsDead.Subscribe(OnPlayerDead);
        }

        private void OnPlayerDead(bool isDead)
        {
            if (isDead)
            {
                Clear();
                _spawnPeriod.Stop();
            }
            else
            {
                _spawnPeriod.Start();
            }
        }

        private void Clear()
        {
            foreach (var active in _zombiePool.GetActives())
            {
                active.GetIsDead().Value = true;
            }
        }

        private void Spawn()
        {
            if (_playerIsDead.Value)
                return;
            if (_zombiePool.CountActives() >= _zombieSpawnMax.Value)
                return;
            var zombie = _gameContext.SpawnZombieInPosition();
            if (!zombie.HasAttackEntity())
                zombie.AddAttackEntity(_gameContext.GetAttackPlayer());
            zombie.GetIsDead().Value = false;
            zombie.GetDeadAction().Subscribe(OnDead);
        }

        private async void OnDead(IEntity zombie)
        {
            zombie.GetDeadAction().Unsubscribe(OnDead);
            await UniTask.Delay(1500);
            _zombiePool.Return(zombie);
        }

        public void Disable(IContext context)
        {
            _spawnPeriod.Stop();
            _spawnPeriod.OnCycle -= Spawn;
            _playerIsDead.Unsubscribe(OnPlayerDead);
        }

        public void Update(IContext context, float deltaTime)
        {
            _spawnPeriod.Tick(deltaTime);
        }
    }
}