using System;
using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadService
    {
        event Action SaveCompleted;
        event Action SaveFailed;
        event Action LoadCompleted;
        event Action<Exception> LoadFailed;
        
        UniTask SaveAsync<T>(T data);
        UniTask<T> LoadAsync<T>();
    }
}