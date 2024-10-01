using System;
using System.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadService
    {
        event Action SaveCompleted;
        event Action SaveFailed;
        event Action LoadCompleted;
        event Action<Exception> LoadFailed;
        
        Task SaveAsync<T>(T data);
        Task<T> LoadAsync<T>();
    }
}