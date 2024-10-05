using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadService
    {
        UniTask SaveAsync<T>(T data);
        UniTask<T> LoadAsync<T>();
    }
}