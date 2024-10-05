using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadService
    {
        UniTask SaveAsync<T>(T data) where T : ISavableData;
        UniTask<T> LoadAsync<T>(T type) where T : ISavableData;
    }
}