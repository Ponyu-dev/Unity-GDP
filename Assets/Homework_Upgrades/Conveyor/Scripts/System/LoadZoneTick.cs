using System.Threading;
using Cysharp.Threading.Tasks;
using Elementary;

namespace Homework_Upgrades.Conveyor.Scripts.System
{
    public sealed class LoadZoneTick
    {
        private const int TIMER = 2000;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IVariableLimited<int> _loadStorage;

        public LoadZoneTick(IVariableLimited<int> loadStorage)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _loadStorage = loadStorage;
            _loadStorage.Current += 1;
            StartTimer(_cancellationTokenSource.Token);
        }

        private async void StartTimer(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TIMER, cancellationToken: cancellationToken);
                _loadStorage.Current += 1;
            }
        }

        ~LoadZoneTick()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}